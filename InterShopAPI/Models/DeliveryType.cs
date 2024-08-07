using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Тип доставки заказа
    /// </summary>
    public class DeliveryType
    {
        public int Id { get; set; }

        [Length(2, 256, ErrorMessage = "Название типа доставки заказа должно содержать от 2 до 256 символов")]
        public string Name { get; set; }
    }
}