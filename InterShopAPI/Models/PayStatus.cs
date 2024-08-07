using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Статус оплаты заказа
    /// </summary>
    public class PayStatus                        
    {
        public int Id { get; set; }

        [Length(2, 256, ErrorMessage = "Наименование статуса оплаты должно содержать от 2 до 256 символов")]
        public string Name { get; set; }
    }
}