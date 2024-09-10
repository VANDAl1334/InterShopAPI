using InterShopAPI.Models;

namespace InterShopAPI.DTO;

public class ProductDetailDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? CategoryName { get; set; }

    public string? PreviewPath { get; set; } = "Default.jpg";

    public bool IsDeleted { get; set; }

    public bool OnSale { get; set; }

    public float Rating { get; set; }

    public ICollection<DiscountHistoryDTO> DiscountHistories { get; set; }
    public ICollection<ProductVariantDetailDTO> ProductVariants { get; set; }
    public ICollection<ImagesOfProductDTO> ImagesOfProduct { get; set; }
    public ICollection<CommentDTO> Comments { get; set; }
}