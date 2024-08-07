using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Характеристики варианта товара
    /// </summary>
    public class ProductVariantCharacteristics
    {
        public int ProductVariantID { get; set; }

        public int CharacteristicID { get; set; }

        [Length(2, 512, ErrorMessage = "Значение характеристики варианта товара должно содержать от 2 до 512 символов")]
        public string Value { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
        public virtual Characteristic Characteristic { get; set; }
    }
}
