using HungryPizza.Api.Mappings;
using HungryPizza.Api.Models;
using HungryPizza.Api.Services.Interface;
using HungryPizza.Infra.Entities;
using HungryPizza.Infra.Infrastructure;
using HungryPizza.Infra.UnitOfWork;
using HungryPizza.Models.Enums;

namespace HungryPizza.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly DbSession _session;
        public OrderService(DbSession session)
        {
            _session = session;
        }

        public Task<List<OrderModel>> GetAll()
        {
            using (var uow = new UnitOfWork(_session))
            {
                var result = uow.OrderRepository.GetAll();

                return Task.FromResult(result.MapToOrderModelList());
            }
        }

        public Task<List<OrderModel>> GetOrdersByUser(int userId, int page, int pageSize)
        {
            using (var uow = new UnitOfWork(_session))
            {
                var result = uow.OrderRepository.GetAllByUser(userId, page, pageSize);

                result = uow.OrderRepository.GetAllByUser(userId, page, pageSize);

                foreach (var order in result)
                {
                    order.OrderItems = uow.ProductOrderRepository.GetAllFromOrder(order.Id);
                }

                return Task.FromResult(result.MapToOrderModelList());
            }
        }

        public Task UpdateStatus(OrderModel model)
        {
            using (var uow = new UnitOfWork(_session))
            {
                var entity = model.MapToOrderEntity();
                uow.OrderRepository.UpdateStatus(entity.Id, entity.OrderStatus);

                return Task.CompletedTask;
            }
        }

        public Task<OrderModel> SaveOrder(OrderModel order)
        {
            using (var uow = new UnitOfWork(_session))
            {
                uow.BeginTransaction();

                order.TotalValue = CalculateTotalAmount(order, uow);

                order.OrderStatus = OrderStatus.RECEIVED;
                order.CreatedAt = DateTime.Now;

                Order entity = order.MapToOrderEntity();

                if (entity.UserId == 0)
                {
                    entity.User = uow.UserRepository.Add(entity.User);
                }
                else
                {
                    entity.User = uow.UserRepository.GetDetails(entity.UserId);
                }

                entity = uow.OrderRepository.Add(entity);

                foreach (var item in entity.OrderItems)
                {
                    item.OrderId = entity.Id;
                    item.Id = uow.ProductOrderRepository.Add(item).Id;
                }

                uow.Commit();

                return Task.FromResult(entity.MapToOrderModel());
            }
        }

        private decimal CalculateTotalAmount(OrderModel order, UnitOfWork uow)
        {
            //Caso futuramente tenha frete basta adicionar a consulta/valor junto ao calculo.            
            decimal totalAmount = 0;
            List<PizzaFlavor> pizzaFlavors = uow.PizzaFlavorRepository.GetAll();

            foreach (var item in order.OrderItems)
            {
                if (item.ProductOrderType == ProductOrderType.PIZZA)
                {
                    if (item.Flavors.Count == 1)
                    {
                        totalAmount += pizzaFlavors.Where(x => item.Flavors.Contains(x.Flavor)).Sum(x => x.Price * item.Quantity);
                    }
                    else
                    {
                        totalAmount += pizzaFlavors.Where(x => item.Flavors.Contains(x.Flavor)).Sum(x => (x.Price / 2) * item.Quantity);
                    }
                }
                else
                {
                    Product entity = uow.ProductRepository.GetById(item.ProductId.Value);

                    if (entity != null)
                    {
                        totalAmount += entity.Price * item.Quantity;
                    }
                }
            }

            //Também é possível fornecer desconto a depender da forma de pagamento. Exemplo abaixo com desconto de 10%
            //if (order.FormPayment == FormPayment.PIX)
            //{
            //    totalAmount = totalAmount * (decimal)0.9;
            //}

            return totalAmount;
        }
    }
}
