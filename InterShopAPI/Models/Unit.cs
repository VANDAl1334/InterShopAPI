using System.ComponentModel.DataAnnotations;

namespace InterShopAPI.Models
{
    /// <summary>
    /// Единицы измерения
    /// </summary>
    public class Unit
    {
        public int Id { get; set; }

        [Length(2, 32, ErrorMessage = "Наименование едицины измерения должно содержать от 2 до 32 символов")]
        public string Name { get; set; }

        [Length(2, 128, ErrorMessage = "Полное наименование едицины измерения должно содержать от 2 до 128 символов")]
        public string FullName { get; set; }
    }
}
