using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Корзина пользователя
    /// </summary>
    public class Basket
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductVariantId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Количество товара в корзине не может быть меньше 1")]
        public int Count { get; set; }

        public virtual User User { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
