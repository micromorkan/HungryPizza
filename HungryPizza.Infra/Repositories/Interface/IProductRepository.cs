using HungryPizza.Infra.Entities;

namespace HungryPizza.Infra.Repositories.Interface
{
    public interface IProductRepository
    {        
        Product GetById(int id);
    }
}
