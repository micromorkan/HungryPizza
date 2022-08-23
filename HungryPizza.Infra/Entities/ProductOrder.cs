namespace HungryPizza.Infra.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public string ProductOrderType { get; set; }
        public string Flavors { get; set; }
        public int Quantity { get; set; }
    }
}
