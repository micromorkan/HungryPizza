using Dapper;
using HungryPizza.Infra.Entities;
using HungryPizza.Infra.Infrastructure;
using HungryPizza.Infra.Repositories.Interface;

namespace HungryPizza.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private DbSession _session;

        public OrderRepository(DbSession session)
        {
            _session = session;
        }

        public Order Add(Order entity)
        {
            entity.Id = _session.Connection.ExecuteScalar<int>(
                "INSERT INTO [ORDER] (UserId, FormPayment, OrderStatus, TotalValue, CreatedAt) VALUES (@UserId, @FormPayment, @OrderStatus, @TotalValue, GETDATE());" +
                "SELECT SCOPE_IDENTITY()",
                entity,
                _session.Transaction
                );

            return entity;
        }

        public Order GetDetails(int id)
        {
            return _session.Connection.Query<Order>("SELECT Id, UserId, FormPayment, OrderStatus, TotalValue, CreatedAt, UpdatedAt FROM [ORDER] WHERE Id = @id",
                new { id = id },
                _session.Transaction).FirstOrDefault();
        }

        public List<Order> GetAll()
        {
            return _session.Connection.Query<Order>("SELECT Id, UserId, FormPayment, OrderStatus, TotalValue, CreatedAt, UpdatedAt FROM [ORDER]",
                param: null,
                _session.Transaction).ToList();
        }

        public List<Order> GetAllByUser(int userId, int page = 1, int qtdPerPage = 10)
        {
            return _session.Connection.Query<Order>("SELECT Id, UserId, FormPayment, OrderStatus, TotalValue, CreatedAt, UpdatedAt FROM [ORDER] WHERE UserId = @userId ORDER BY CreatedAt DESC OFFSET @skip ROWS FETCH NEXT @qtd ROWS ONLY",
                new { userId = userId, skip = (page - 1) * qtdPerPage, qtd  = qtdPerPage },
                _session.Transaction).ToList();
        }

        public void UpdateStatus(int id, string status)
        {
            _session.Connection.Execute(
                "UPDATE [ORDER] SET OrderStatus = @status, UpdatedAt = GETDATE() WHERE Id = @id",
                new { id = id, status = status.ToUpper()},
                _session.Transaction
                );
        }
    }
}
