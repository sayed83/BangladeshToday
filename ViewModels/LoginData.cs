using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangladeshToday.ViewModels
{
    public class LoginData
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string RequestPath { get; set; }
    }
}
