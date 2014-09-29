using System.Collections.Generic;

namespace IngredientDAL.Models
{
    public class Step
    {
        public int StepId { get; set; }
        public int RecipeId { get; set; }
        public int StepNum { get; set; }
        public string StepInstructions { get; set; }

        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
