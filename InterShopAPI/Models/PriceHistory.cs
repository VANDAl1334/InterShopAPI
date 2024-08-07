using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Models
{
    /// <summary>
    /// История цен на варианты товаров
    /// </summary>
    public class PriceHistory
    {
        public int Id { get; set; }

        public int ProductVariantId { get; set; }

        [Remote(action: "ValidateDate", controller: "PriceHistory", ErrorMessage = "Дата изменения цены не может быть меньше, чем 01.01.2020")]
        public DateOnly Date { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Цена не может быть меньше 0")]
        public float Price { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}

