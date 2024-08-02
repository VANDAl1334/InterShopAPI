using System.ComponentModel.DataAnnotations.Schema;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Категории товаров
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public Category? Parent { get; set; }
        public string? PreviewPath { get; set; }

        public virtual ICollection<Category> Children { get; }
    }
}