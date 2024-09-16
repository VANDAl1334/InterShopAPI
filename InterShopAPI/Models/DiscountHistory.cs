using System.ComponentModel.DataAnnotations;
using InterShopAPI.Libs;
using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.Models
{
    /// <summary>
    /// История скидок на товары
    /// </summary>
    public class DiscountHistory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public DateOnly DateFrom { get; set; }

        public DateOnly DateTo { get; set; }

        [Range(1, 2, ErrorMessage = "Скидка может быть от 1 до 100")]
        public byte Discount { get; set; }

        public virtual Product Product { get; set; }
    }
}