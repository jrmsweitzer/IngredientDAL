namespace IngredientDAL.Models
{
    public class RecipeItem
    {
        //An amount of a given ingredient, for a step in a recipe
        //
        //TWO OUNCES 
        public int RecipeItemId { get; set; }
        public int StepId { get; set; }
        public int IngredientId { get; set; }
        public double IngredientQuantity { get; set; }
        public string IngredientUnit { get; set; }

        public virtual Step Step { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
