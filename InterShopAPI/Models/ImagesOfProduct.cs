using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Фотографии товаров
    /// </summary>
    public class ImagesOfProduct
    {
        public int Id { get; set; }

        [Length(0, 512, ErrorMessage = "Путь к изображению товара должен содержать от 2 до 512 символов")]
        public string Path { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
