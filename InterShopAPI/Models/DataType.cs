using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Типы данных характеристик товара
    /// </summary>
    public class DataType
    {
        public int Id { get; set; }

        [Length(2, 512, ErrorMessage = "Название типа данных должно содержать от 2 до 512 символов")]
        public string Name { get; set; }
    }
}
