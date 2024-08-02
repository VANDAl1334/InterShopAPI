namespace InterShopAPI.Models
{
    /// <summary>
    /// Вариант товара
    /// </summary>
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int InStock { get; set; }
        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductVariantCharacteristics> ProductVariantCharacteristics { get; set; }
    }
}
