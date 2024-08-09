using InterShopAPI.Models;

namespace InterShopAPI.DTO;

public class ProductDTO
{
    public int Id { get; set; }

        //[Length(2, 256, ErrorMessage = "Наименование товара должно содержать от 2 до 512 символов")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? CategoryName { get; set; }

        public string? PreviewPath { get; set; } = "Default.jpg";

        public bool IsDeleted { get; set; }

        public bool OnSale { get; set; }

        public ICollection<DiscountHistoryDTO> DiscountHistories { get; set; }
        public ICollection<ProductVariantDTO> ProductVariants { get; set; }
        public ICollection<ImagesOfProductDTO> ImagesOfProduct { get; set;}
}