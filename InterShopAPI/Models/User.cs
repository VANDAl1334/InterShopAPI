using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace InterShopAPI.Models
{
    public class User
    {
        /// <summary>
        /// Модель пользователя с его свойствами
        /// </summary>
        public int Id { get; set; } // Идентификационный номер
        [StringLength(30, MinimumLength = 5)]
        public string? Login { get; set; } // Регистрационное имя (автоматическое)
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(50, ErrorMessage = "Минимум 4 символа ", MinimumLength = 4)]
        public string? Mail { get; set; } // Почта
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(84, ErrorMessage = "Минимум 8", MinimumLength = 8)]
        [NotMapped]
        public string Password { get; set; } // Пароль
        [Required(ErrorMessage = "Обязательное поле")]
        [DisplayName("ConfirmPassword")]
        [NotMapped]
        public string? ConfirmPassword { get; set; }
        public byte[] HashPassword { get; set; }
        public string JwtToken { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
    
}
