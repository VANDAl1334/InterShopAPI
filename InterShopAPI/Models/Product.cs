namespace InterShopAPI.Models
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryID { get; set; }        
        public string? PreviewPath { get; set; }
        public bool IsDeleted { get; set; }
        public bool OnSale { get; set; }

        public Category? Category { get; set; }
        public virtual ICollection<DiscountHistory> DiscountHistories { get; set; }
    }
}
