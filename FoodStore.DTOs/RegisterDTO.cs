using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Full Name")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Enter Your Email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email Format Is Not Correct")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Enter Your MobileNum")]
        [Display(Name = "Mobile Number")]
        public string MobileNum { get; set; }


        [Required(ErrorMessage = "Enter Your Mobile Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Enter Your Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
