using IngredientDAL.Models;
using System.Data.Entity;

namespace IngredientDAL.DAL
{
    public interface IIngredientContext
    {
        /**
         * This interface is to allow me to send a FakeIngredientContext, used
         * for testing, to my DALBot, which is used to control the Database
         * actions.
         */
        IDbSet<Ingredient> Ingredients { get; }
        IDbSet<ReceiptItem> ReceiptItems { get; }
        IDbSet<Product> Products { get; }
        IDbSet<RecipeItem> RecipeItems { get; }
        IDbSet<Step> Steps { get; }
        IDbSet<Recipe> Recipes { get; }
        IDbSet<Refrigerator> Refrigerator { get; }
        IDbSet<RefrigeratedProduct> ProductsInFridge { get; }

        int SaveChanges();
    }
}
