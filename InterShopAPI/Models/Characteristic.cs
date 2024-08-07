using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Характеристики товара
    /// </summary>
    public class Characteristic
    {
        public int Id { get; set; }

        [Length(2, 512, ErrorMessage = "Название характеристики должно содержать от 2 до 512 символов")]
        public string Name { get; set; }

        public int DataTypeID { get; set; }

        public int? UnitID { get; set; }

        public virtual DataType DataType { get; set; }
        public virtual Unit? Unit { get; set; }
        public virtual ICollection<ProductVariantCharacteristics> ProductVariantCharacteristics { get; set; }
    }
}
