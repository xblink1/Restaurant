using FoodStore.DTOs.AdminBreakFastDTO;
using FoodStore.DTOs.AdminDinerDTO;
using FoodStore.DTOs.AdminLunchDTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IConfiguration _configuration;
        public AdminRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SqlConnection()
        {
            return _configuration.GetConnectionString("MyConnectionCS").ToString();
        }
        #region -- Break fast Dish Crud --

        public List<GetBreakfastDishDTO> GetBreakfastDish()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetBreakFastDishSP", con))
                {
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
                            Price =  (dr["Price"]).ToString(),
                            Description = dr["Description"].ToString(),
                            DishImage = dr["DishImage"].ToString(),
                            
                        });
                    }
                    con.Close();
                    return BreakFastDishList;
                }
            }
        }

		public void AddBreakfastDish(AddBreakFastDishDTO addBreakFastDishDTO)
		{
			using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AddBreakFastDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@DishName", addBreakFastDishDTO.DishName);
                    cmd.Parameters.AddWithValue("@Price", addBreakFastDishDTO.Price);
                    cmd.Parameters.AddWithValue("@Description", addBreakFastDishDTO.Description);
                    cmd.Parameters.AddWithValue("@ImagePath", addBreakFastDishDTO.ImagePath);


                    // Open the connection, execute the command, and close the connection automatically
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UpdateBreakfastDish(UpdateBreakFastDishDTO updateBreakFastDishDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateBreakFastSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", updateBreakFastDishDTO.ID);
                    cmd.Parameters.AddWithValue("@DishName", updateBreakFastDishDTO.DishName);
                    cmd.Parameters.AddWithValue("@Price", updateBreakFastDishDTO.Price);
                    cmd.Parameters.AddWithValue("@Description", updateBreakFastDishDTO.Description);
                    cmd.Parameters.AddWithValue("@DishImage", updateBreakFastDishDTO.DishImage);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBreakfastDish(GetBreakfastDishDTO Delete)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteBreakfastSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", Delete.ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        #endregion

        #region --- Dinner Dish DTO ----
        public List<GetDinnerDTO> GetDinnerDish()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetDinnerDishSP", con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    adapter.Fill(dt);
                    List<GetDinnerDTO> GetDinnerDTOList = new List<GetDinnerDTO>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        GetDinnerDTOList.Add(new GetDinnerDTO
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            DishName = dr["DishName"].ToString(),
                            Price = (dr["Price"]).ToString(),
                            Description = dr["Description"].ToString(),
                            DishImage = dr["DishImage"].ToString(),

                        });
                    }
                    con.Close();
                    return GetDinnerDTOList;
                }
            }
        }

        public void AddDinnerDish(AddDinnerDTO addDinnerDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AddDinnerDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@DishName", addDinnerDTO.DishName);
                    cmd.Parameters.AddWithValue("@Price", addDinnerDTO.Price);
                    cmd.Parameters.AddWithValue("@Description", addDinnerDTO.Description);
                    cmd.Parameters.AddWithValue("@DishImage", addDinnerDTO.ImagePath);


                    // Open the connection, execute the command, and close the connection automatically
                    con.Open();
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UpdateDinnerDish(UpdateDinnerDTO updateDinnerDTO )
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateDinnerDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", updateDinnerDTO.ID);
                    cmd.Parameters.AddWithValue("@DishName", updateDinnerDTO.DishName);
                    cmd.Parameters.AddWithValue("@Price", updateDinnerDTO.Price);
                    cmd.Parameters.AddWithValue("@Description", updateDinnerDTO.Description);
                    cmd.Parameters.AddWithValue("@DishImage", updateDinnerDTO.DishImage);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDinnerDish(GetDinnerDTO getDinnerDTO )
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteDinnerDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", getDinnerDTO.ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        #endregion

        #region --- Lunch Dish ---
        public List<GetLunchDTO> GetLunch()
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetlunchDishSP", con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    adapter.Fill(dt);
                    List<GetLunchDTO> lunchlist = new List<GetLunchDTO>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        lunchlist.Add(new GetLunchDTO
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            DishName = dr["DishName"].ToString(),
                            Price = (dr["Price"]).ToString(),
                            Description = dr["Description"].ToString(),
                            DishImage = dr["DishImage"].ToString(),

                        });
                    }
                    con.Close();
                    return lunchlist;
                }
            }
        }

        public void AddLunchDish(AddLunchDTO addLunchDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AddLunchSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the stored procedure
                    cmd.Parameters.AddWithValue("@DishName", addLunchDTO.DishName);
                    cmd.Parameters.AddWithValue("@Price", addLunchDTO.Price);
                    cmd.Parameters.AddWithValue("@Description", addLunchDTO.Description);
                    cmd.Parameters.AddWithValue("@ImagePath", addLunchDTO.ImagePath);


                    // Open the connection, execute the command, and close the connection automatically
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLunchDish(UpdateLunchDTO updateLunch)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateLunchDishSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", updateLunch.ID);
                    cmd.Parameters.AddWithValue("@DishName", updateLunch.DishName);
                    cmd.Parameters.AddWithValue("@Price", updateLunch.Price);
                    cmd.Parameters.AddWithValue("@Description", updateLunch.Description);
                    cmd.Parameters.AddWithValue("@DishImage", updateLunch.DishImage);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteLunchDish(GetLunchDTO getLunchDTO)
        {
            using (SqlConnection con = new SqlConnection(this.SqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeletLunchSP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", getLunchDTO.ID);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        #endregion
    }
}
