namespace InterShopAPI.Models
{
    /// <summary>
    /// Характеристики
    /// </summary>
    public class Characteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DataTypeID { get; set; }
        public int? UnitID { get; set; }
        public virtual DataType DataType { get; set; }
        public virtual Unit? Unit { get; set; }
        public virtual ICollection<ProductVariantCharacteristics> ProductVariantCharacteristics { get; set; }
    }
}
