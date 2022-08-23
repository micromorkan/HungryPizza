using FluentAssertions;
using HungryPizza.Api.Models;
using HungryPizza.Api.Services.Interface;
using HungryPizza.Controllers;
using HungryPizza.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HungryPizza.UnitTests.System.Controllers
{
    public class UnitTest1
    {
        private List<PizzaFlavorModel> MockListPizzaFlavorModel = new List<PizzaFlavorModel>() 
        { 
            new PizzaFlavorModel { Flavor = "Mussarela", IsLacking = false, Price = (decimal)42.5 },
            new PizzaFlavorModel { Flavor = "3 Queijos", IsLacking = false, Price = (decimal)50 },
            new PizzaFlavorModel { Flavor = "Frango com requeijão", IsLacking = false, Price = (decimal)59.99 },
            new PizzaFlavorModel { Flavor = "Calabresa", IsLacking = true, Price = (decimal)42.5 },
            new PizzaFlavorModel { Flavor = "Pepperoni", IsLacking = false, Price = (decimal)55 },
            new PizzaFlavorModel { Flavor = "Portuguesa", IsLacking = false, Price = (decimal)45 },
            new PizzaFlavorModel { Flavor = "Veggie", IsLacking = false, Price = (decimal)59.99 } 
        };

        [Fact]
        public async Task SaveOrder_WithExistingUser_ReturnMaxPizzaQtd()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.UserId = 1;
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { "Mussarela" },
                    Quantity = 11
                }
            };
            orderModel.FormPayment = Models.Enums.FormPayment.CREDITCARD;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("An order can only have a maximum of 10 pizzas!");
        }

        [Fact]
        public async Task SaveOrder_WithExistingUser_ReturnPizzaFlavorsEmpty()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.UserId = 1;
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { },
                    Quantity = 1
                }
            };
            orderModel.FormPayment = Models.Enums.FormPayment.CREDITCARD;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("The flavor of the pizza is empty!");
        }

        [Fact]
        public async Task SaveOrder_WithExistingUser_ReturnPizzaMaxFlavors()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.UserId = 1;
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { "Mussarela","Portuguesa","Pepperoni" },
                    Quantity = 11
                }
            };
            orderModel.FormPayment = Models.Enums.FormPayment.CREDITCARD;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("You can only choose up to 2 flavors!");
        }

        [Fact]
        public async Task SaveOrder_WithExistingUser_ReturnMinQuantityProducts()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.UserId = 1;
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { "Mussarela" },
                    Quantity = 0
                }
            };
            orderModel.FormPayment = Models.Enums.FormPayment.CREDITCARD;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("Quantity of products must be greater than 0!");
        }

        [Fact]
        public async Task SaveOrder_WithExistingUser_ReturnLackingPizzaFlavor()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.UserId = 1;
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { "Calabresa" },
                    Quantity = 0
                }
            };
            orderModel.FormPayment = Models.Enums.FormPayment.CREDITCARD;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("Flavor Calabresa is out of stock");
        }

        [Fact]
        public async Task SaveOrder_WithExistingUser_ReturnFormPayment()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.UserId = 1;
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { "Mussarela" },
                    Quantity = 1
                }
            };
            orderModel.FormPayment = null;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("Form Payment is empty!");
        }

        [Fact]
        public async Task SaveOrder_WithNewUser_ReturnMissingUserData()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { "Mussarela" },
                    Quantity = 1
                }
            };
            orderModel.FormPayment = Models.Enums.FormPayment.CREDITCARD;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("User data is empty!");
        }

        [Fact]
        public async Task SaveOrder_WithNewUser_ReturnMissingAddress()
        {
            OrderModel orderModel = new OrderModel();
            orderModel.User = new UserModel();
            orderModel.OrderItems = new List<Models.ProductOrderModel>()
            {
                new Models.ProductOrderModel()
                {
                    ProductOrderType = Models.Enums.ProductOrderType.PIZZA,
                    Flavors = new List<string> { "Mussarela" },
                    Quantity = 1
                }
            };
            orderModel.FormPayment = Models.Enums.FormPayment.CREDITCARD;

            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(service => service.SaveOrder(It.IsAny<OrderModel>()))
                .ReturnsAsync(new OrderModel());

            var mockPizzaFlavorService = new Mock<IPizzaFlavorService>();
            mockPizzaFlavorService
               .Setup(service => service.GetAll())
               .ReturnsAsync(MockListPizzaFlavorModel);

            var controller = new OrderController(mockOrderService.Object, mockPizzaFlavorService.Object);

            var result = (JsonResult)await controller.SaveOrder(orderModel);
            var objectResult = (ApiReturnModel)result?.Value;

            objectResult.IsSuccess.Should().BeFalse();
            objectResult.ErroList.Should().Contain("User Name is empty!");
            objectResult.ErroList.Should().Contain("User Cpf is empty!");
            objectResult.ErroList.Should().Contain("User City is empty!");
            objectResult.ErroList.Should().Contain("User Street is empty!");
            objectResult.ErroList.Should().Contain("User ZipCode is empty!");
        }
    }
}