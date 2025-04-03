using FoodStore.DTOs;
using FoodStore.DTOs.AdminBreakFastDTO;
using FoodStore.DTOs.AdminDinerDTO;
using FoodStore.DTOs.AdminLunchDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Repository
{
    public interface IRepository
    {
        public void SignUp(RegisterDTO registerDTO);
        public bool CheckUser(string userName);

        public (bool IsValid, bool IsAdmin) LoginValidate(string EmailOrUserName, string password);

        public void SendEMAIL(string address, string subject, string body);

        public bool EMAIL(string Email, int ID, string Type);
        public List<GetBreakfastDishDTO> GetBreakfastDish();
        public List<GetDinnerDTO> GetDinnerDish();

        public List<GetLunchDTO> GetLunchDish();

        public void BookTable (BookTableDTO bookTableDTO);

        public void AddProdectToCart(CartDTO cartDTO);
        public int GetCartCount(string username);


    }
}
