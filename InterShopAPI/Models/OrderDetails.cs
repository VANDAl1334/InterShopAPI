using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Состав заказа
    /// </summary>
    public class OrderDetails
    {
        public int OrderId { get; set; }

        public int ProductVariantId { get; set; }

        [Range(1, 100, ErrorMessage = "Количество одного вида товара в заказе может варьироваться от 1 до 100")]
        public int Count { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Стоимость не может быть меньше нуля")]
        public float Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}