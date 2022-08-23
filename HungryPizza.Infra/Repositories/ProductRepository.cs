using Dapper;
using HungryPizza.Infra.Entities;
using HungryPizza.Infra.Repositories.Interface;
using HungryPizza.Infra.UnitOfWork;

namespace HungryPizza.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DbSession _session;

        public ProductRepository(DbSession session)
        {
            _session = session;
        }

        public Product GetById(int id)
        {
            return _session.Connection.Query<Product>("SELECT Id, Name, ProductType, Price FROM [PRODUCT] WHERE Id = @id;",
                new { Id = id },
                _session.Transaction).FirstOrDefault();
        }
    }
}
