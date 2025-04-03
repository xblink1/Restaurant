using FoodStore.DTOs.AdminBreakFastDTO;
using FoodStore.DTOs.AdminLunchDTO;
using FoodStore.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace RestaurentProject.Controllers
{
    public class AdminLunchController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminLunchController(IAdminRepository adminRepository, IWebHostEnvironment webHostEnvironment)
        {
            _adminRepository = adminRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult GetLunchDish()
        {
            var lunch =_adminRepository.GetLunch();
            return View(lunch);
        }
        [HttpGet]
        public IActionResult AddLunchDish()
        {       
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddLunchDish(AddLunchDTO addLunchDTO)
        {
            if (addLunchDTO.IMAGE != null && addLunchDTO.IMAGE.Length > 0)
            {
                // Determine the file path and ensure it exists
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/LunchDishImg");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(addLunchDTO.IMAGE.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save the file synchronously
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    addLunchDTO.IMAGE.CopyTo(stream); // Synchronously copy the file
                }

                addLunchDTO.ImagePath = $"/LunchDishImg/{fileName}"; // Set image path in DTO
            }

            // Add the breakfast dish
            _adminRepository.AddLunchDish(addLunchDTO);

            return RedirectToAction("AdminIndex", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateLunch(int id)
        {
            var obj1 = _adminRepository.GetLunch().FirstOrDefault(x=> x.ID == id);
            return View(obj1);
        }
        [HttpPost]
        public IActionResult UpdateLunch(UpdateLunchDTO updateLunch)
        {
            _adminRepository.UpdateLunchDish(updateLunch);
            return RedirectToAction("GetLunchDish", "AdminLunch");
        }
        [HttpGet]
        public IActionResult DeleteLunch(int id)
        {
            var dish = _adminRepository.GetLunch().Find(model => model.ID == id);
            return View(dish);
        }
        [HttpPost]
        public IActionResult DeleteLunch(GetLunchDTO getLunchDTO)
        {
            _adminRepository.DeleteLunchDish(getLunchDTO);
            return RedirectToAction("GetLunchDish", "AdminLunch");
        }
    }
}
