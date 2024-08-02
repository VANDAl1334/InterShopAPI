namespace InterShopAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int OrderStatusId { get; set; }
        public bool PayStatusId { get; set; }
        public int PaymentTypeId { get; set; }
        public int DeliveryTypeId { get; set; }

        public virtual User User { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PayStatus PayStatus { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}