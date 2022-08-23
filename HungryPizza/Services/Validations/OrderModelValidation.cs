using FluentValidation;
using HungryPizza.Api.Models;
using HungryPizza.Api.Services.Interface;
using HungryPizza.Models;
using HungryPizza.Models.Enums;

namespace HungryPizza.Api.Services.Validations
{
    public class OrderModelValidation : AbstractValidator<OrderModel>
    {
        private readonly IPizzaFlavorService _pizzaFlavorService;

        public OrderModelValidation(IPizzaFlavorService pizzaFlavorService)
        {
            _pizzaFlavorService = pizzaFlavorService;

            RuleSet("Add", () =>
            {
                RuleFor(x => x.FormPayment)
                .NotEmpty()
                .WithMessage("Form Payment is empty!");

                RuleFor(x => x.User)
                .NotEmpty()
                .WithMessage("User data is empty!")
                .When(x => x.UserId == 0);

                RuleFor(x => x.User.Name)
                .NotEmpty()
                .WithMessage("User Name is empty!")
                .When(x => x.UserId == 0 && x.User != null);

                RuleFor(x => x.User.Cpf)
                .NotEmpty()
                .WithMessage("User Cpf is empty!")
                .When(x => x.UserId == 0 && x.User != null);

                RuleFor(x => x.User.ZipCode)
                .NotEmpty()
                .WithMessage("User ZipCode is empty!")
                .When(x => x.UserId == 0 && x.User != null);

                RuleFor(x => x.User.City)
                .NotEmpty()
                .WithMessage("User City is empty!")
                .When(x => x.UserId == 0 && x.User != null);

                RuleFor(x => x.User.Street)
                .NotEmpty()
                .WithMessage("User Street is empty!")
                .When(x => x.UserId == 0 && x.User != null);

                RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("At least one Product need be informed!");

                RuleFor(x => x.OrderItems)
                .Must(ValidateIsLackingFlavor)
                .WithMessage((_) => "");

                RuleFor(x => x.OrderItems.Where(z => z.ProductOrderType == HungryPizza.Models.Enums.ProductOrderType.PIZZA).Sum(c => c.Quantity))
                .LessThanOrEqualTo(10)
                .WithMessage("An order can only have a maximum of 10 pizzas!");

                RuleForEach(x => x.OrderItems).SetValidator(new ProductOrderModelValidation(), "Add");

            });

            RuleSet("UpdateStatus", () =>
            {
                RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is empty!");

                RuleFor(x => x.OrderStatus)
                .NotEmpty()
                .WithMessage("Order Status is empty!");
            });

            RuleSet("GetOrdersByUser", () =>
            {
                RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is empty!");
            });

            bool ValidateIsLackingFlavor(OrderModel orderModel, List<ProductOrderModel> orderItems, ValidationContext<OrderModel> validationContext)
            {
                bool isOk = true;
                var pizzaFlavorsDB = _pizzaFlavorService.GetAll().Result;

                var pizzaFlavorsOrder = orderItems.Where(x => x.ProductOrderType == ProductOrderType.PIZZA).SelectMany(x => x.Flavors).Distinct();

                foreach (var flavor in pizzaFlavorsOrder)
                {
                    if (pizzaFlavorsDB.Where(x => x.Flavor == flavor).FirstOrDefault().IsLacking)
                    {
                        validationContext.AddFailure("Flavor " + flavor + " is out of stock");
                        isOk = false;
                    }
                }

                return isOk;
            }
        }
    }
}
