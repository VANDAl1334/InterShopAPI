using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterShopAPI.DTO
{
    public class UserMinimalDTO
    {
        
        public string Login { get; set; }
        public string? Mail { get; set; } // Почта
        public bool InstanseMail { get; set; } 
    }
}
