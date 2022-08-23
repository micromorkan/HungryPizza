using HungryPizza.Infra.Entities;

namespace HungryPizza.Infra.Repositories.Interface
{
    public interface IOrderRepository
    {
        Order Add(Order entity);
        void UpdateStatus(int id, string status);
        Order GetDetails(int id);
        List<Order> GetAll();
        List<Order> GetAllByUser(int userId, int page = 1, int qtdPerPage = 10);
    }
}
