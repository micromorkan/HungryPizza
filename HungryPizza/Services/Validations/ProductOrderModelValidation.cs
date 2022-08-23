using FluentValidation;
using HungryPizza.Models;

namespace HungryPizza.Api.Services.Validations
{
    public class ProductOrderModelValidation : AbstractValidator<ProductOrderModel>
    {
        public ProductOrderModelValidation()
        {
            RuleSet("Add", () =>
            {
                RuleFor(x => x.Flavors)
                .NotEmpty()
                .WithMessage("The flavor of the pizza is empty!")
                .When(x => x.ProductOrderType == HungryPizza.Models.Enums.ProductOrderType.PIZZA);

                RuleFor(x => x.Flavors.Count)
                .LessThanOrEqualTo(2)
                .WithMessage("You can only choose up to 2 flavors!")
                .When(x => x.ProductOrderType == HungryPizza.Models.Enums.ProductOrderType.PIZZA);
                
                RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product not informed!")
                .When(x => x.ProductOrderType != HungryPizza.Models.Enums.ProductOrderType.PIZZA);

                RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity of products must be greater than 0!");
            });
        }
    }
}
