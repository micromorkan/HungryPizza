using HungryPizza.Api.Mappings;
using HungryPizza.Api.Models;
using HungryPizza.Api.Services.Interface;
using HungryPizza.Infra.UnitOfWork;

namespace HungryPizza.Api.Services
{
    public class PizzaFlavorService : IPizzaFlavorService
    {
        private readonly DbSession _session;
        public PizzaFlavorService(DbSession session)
        {
            _session = session;
        }

        public Task<List<PizzaFlavorModel>> GetAll()
        {
            using (var uow = new UnitOfWork(_session))
            {
                return Task.FromResult(uow.PizzaFlavorRepository.GetAll().MapToPizzaFlavorModelList());
            }
        }

        public Task Add(PizzaFlavorModel model)
        {
            using (var uow = new UnitOfWork(_session))
            {
                return Task.CompletedTask;
            }
        }
    }
}
