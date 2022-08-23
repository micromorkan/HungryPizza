using HungryPizza.Infra.Entities;

namespace HungryPizza.Infra.Repositories.Interface
{
    public interface IPizzaFlavorRepository
    {
        List<PizzaFlavor> GetAll();
        void Add(PizzaFlavor entity);
    }
}
