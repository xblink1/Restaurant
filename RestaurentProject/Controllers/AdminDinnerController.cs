using FoodStore.DTOs;
using FoodStore.DTOs.AdminBreakFastDTO;
using FoodStore.DTOs.AdminDinerDTO;
using FoodStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RestaurentProject.Controllers
{
    public class AdminDinnerController : Controller
    {
        private readonly IAdminRepository adminRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminDinnerController(IAdminRepository adminRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.adminRepository = adminRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult GetDish()
        {
           var dish= adminRepository.GetDinnerDish();
            return View(dish);
        }
        [HttpGet]
        public IActionResult AddDish()
        {          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDish(AddDinnerDTO addDinnerDTO)
        {
            if (addDinnerDTO.IMAGE != null && addDinnerDTO.IMAGE.Length > 0)
            {
                // Determine the file path and ensure it exists
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DinnerDishImg");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(addDinnerDTO.IMAGE.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save the file synchronously
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    addDinnerDTO.IMAGE.CopyTo(stream); // Synchronously copy the file
                }

                addDinnerDTO.ImagePath = $"/DinnerDishImg/{fileName}"; // Set image path in DTO
            }

            // Add the breakfast dish
            adminRepository.AddDinnerDish(addDinnerDTO);

            return RedirectToAction("AdminIndex", "Admin");
        }
        [HttpGet]
        public IActionResult UpdateDinner(int id)
        {
            var dish = adminRepository.GetDinnerDish().Find(model => model.ID == id);
            return View(dish);
        }
        [HttpPost]
        public IActionResult UpdateDinner(UpdateDinnerDTO updateDinnerDTO)
        {
            adminRepository.UpdateDinnerDish(updateDinnerDTO);
            return RedirectToAction("GetDish", "AdminDinner");
        }
        [HttpGet]
        public IActionResult DeleteDinner(int id)
        {
            var dish = adminRepository.GetDinnerDish().Find(model => model.ID == id);
            return View(dish);
        }
        [HttpPost]
        public IActionResult DeleteDinner(GetDinnerDTO getDinnerDTO )
        {
            adminRepository.DeleteDinnerDish(getDinnerDTO);
            return RedirectToAction("GetDish", "AdminDinner");
        }
    }
}
