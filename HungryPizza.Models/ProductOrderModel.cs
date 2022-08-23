using HungryPizza.Models.Enums;
using System.Text.Json.Serialization;

namespace HungryPizza.Models
{
    public class ProductOrderModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductOrderType ProductOrderType { get; set; }
        public List<string> Flavors { get; set; }
        public int Quantity { get; set; }
    }
}
