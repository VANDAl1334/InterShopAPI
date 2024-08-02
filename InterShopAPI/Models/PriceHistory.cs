using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public int ProductVariantId { get; set; }
        public DateOnly Date { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "Цена должна быть положительным числом")]
        public float Price { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}

