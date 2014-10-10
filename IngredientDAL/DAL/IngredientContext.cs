using IngredientDAL.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace IngredientDAL.DAL
{
    public class IngredientContext : DbContext, IIngredientContext
    {
        public IngredientContext()
            : base("DefaultConnection") {
        }
        //Database Models
        public IDbSet<Ingredient> Ingredients { get; set; }
        public IDbSet<ReceiptItem> ReceiptItems { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<RecipeItem> RecipeItems { get; set; }
        public IDbSet<Step> Steps { get; set; }
        public IDbSet<Recipe> Recipes { get; set; }
        public IDbSet<Refrigerator> Refrigerator { get; set; }
        public IDbSet<RefrigeratedProduct> ProductsInFridge { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
