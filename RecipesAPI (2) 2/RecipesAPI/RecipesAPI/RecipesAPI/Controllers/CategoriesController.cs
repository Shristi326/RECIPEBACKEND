using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Data;
using RecipesAPI.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient; // Ensure you have this using directive

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public CategoriesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = new List<Category>();

            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Categories";

                using (var command = new MySqlCommand(query, (MySqlConnection)connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return Ok(categories);
        }
    }
}
