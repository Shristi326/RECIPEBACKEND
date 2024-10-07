using RecipesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesAPI.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
    }
}
