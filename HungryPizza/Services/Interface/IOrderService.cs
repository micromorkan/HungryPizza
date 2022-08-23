using HungryPizza.Api.Models;

namespace HungryPizza.Api.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderModel> SaveOrder(OrderModel order);
        Task<List<OrderModel>> GetAll();
        Task<List<OrderModel>> GetOrdersByUser(int userId, int page, int pageSize);
        Task UpdateStatus(OrderModel order);
    }
}
