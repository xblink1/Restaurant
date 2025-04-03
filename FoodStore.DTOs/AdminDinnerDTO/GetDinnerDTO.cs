﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DTOs.AdminDinerDTO
{
    public class GetDinnerDTO
    {
        public int ID { get; set; }
        public string DishName { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string DishImage { get; set; }
    }
}
