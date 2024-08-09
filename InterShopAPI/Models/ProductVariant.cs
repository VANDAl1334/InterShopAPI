using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Вариант товара
    /// </summary>
    public class ProductVariant
    {
        public int Id { get; set; }

        public int ProductID { get; set; }

        [Length(2, 512, ErrorMessage = "Наименование варианта товара должно содержать от 2 до 256 символов")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsMain { get; set; }

        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductVariantCharacteristics> ProductVariantCharacteristics { get; set; }
    }
}
