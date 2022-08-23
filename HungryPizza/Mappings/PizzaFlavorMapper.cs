using HungryPizza.Api.Models;
using HungryPizza.Infra.Entities;

namespace HungryPizza.Api.Mappings
{
    public static class PizzaFlavorMapper
    {
        public static List<PizzaFlavorModel> MapToPizzaFlavorModelList(this List<PizzaFlavor> list)
        {
            return list.Select(model => model.MapToPizzaFlavorModel()).ToList();
        }

        public static PizzaFlavorModel MapToPizzaFlavorModel(this PizzaFlavor entity)
        {
            return new PizzaFlavorModel
            {
                Flavor = entity.Flavor,
                IsLacking = entity.IsLacking,
                Price = entity.Price,
            };
        }

        public static PizzaFlavor MapToPizzaFlavorEntity(this PizzaFlavorModel model)
        {
            return new PizzaFlavor
            {
                Flavor = model.Flavor,
                IsLacking = model.IsLacking,
                Price = model.Price,
            };
        }
    }
}
