using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Data;
using RecipesAPI.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient; // Change to MySqlConnector if needed

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public RecipesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetRecipes()
        {
            var recipes = new List<Recipe>();

            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Recipes";
                using (var command = new MySqlCommand(query, (MySqlConnection)connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recipes.Add(new Recipe
                            {
                                RecipeId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Ingredients = reader.GetString(2),
                                Instructions = reader.GetString(3),
                                CategoryId = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id)
        {
            Recipe recipe = null;

            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Recipes WHERE RecipeId = @RecipeId";
                using (var command = new MySqlCommand(query, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@RecipeId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipe = new Recipe
                            {
                                RecipeId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Ingredients = reader.GetString(2),
                                Instructions = reader.GetString(3),
                                CategoryId = reader.GetInt32(4)
                            };
                        }
                    }
                }
            }

            if (recipe == null)
                return NotFound();

            return Ok(recipe);
        }

        [HttpPost]
        public IActionResult AddRecipe([FromBody] Recipe recipe)
        {
            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                var query = "INSERT INTO Recipes (Title, Ingredients, Instructions, CategoryId) VALUES (@Title, @Ingredients, @Instructions, @CategoryId)";
                using (var command = new MySqlCommand(query, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@Title", recipe.Title);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Instructions", recipe.Instructions);
                    command.Parameters.AddWithValue("@CategoryId", recipe.CategoryId);
                    command.ExecuteNonQuery();
                }
            }

            return Ok();
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetRecipesByCategory(int categoryId)
        {
            var recipes = new List<Recipe>();

            using (var connection = _dbContext.CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Recipes WHERE CategoryId = @CategoryId";
                using (var command = new MySqlCommand(query, (MySqlConnection)connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recipes.Add(new Recipe
                            {
                                RecipeId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Ingredients = reader.GetString(2),
                                Instructions = reader.GetString(3),
                                CategoryId = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return Ok(recipes);
        }
    }
}
