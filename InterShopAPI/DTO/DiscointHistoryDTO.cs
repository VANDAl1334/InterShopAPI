namespace InterShopAPI.DTO;

public class DiscountHistoryDTO
{
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public byte Discount { get; set; }
}