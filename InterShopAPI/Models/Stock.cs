using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Склад
    /// </summary>
    public class Stock
    {
        public int Id { get; set; }

        [Length(2, 256, ErrorMessage = "Адрес склада должен содержать от 2 до 512 символов")]
        public string Address { get; set; }
    }
}