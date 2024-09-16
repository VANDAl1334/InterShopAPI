using InterShopAPI.Libs;
using Newtonsoft.Json;

namespace  InterShopAPI.DTO;

public class OrderDetailDTO
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime DateTimeCreating { get; set; }

    public DateTime DateTimeClosing { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly DeliveryDate { get; set; }

    public string? OrderStatus { get; set; }

    public string? PayStatus { get; set; }

    public string PaymentType { get; set; }

    public string DeliveryType { get; set; }

    public string DeliveryAddress { get; set; }

    public float TotalCost { get; set; }

    public ICollection<OrderDetailsDTO> OrderDetails { get; set; }
}