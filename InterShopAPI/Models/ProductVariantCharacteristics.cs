namespace InterShopAPI.Models
{
    /// <summary>
    /// Характеристики варианта товара
    /// </summary>
    public class ProductVariantCharacteristics
    {
        public int ProductVariantID { get; set; }
        public int CharacteristicID { get; set; }
        public string Value { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
        public virtual Characteristic Characteristic { get; set; }
    }
}
