using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public class OrderStatus
    {
        public int Id { get; set; }

        [Length(2, 256, ErrorMessage = "Наименование статуса заказа должно содержать от 2 до 256 символов")]
        public string Name { get; set; }
    }
}