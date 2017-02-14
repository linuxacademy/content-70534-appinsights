using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Mvc;
using la70534ai.Models;

namespace la70534ai.Controllers
{
    public class HomeController : Controller
    {
        private const string ConnString = "ENTER YOUR AZURE SQL DB CONNECTION STRING HERE";
        
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Products()
        {
            var model = new List<StoreProduct>();
            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("StoreProductsReadAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows) return View(model);
                        while (await reader.ReadAsync())
                        {
                            var tmp = new StoreProduct
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("ItemName")),
                                Description = reader.GetString(reader.GetOrdinal("ItemDescription")),
                                Color = reader.GetString(reader.GetOrdinal("ItemColor")),
                                Size = reader.GetString(reader.GetOrdinal("ItemSize")),
                                AgeRestricted = reader.GetBoolean(reader.GetOrdinal("ItemAgeRestricted"))
                            };
                            model.Add(tmp);
                        }
                    }
                }
            }

            return View(model);
        }

        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            var model = new StoreProduct();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(StoreProduct model)
        {
            if (!ModelState.IsValid) return View(model);
            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();
                using (var cmd = new SqlCommand("StoreProductsInsert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ItemName", SqlDbType.NVarChar, 255)).Value = model.Name;
                    cmd.Parameters.Add(new SqlParameter("ItemDescription", SqlDbType.NVarChar, 3000)).Value = model.Description;
                    cmd.Parameters.Add(new SqlParameter("ItemColor", SqlDbType.NVarChar, 10)).Value = model.Color;
                    cmd.Parameters.Add(new SqlParameter("ItemSize", SqlDbType.NVarChar, 10)).Value = model.Size;
                    cmd.Parameters.Add(new SqlParameter("ItemAgeRestricted", SqlDbType.Bit)).Value = model.AgeRestricted;

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return RedirectToAction("Products");
        }

        public ActionResult Throw500Error()
        {
            throw new Exception();
        }
    }
}
