using Microsoft.AspNetCore.Mvc;

namespace InterShopAPI.DTO;

public class OrderMinimalDTO
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime DateTimeCreating { get; set; }

    public DateTime DateTimeClosing { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public string OrderStatus { get; set; }

    public string PayStatus { get; set; }

    public string PaymentType { get; set; }

    public string DeliveryType { get; set; }

    public float TotalCost { get; set; }
}