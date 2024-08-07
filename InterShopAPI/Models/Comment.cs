using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models;

/// <summary>
/// Отзыв о товаре 
/// </summary>
public class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [Length(1, 1024, ErrorMessage = "Отзыв должен содержать от 1 до 1024 символов")]
    public string Message { get; set; }

    public DateTime DateTime { get; set; }

    [Range(1, 5, ErrorMessage = "Оценка товара должна находиться в диапазоне от 1 до 5")]
    public int Rating { get; set; }

    public bool IsDeleted { get; set; }

    public int ProductId { get; set; }

    public User User { get; set; }
    public Product Product { get; set; }
}
