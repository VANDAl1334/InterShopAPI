using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateTimeCreating { get; set; }

        public DateTime DateTimeClosing { get; set; }

        public DateOnly DeliveryDate { get; set; }

        public int OrderStatusId { get; set; }

        public int PayStatusId { get; set; }

        public int PaymentTypeId { get; set; }

        public int DeliveryTypeId { get; set; }

        public string DeliveryAddress { get; set; }

        public float TotalCost { get; set; }

        public virtual User User { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PayStatus PayStatus { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}