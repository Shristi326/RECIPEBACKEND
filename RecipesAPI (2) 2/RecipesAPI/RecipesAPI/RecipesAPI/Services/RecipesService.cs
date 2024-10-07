using RecipesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesAPI.Services
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipeById(int id);
        Task AddRecipe(Recipe recipe);
        Task<List<Recipe>> GetRecipesByCategory(int categoryId);
    }
}
