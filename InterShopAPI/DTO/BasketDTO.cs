namespace InterShopAPI.DTO;

public class BasketDTO
{
    public int UserId { get; set; }

    public ProductMinimalDTO Product { get; set; }

    public ProductVariantMinimalDTO ProductVariant { get; set; }

    public int Count { get; set; }

    public float TotalCost { get; set; }
}