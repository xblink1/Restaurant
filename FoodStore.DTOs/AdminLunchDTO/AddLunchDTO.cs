using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DTOs.AdminLunchDTO
{
    public class AddLunchDTO
    {
        public string DishName { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required]
        [Display(Name = "Uplaod Image")]
        public IFormFile IMAGE { get; set; }
    }
}
