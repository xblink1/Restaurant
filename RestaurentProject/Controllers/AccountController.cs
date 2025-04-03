using FoodStore.DTOs;
using FoodStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace RestaurentProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepository repository;
        public AccountController(IRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var CheckEmailExist = repository.EMAIL(registerDTO.Email, 0, "INSERT");          
            if (CheckEmailExist == true)
            {
                ViewBag.ErrorMessage = "This Email is Already in use.";
                return View(registerDTO);
            }
            var Checkuser = repository.CheckUser(registerDTO.UserName);
            if (Checkuser == true)
            {
                ViewBag.UserError = "This User Name is Already Taken.";
                return View(registerDTO);
            }
            else
            {
                repository.SignUp(registerDTO);
                string subject = "New Car Added";
                string body = $"Dear{registerDTO.Email},<br>" +
                           "Your Car Was Successfully Added.<br> " +
                           "$Car: { insert.CarName}<br>" +
                "$Price : {insert.Carprice}<br>" +
                "Thank You !!";

                repository.SendEMAIL(registerDTO.Email, subject, body);

                TempData["InsertMessege"] = "SignUp Successfully";
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string EmailOrUserName, string password)
        {
           
            var result = repository.LoginValidate(EmailOrUserName, password);
            
            if (result.IsAdmin)
            {
                return RedirectToAction("AdminIndex", "Admin");
            }
            else if (result.IsValid)
            {
                HttpContext.Session.SetString("UserSession", EmailOrUserName);

                int CartCount = repository.GetCartCount(EmailOrUserName);
                HttpContext.Session.SetInt32("CartCount", CartCount);

                return RedirectToAction("Index", "Home");
            }
         

            // Invalid Credentials
            ViewBag.Message = "Invalid username or password.";
            return View();
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            // Log current session state for debugging
            var cartCount = HttpContext.Session.GetInt32("CartCount");
            var userSession = HttpContext.Session.GetString("UserSession");

            // Clear all session data
            HttpContext.Session.Clear();

            // Redirect to the login page or home page
            return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
        }

    }
}

