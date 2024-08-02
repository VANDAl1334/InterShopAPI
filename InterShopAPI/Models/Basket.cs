namespace InterShopAPI.Models
{
    /// <summary>
    /// Корзина пользователя
    /// </summary>
    public class Basket
    {
        public int UserId { get; set; }
        public int ProductVariantId { get; set; }
        public int Count { get; set; }

        public virtual User User { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
