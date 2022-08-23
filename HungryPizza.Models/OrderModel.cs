using HungryPizza.Models;
using HungryPizza.Models.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace HungryPizza.Api.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public List<ProductOrderModel> OrderItems { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FormPayment? FormPayment { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus? OrderStatus { get; set; }

        public decimal TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
