using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterShopAPI.Models;

namespace InterShopAPI.DTO
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Mail { get; set; } // Почта
        public string? Password { get; set; } // Хеш пароля
        public string? RoleName { get; set; }
        public bool? InstanseMail { get; set; } 
        public bool? IsDeleted { get; set; } 
    }
}