using HungryPizza.Api.Models;
using HungryPizza.Api.Services.Validations;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using HungryPizza.Models;
using HungryPizza.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace HungryPizza.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPizzaFlavorService _pizzaFlavorService;

        public OrderController(IOrderService orderService, IPizzaFlavorService pizzaFlavorService)
        {
            _orderService = orderService;
            _pizzaFlavorService = pizzaFlavorService;
        }

        [HttpGet("{userId}/{page}/{pageSize}", Name = "GetOrders")]
        public async Task<JsonResult> GetOrders(int userId, int page, int pageSize)
        {
            try
            {
                OrderModelValidation validator = new OrderModelValidation(_pizzaFlavorService);
                var resultValidator = await validator.ValidateAsync(new OrderModel() { UserId = userId }, options => options.IncludeRuleSets("GetOrdersByUser"));

                if (!resultValidator.IsValid)
                    return new JsonResult(new ApiReturnModel(null, resultValidator.Errors.Select(x => x.ErrorMessage)));

                var orders = await _orderService.GetOrdersByUser(userId, page, pageSize);

                return new JsonResult(new ApiReturnModel(orders));
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiReturnModel(ex));
            }
        }

        [HttpPost(Name = "SaveOrder")]
        public async Task<JsonResult> SaveOrder(OrderModel order)
        {
            try
            {
                OrderModelValidation validator = new OrderModelValidation(_pizzaFlavorService);
                var resultValidator = await validator.ValidateAsync(order, options => options.IncludeRuleSets("Add"));

                if (!resultValidator.IsValid)
                    return new JsonResult(new ApiReturnModel(null, resultValidator.Errors.Select(x => x.ErrorMessage)));               

                var result = await _orderService.SaveOrder(order);

                return new JsonResult(new ApiReturnModel(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiReturnModel(ex));
            }
        }

        [HttpPut(Name = "UpdateStatus")]
        public async Task<JsonResult> UpdateStatus(OrderModel order)
        {
            try
            {
                OrderModelValidation validator = new OrderModelValidation(_pizzaFlavorService);
                var resultValidator = await validator.ValidateAsync(order, options => options.IncludeRuleSets("UpdateStatus"));

                if (!resultValidator.IsValid)
                    return new JsonResult(new ApiReturnModel(null, resultValidator.Errors.Select(x => x.ErrorMessage)));

                await _orderService.UpdateStatus(order);

                return new JsonResult(new ApiReturnModel());
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiReturnModel(ex));
            }
        }
    }
}