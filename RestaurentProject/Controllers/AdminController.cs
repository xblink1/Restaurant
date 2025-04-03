
using FoodStore.DTOs;
using FoodStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurentProject.Controllers
{
    public class AdminController : Controller
    {
      

		public IActionResult AdminIndex()
        {
           
            return View();
        }

        //[HttpGet]
        //public IActionResult AddBreakFastDish()
        //{
            
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddBreakFastDish(BreakFastDishDTO breakFastDishDTO, IFormFile IMAGE)
        //{
        //    if (IMAGE != null && IMAGE.Length > 0)
        //    {
        //        // Determine the file path and ensure it exists
        //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductsImg");
        //        Directory.CreateDirectory(uploadsFolder);

        //        var fileName = Path.GetFileName(IMAGE.FileName);
        //        var filePath = Path.Combine(uploadsFolder, fileName);

        //        // Save the file synchronously
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            IMAGE.CopyTo(stream); // Synchronously copy the file
        //        }

        //        breakFastDishDTO.DishImage = $"/ProductsImg/{fileName}"; // Set image path in DTO
        //    }

        //    _repository.AddBreakFastDish(breakFastDishDTO);
        //    return RedirectToAction("AdminIndex", "Admin");
        //}

        //[HttpGet]
        //public IActionResult AdminDelete(int id)
        //{
        //    var row = _repository.GetBreakfastDish().Find(model => model.ID == id);
        //    return View(row);
        //}
        //[HttpPost]
        //public IActionResult AdminDelete(BreakFastDishUpdateDTO breakFastDishUpdateDTO)
        //{
        //    _repository.DeleteBreakFastDish(breakFastDishUpdateDTO);
        //    return View();
        //}
    }
}
