using System.Collections.Generic;

namespace IngredientDAL.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }

        public ICollection<Step> Steps { get; set; }
    }
}
