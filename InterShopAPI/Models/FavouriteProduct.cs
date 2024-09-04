namespace InterShopAPI.Models;

/// <summary>
/// Избранные товары пользователей
/// </summary>
public class FavouriteProduct
{
    public int UserId { get; set; }
    public int ProductId { get; set; }

    public User User { get; set; }
    public Product Product { get; set; }
}