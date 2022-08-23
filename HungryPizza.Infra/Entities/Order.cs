namespace HungryPizza.Infra.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<ProductOrder> OrderItems { get; set; }
        public string FormPayment { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
