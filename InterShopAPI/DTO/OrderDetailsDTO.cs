namespace InterShopAPI.DTO;

public class OrderDetailsDTO
{
    public ProductVariantMinimalDTO ProductVariant { get; set; }

    public int Count { get; set; }

    public float Price { get; set; }
}