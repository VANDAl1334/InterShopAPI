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
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "Минимум 4 символа", MinimumLength = 4)]
        public string Login { get; set; }
        [StringLength(50, ErrorMessage = "Минимум 6 символов", MinimumLength = 6)]
        public string? Mail { get; set; } // Почта
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(256, ErrorMessage = "Минимум 10", MinimumLength = 10)]
        public string Password { get; set; } // Хеш пароля
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }   
    
}
