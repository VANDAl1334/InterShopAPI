namespace InterShopAPI.DTO;

public class ShopDTO
{
    public int Id { get; set; }

    public string Address { get; set; }

    public float Latitude { get; set; } // широта

    public float Longitude { get; set; } // долгота

    public string YandexLink { get; set; }

}