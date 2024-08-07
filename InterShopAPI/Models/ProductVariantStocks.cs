using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Промежуточная таблица. Варианты товаров на складах
    /// </summary>
    public class ProductVariantStocks
    {
        public int ProductVariantId { get; set; }
        
        public int StockId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "На складе не может быть отрицательное количество товара")]
        public int Count { get; set; }

        public ProductVariant ProductVariant { get; set; }
        public Stock Stock { get; set; }
    }
}