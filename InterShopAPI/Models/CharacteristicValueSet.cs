using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Набор значений характеристики товара
    /// </summary>
    public class CharacteristicValueSet
    {
        public int Id { get; set; }
        
        public int CharacteristicID { get; set; }

        [Length(1, 512, ErrorMessage = "Значение характеристики должно содержать от 1 до 512 символов")]
        public string Value {get; set; }

        public virtual Characteristic Characteristic { get; set; }
    }
}
