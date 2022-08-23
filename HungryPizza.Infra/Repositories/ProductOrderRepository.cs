using Dapper;
using HungryPizza.Infra.Entities;
using HungryPizza.Infra.Infrastructure;
using HungryPizza.Infra.Repositories.Interface;

namespace HungryPizza.Infra.Repositories
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private DbSession _session;

        public ProductOrderRepository(DbSession session)
        {
            _session = session;
        }

        public ProductOrder Add(ProductOrder entity)
        {
            entity.Id = _session.Connection.ExecuteScalar<int>(
                "INSERT INTO [PRODUCTORDER] (OrderId, ProductId, ProductOrderType, Flavors, Quantity) VALUES (@OrderId, @ProductId, @ProductOrderType, @Flavors, @Quantity);" +
                "SELECT SCOPE_IDENTITY()",
                entity,
                _session.Transaction
                );

            return entity;
        }

        public List<ProductOrder> GetAllFromOrder(int orderId)
        {
            return _session.Connection.Query<ProductOrder>("SELECT Id, OrderId, ProductId, ProductOrderType, Flavors, Quantity FROM [PRODUCTORDER] WHERE OrderId = @orderId;",
                new { orderId = orderId },
                _session.Transaction).ToList();
        }
    }
}
