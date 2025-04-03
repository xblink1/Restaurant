using FoodStore.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using FoodStore.DTOs.AdminBreakFastDTO;
using FoodStore.DTOs.AdminDinerDTO;
using FoodStore.DTOs.AdminLunchDTO;
using System.Reflection;
using System.Web.Http.ModelBinding;

namespace FoodStore.Repository
{
    public class Repository : IRepository
    {
        private readonly IConfiguration _config;
        public Repository(IConfiguration configuration)
        {
            _config = configuration;
        }
        private string SqlConnection()
        {
            return _config.GetConnectionString("MyConnectionCS").ToString();
        }

        #region ---- Registeration Logic ----
        public void SignUp(RegisterDTO registerDTO)
        {
            // Using statement to ensure SqlConnection is properly disposed of
            using (SqlConnection connection = new SqlConnection(this.SqlConnection()))
            {
                // SqlCommand is also wrapped in a using statement to ensure proper disposal
                using (SqlCommand cmd = new SqlCommand("SignUpSP", connection))
                {
                    // Set the command type to StoredProcedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@UserName", registerDTO.UserName);
                    cmd.Parameters.AddWithValue("@Email", registerDTO.Email);
                    cmd.Parameters.AddWithValue("@MobileNumber", registerDTO.MobileNum);
                    cmd.Parameters.AddWithValue("@Address", registerDTO.Address);
                    cmd.Parameters.AddWithValue("@Password", registerDTO.Password);

                    // Open the connection, execute the command, and close the connection automatically
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
        public bool CheckUser(string userName)
        {
            using (SqlConnection connection = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CheckUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    connection.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null; // Returns true if username exists
                }
            }
        }

        #region ---- Login Validation ---
        public (bool IsValid, bool IsAdmin) LoginValidate(string EmailOrUserName, string password)
        {
            bool isValid = false;
            bool isAdmin = false;

            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("MyConnectionCS")))
            //using (SqlConnection connection = new SqlConnection("Server=DESKTOP-T4LB3IA\\SQLEXPRESS;Database=BurgerShop;Trusted_Connection=True;TrustServerCertificate=true"))
            {
                connection.Open();

                // First, check in the Admin table
                using (SqlCommand cmd = new SqlCommand("AdminLoginValidate", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", (object)EmailOrUserName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Password", (object)password ?? DBNull.Value);

                    SqlParameter outputParam = new SqlParameter
                    {
                        ParameterName = "@IsValid",
                        SqlDbType = SqlDbType.Bit,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    isAdmin = (bool)outputParam.Value;
                    if (isAdmin)
                    {
                        // If admin credentials are valid, return immediately
                        return (true, true);
                    }
                }

                // If not an admin, check in the User table
                using (SqlCommand cmd = new SqlCommand("LoginValidate", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", (object)EmailOrUserName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);  // You may handle email separately if needed
                    cmd.Parameters.AddWithValue("@Password", (object)password ?? DBNull.Value);

                    SqlParameter outputParam = new SqlParameter
                    {
                        ParameterName = "@IsValid",
                        SqlDbType = SqlDbType.Bit,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    isValid = (bool)outputParam.Value;
                }
            }

            return (isValid, false);
        }


        #endregion

        #region ---- Sending Email Logic ----

        public void SendEMAIL(string address, string subject, string body)
        {
            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress("corporatehuntofficial@gmail.com");
                mm.To.Add(address);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential("corporatehuntofficial@gmail.com", "tsjs nnlw kpim noqo");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = nc;
                    smtp.Port = 587;

                    smtp.Send(mm);
                }

            }

        }
        #endregion

        #region ---- Email Validation ----

        public bool EMAIL(string Email, int ID, string Type)
        {
            using (SqlConnection connection = new SqlConnection(this.SqlConnection()))
            {
                // SqlCommand is also wrapped in a using statement to ensure proper disposal
                using (SqlCommand cmd = new SqlCommand("CheckMail", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EMAIL", Email);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Type", Type);
                    //SqlCommand cmd = new SqlCommand("SELECT * FROM BOOKS where Email='" + EmailId + "' and ID <> "+id, con);
                    connection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }



        #endregion

        #region ---- Get Break Fast Dishes ----
        public List<GetBreakfastDishDTO> GetBreakfastDish()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetBreakFastDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    adapter.Fill(dt);
                    List<GetBreakfastDishDTO> BreakFastDishList = new List<GetBreakfastDishDTO>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        BreakFastDishList.Add(new GetBreakfastDishDTO
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            DishName = dr["DishName"].ToString(),
                            Price = (dr["Price"]).ToString(),
                            Description = dr["Description"].ToString(),
                            DishImage = dr["DishImage"].ToString(),

                        });
                    }
                    con.Close();
                    return BreakFastDishList;
                }
            }
        }


        #endregion

        #region ---- Get Dinner Dishes ----
        public List<GetDinnerDTO> GetDinnerDish()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetDinnerDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    adapter.Fill(dt);
                    List<GetDinnerDTO> DinnerList = new List<GetDinnerDTO>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DinnerList.Add(new GetDinnerDTO
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            DishName = dr["DishName"].ToString(),
                            Price = (dr["Price"]).ToString(),
                            Description = dr["Description"].ToString(),
                            DishImage = dr["DishImage"].ToString(),

                        });
                    }
                    con.Close();
                    return DinnerList;
                }
            }
        }


        #endregion

        #region ---- Get Lunch Dishes ----
        public List<GetLunchDTO> GetLunchDish()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetlunchDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    adapter.Fill(dt);
                    List<GetLunchDTO> DinnerList = new List<GetLunchDTO>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        DinnerList.Add(new GetLunchDTO
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            DishName = dr["DishName"].ToString(),
                            Price = (dr["Price"]).ToString(),
                            Description = dr["Description"].ToString(),
                            DishImage = dr["DishImage"].ToString(),

                        });
                    }
                    con.Close();
                    return DinnerList;
                }
            }
        }
        #endregion

        #region -- Book A Table ----
        public void BookTable(BookTableDTO bookTableDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("TableBookingSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerName", bookTableDTO.CustomerName);
                    cmd.Parameters.AddWithValue("@DateNTime", bookTableDTO.DateNTime);
                    cmd.Parameters.AddWithValue("@NoOFGuest", bookTableDTO.NumberOfGuest);
                    cmd.Parameters.AddWithValue("@SpeicalRequest", bookTableDTO.SpecialRequest);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region -- Add to cart ---
        public void AddProdectToCart(CartDTO cartDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("CartSp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductName", cartDTO.ProductName);
                    cmd.Parameters.AddWithValue("@Price", cartDTO.Price);
                    cmd.Parameters.AddWithValue("@Quantity", cartDTO.Quantity);
                    cmd.Parameters.AddWithValue("@ProductImage", cartDTO.ProductImage);
                    cmd.Parameters.AddWithValue("@UserName", cartDTO.UserName);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region --- Get Count If User Existing ---
        public int GetCartCount(string username)
        {
            int count = 0;

            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ShoppingCart WHERE UserName = @UserName", con))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);

                    con.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }

            return count;
        }
        #endregion

        #region -- Registration Validation If UserName Already Exists ---

        public void Checkuser(string userName)
        {
            using (SqlConnection connection = new SqlConnection(this.SqlConnection()))
            {
                // SqlCommand is also wrapped in a using statement to ensure proper disposal
                using (SqlCommand cmd = new SqlCommand("CheckUser", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", userName);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

    }
}

