using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Склад
    /// </summary>
    public class Shop
    {
        public int Id { get; set; }

        [Length(2, 256, ErrorMessage = "Адрес склада должен содержать от 2 до 512 символов")]
        public string Address { get; set; }

        public float Latitude { get; set; } // широта

        public float Longitude { get; set; } // долгота

        public string YandexLink { get; set; }
    }
}