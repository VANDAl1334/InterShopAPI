using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        [Length(2, 256, ErrorMessage = "Наименование товара должно содержать от 2 до 512 символов")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int CategoryID { get; set; } 

        public string? PreviewPath { get; set; } = "Default.jpg";

        public bool IsDeleted { get; set; }

        public bool OnSale { get; set; }

        public Category? Category { get; set; }
        public virtual ICollection<DiscountHistory> DiscountHistories { get; set; }
    }
}
