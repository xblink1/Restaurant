using FoodStore.DTOs;
using FoodStore.Repository;
using Microsoft.AspNetCore.Mvc;
using RestaurentProject.Models;
using System.Diagnostics;

namespace RestaurentProject.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult About() 
        {
            return View();
        }
        [HttpGet]   
        public IActionResult BookTable()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BookTable(BookTableDTO bookTableDTO)
        {
            _repository.BookTable(bookTableDTO);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Cart() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cart(CartDTO cartDTO)
        {
            _repository.AddProdectToCart(cartDTO);
            return View();
        }
    }
}
