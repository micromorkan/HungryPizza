using HungryPizza.Api.Models;

namespace HungryPizza.Api.Services.Interface
{
    public interface IPizzaFlavorService
    {
        public Task<List<PizzaFlavorModel>> GetAll();
        public Task Add(PizzaFlavorModel model);
    }
}
