using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Enter Your Name Or Email")]
        [Display(Name = "UserName or Email")]
        public string EmailOrUserName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
