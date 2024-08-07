using Microsoft.EntityFrameworkCore;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Промежуточная таблица характеристик категорий
    /// </summary>
    public class CategoryCharacteristics
    {
        public int CategoryId { get; set; }

        public int CharacteristicId { get; set; }

        public virtual Category? Category { get; set; }        
        public virtual Characteristic? Characteristic { get; set; }
    }
}
