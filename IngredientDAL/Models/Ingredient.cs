using System;
using System.Collections.Generic;

namespace IngredientDAL.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String IngredientName { get; set; }
        public double ServingSizeQuantity { get; set; }
        public string ServingSizeUnits { get; set; }
        public double CaloriesPerServing { get; set; }
        public double FatPerServing { get; set; }
        public double CholesterolPerServing { get; set; }
        public double SodiumPerServing { get; set; }
        public double PotassiumPerServing { get; set; }
        public double CarbohydratesPerServing { get; set; }
        public double SugarsPerServing { get; set; }
        public double ProteinPerServing { get; set; }
        public bool HasFoundNutrients { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}
