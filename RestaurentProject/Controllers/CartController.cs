using FoodStore.DTOs;
using FoodStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Build.Execution;


namespace RestaurentProject.Controllers
{

    public class CartController : Controller
    {
        private readonly IConfiguration _config;
        public CartController(IConfiguration configuration)
        {
            _config = configuration;
        }
        private string SqlConnection()
        {
            return _config.GetConnectionString("MyConnectionCS").ToString();
        }
        [HttpPost]
        public IActionResult AddToCart(string productName, decimal price, int quantity, string productImage, string userName)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return Json(new { success = false, message = "Please login first to access this page." });
            }

            using (SqlConnection conn = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("CartSp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", productName);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@ProductImage", productImage);
                cmd.Parameters.AddWithValue("@UserName", userName);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Update session cart count
            int? cartCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            cartCount += quantity;
            HttpContext.Session.SetInt32("CartCount", (int)cartCount);

            return RedirectToAction("Menu", "Menu");
        }


        public IActionResult Cart()
        {
            List<CartDTO> cartItems = new List<CartDTO>();

            decimal totalPrice = 0;
            using (SqlConnection conn = new SqlConnection(this.SqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ShoppingCart WHERE UserName = @UserName", conn);
                string username = HttpContext.Session.GetString("UserSession");
                cmd.Parameters.AddWithValue("@UserName", username);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                     var item = new CartDTO
                     {
                        CartID = (int)reader["CartID"],
                        ProductName = reader["ProductName"].ToString(),
                        Price = (decimal)reader["Price"],
                        Quantity = (int)reader["Quantity"],
                        ProductImage = reader["ProductImage"].ToString()
                    };
                    cartItems.Add(item);

                    totalPrice += item.Price * item.Quantity;
                   
                }
                conn.Close();
            }
            ViewBag.TotalPrice = totalPrice;
            return View(cartItems);
        }
        public IActionResult DeleteSItem(int id)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteSItemFCart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            //// Update the cart count in the session (decrement by 1)
            int? cartCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            cartCount = (cartCount > 0) ? cartCount - 1 : 0;
            HttpContext.Session.SetInt32("CartCount", (int)cartCount);

            // Return JSON response
            return RedirectToAction("Cart","Cart");
        }


        public IActionResult DeleteAllItemFCart()
        {
            string username = HttpContext.Session.GetString("UserSession");
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteAllItemFCart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", username);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            int? cartcount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            return Json(new { success = true });
        }
    }
}

