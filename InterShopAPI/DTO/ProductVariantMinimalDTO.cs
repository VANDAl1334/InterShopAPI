namespace InterShopAPI.DTO;

public class ProductVariantMinimalDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsMain { get; set; }

    public float Cost { get; set; }
}