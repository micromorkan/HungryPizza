using Dapper;
using HungryPizza.Infra.Entities;
using HungryPizza.Infra.Infrastructure;
using HungryPizza.Infra.Repositories.Interface;

namespace HungryPizza.Infra.Repositories
{
    public class PizzaFlavorRepository : IPizzaFlavorRepository
    {
        private DbSession _session;

        public PizzaFlavorRepository(DbSession session)
        {
            _session = session;
        }

        public void Add(PizzaFlavor entity)
        {
            _session.Connection.Execute(
                "",
                entity,
                _session.Transaction
                );
        }

        public List<PizzaFlavor> GetAll()
        {
            return _session.Connection.Query<PizzaFlavor>("SELECT Flavor, Price, IsLacking FROM [Flavors]", param: null, 
                _session.Transaction).ToList();
        }
    }
}
