using IngredientDAL.Models;
using System.Data.Entity;

namespace IngredientDAL.DAL
{
    public class FakeIngredientContext : IIngredientContext
    {
        /**
         * This is our Fake Database. As you can see, we private set our
         * database models. 
         * */
        public IDbSet<Ingredient> Ingredients { get; private set; }
        public IDbSet<Product> Products { get; private set; }
        public IDbSet<ReceiptItem> ReceiptItems { get; private set; }
        public IDbSet<RecipeItem> RecipeItems { get; private set; }
        public IDbSet<Recipe> Recipes { get; private set; }
        public IDbSet<Step> Steps { get; private set; }
        public IDbSet<Refrigerator> Refrigerator { get; private set; }
        public IDbSet<RefrigeratedProduct> ProductsInFridge { get; private set; }
 

        public FakeIngredientContext()
        {
            Ingredients = new FakeIngredientSet();
            Products = new FakeProductSet();
            ReceiptItems = new FakeReceiptItemSet();
            Recipes = new FakeRecipeSet();
            RecipeItems = new FakeRecipeItemSet();
            Steps = new FakeStepSet();
            Refrigerator = new FakeRefrigeratorSet();
            ProductsInFridge = new FakeRefrigeratedProductSet();
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
