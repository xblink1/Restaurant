using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DTOs
{
    public class BookTableDTO
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string DateNTime { get; set; }
        public int NumberOfGuest { get; set; }
        public string SpecialRequest { get; set; }


    }
}
