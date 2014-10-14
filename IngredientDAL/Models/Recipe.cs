using System;
using System.Collections.Generic;

namespace IngredientDAL.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }

        public ICollection<Step> Steps { get; set; }

        public List<RecipeItem> GetAllRecipeIngredients()
        {
            var list = new List<RecipeItem>();

            foreach (var step in Steps)
            {
                foreach (var item in step.RecipeItems)
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }
}
