namespace RecipesAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public required string ? CategoryName { get; set; } // Use 'required' or make it nullable
    }
}
