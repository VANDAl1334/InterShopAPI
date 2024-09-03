namespace InterShopAPI.DTO;

public class ProductMinimalDTO
{
    public int Id { get; set;}

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? CategoryName { get; set; }

    public string? PreviewPath { get; set; } = "Default.jpg";

    public bool IsDeleted { get; set; }

    public bool OnSale { get; set; }

    public float Rating { get; set; }

    public float Price { get; set;}

    public byte Discount { get; set; } 
}