using FoodStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace RestaurentProject.Controllers
{
    public class MenuController : Controller
    {
        private readonly IRepository repository;

        public MenuController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Menu()
        {
            var dishes = repository.GetBreakfastDish();
            return View(dishes);
        }
        [HttpGet]
        public IActionResult Dinner()
        {
            var dishes = repository.GetDinnerDish();
            return View(dishes);
            
        }
        [HttpGet]
        public IActionResult Lunch()
        {
            var dishes = repository.GetLunchDish();
            return View(dishes);

        }
    }
}
