using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Роли пользователей
    /// </summary>
    public class Role
    {
        public int Id { get; set; }

        [Length(2, 128, ErrorMessage = "Наименование роли должно содержать от 2 до 128 символов")]
        public string Name { get; set; }
    }
}
