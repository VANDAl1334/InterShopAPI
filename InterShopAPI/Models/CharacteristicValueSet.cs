namespace InterShopAPI.Models
{
    /// <summary>
    /// Набор значений характеристики
    /// </summary>
    public class CharacteristicValueSet
    {
        public int Id { get; set; }
        public int CharacteristicID { get; set; }
        public string Value {get; set; }
        public virtual Characteristic Characteristic { get; set; }
    }
}
