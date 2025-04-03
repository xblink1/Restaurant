using FoodStore.DTOs.AdminBreakFastDTO;
using FoodStore.Repository;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace RestaurentProject.Controllers
{
    public class AdminBreakfastDish : Controller
	{
		private readonly IAdminRepository _adminRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminBreakfastDish(IAdminRepository adminRepository, IWebHostEnvironment webHostEnvironment)
		{
            _webHostEnvironment = webHostEnvironment;
			_adminRepository = adminRepository;
        }
		public IActionResult GetDish()
		{
			var dish = _adminRepository.GetBreakfastDish();
			return View(dish);
		}
		[HttpGet]
		public IActionResult AddBreakfastDish()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> AddBreakfastDish(AddBreakFastDishDTO addBreakFastDishDTO)
        {
            if (addBreakFastDishDTO.IMAGE != null && addBreakFastDishDTO.IMAGE.Length > 0)
            {
                // Determine the file path and ensure it exists
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/BreakFastImages");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(addBreakFastDishDTO.IMAGE.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save the file synchronously
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    addBreakFastDishDTO.IMAGE.CopyTo(stream); // Synchronously copy the file
                }

                addBreakFastDishDTO.ImagePath = $"/BreakFastImages/{fileName}"; // Set image path in DTO
            }

            // Add the breakfast dish
            _adminRepository.AddBreakfastDish(addBreakFastDishDTO);

            return RedirectToAction("AdminIndex", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateBreakfast(int id)
        {
			var dish = _adminRepository.GetBreakfastDish().Find(model => model.ID == id);
            return View(dish);
        }
        [HttpPost]
        public IActionResult UpdateBreakfast(UpdateBreakFastDishDTO updateBreakFastDishDTO)
        {
			_adminRepository.UpdateBreakfastDish(updateBreakFastDishDTO);
            return RedirectToAction("GetDish", "AdminBreakfastDish");
        }
        [HttpGet]
        public IActionResult DeleteBreakFast(int id)
        {
            var dish = _adminRepository.GetBreakfastDish().Find(model => model.ID == id);
            return View(dish);
        }
        [HttpPost]
        public IActionResult DeleteBreakFast(GetBreakfastDishDTO breakfastDishDTO)
        {
            _adminRepository.DeleteBreakfastDish(breakfastDishDTO);
            return RedirectToAction("GetDish", "AdminBreakfastDish");
        }
    }
}
