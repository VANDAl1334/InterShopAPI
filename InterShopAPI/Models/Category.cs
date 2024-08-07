using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Категории товаров
    /// </summary>
    public class Category
    {
        public int Id { get; set; }

        [Length(2, 512, ErrorMessage = "Название категории должно содержать от 2 до 512 символов")]
        public string Name { get; set; }
        
        public int? ParentID { get; set; }

        [Length(0, 512, ErrorMessage = "Длина пути должна состоять не более, чем из 512 символов")]
        public string? PreviewPath { get; set; } = "Default.jpg";

        public bool IsDeleted { get; set; }

        public Category? Parent { get; set; }
        public virtual ICollection<Category> Children { get; }
    }
}