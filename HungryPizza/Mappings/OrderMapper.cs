using HungryPizza.Api.Models;
using HungryPizza.Api.Util;
using HungryPizza.Infra.Entities;
using HungryPizza.Models;
using HungryPizza.Models.Enums;

namespace HungryPizza.Api.Mappings
{
    public static class OrderMapper
    {
        public static List<OrderModel> MapToOrderModelList(this List<Order> list)
        {
            return list.Select(model => model.MapToOrderModel()).ToList();
        }

        public static OrderModel MapToOrderModel(this Order entity)
        {
            return new OrderModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                TotalValue = entity.TotalValue,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                OrderStatus = Utils.ParseEnum<OrderStatus>(entity.OrderStatus),
                FormPayment = Utils.ParseEnum<FormPayment>(entity.FormPayment),
                OrderItems = entity.OrderItems != null ? entity.OrderItems.Select(model => model.MapToProductOrderModel()).ToList() : null,
                User = entity.User != null ? entity.User.MapToUserModel() : null,
            };
        }

        public static ProductOrderModel MapToProductOrderModel(this ProductOrder entity)
        {
            return new ProductOrderModel
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                ProductOrderType = Utils.ParseEnum<ProductOrderType>(entity.ProductOrderType),
                Flavors = entity.Flavors != null ? entity.Flavors.Split(";").ToList() : null,
                Quantity = entity.Quantity
            };
        }

        public static Order MapToOrderEntity(this OrderModel model)
        {
            return new Order
            {
                Id = model.Id,
                UserId = model.UserId,
                TotalValue = model.TotalValue,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                OrderStatus = model.OrderStatus != null ? Utils.GetEnumString(model.OrderStatus) : null,
                FormPayment = model.FormPayment != null ? Utils.GetEnumString(model.FormPayment) : null,
                OrderItems = model.OrderItems != null ? model.OrderItems.Select(i => i.MapToProductOrderEntity()).ToList() : null,
                User = model.User != null ? model.User.MapToUserEntity() : null
            };
        }

        public static ProductOrder MapToProductOrderEntity(this ProductOrderModel model)
        {
            return new ProductOrder
            {
                Id = model.Id,
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                ProductOrderType = Utils.GetEnumString(model.ProductOrderType),
                Flavors = model.Flavors != null ? String.Join(";", model.Flavors) : null,
                Quantity = model.Quantity
            };
        }
    }
}
