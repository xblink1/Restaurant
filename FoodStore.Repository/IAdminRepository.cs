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
    public interface IAdminRepository
    {
		#region --- Breakfast Dish ----
		public List<GetBreakfastDishDTO> GetBreakfastDish();

		public void AddBreakfastDish(AddBreakFastDishDTO addBreakFastDishDTO);

		public void UpdateBreakfastDish(UpdateBreakFastDishDTO updateBreakFastDishDTO);

		public void DeleteBreakfastDish(GetBreakfastDishDTO breakfastDishDTO);
        #endregion

        #region --- Dinner Dish ----
        public List<GetDinnerDTO> GetDinnerDish();

        public void AddDinnerDish(AddDinnerDTO addDinnerDTO);

        public void UpdateDinnerDish(UpdateDinnerDTO updateDinnerDTO);

        public void DeleteDinnerDish(GetDinnerDTO getDinnerDTO);
        #endregion

        #region --- Lunch Dish ----
        public List<GetLunchDTO> GetLunch();

        public void AddLunchDish(AddLunchDTO addLunchDTO );

        public void UpdateLunchDish(UpdateLunchDTO updateLunch);

        public void DeleteLunchDish(GetLunchDTO getLunchDTO);
        #endregion

    }
}
