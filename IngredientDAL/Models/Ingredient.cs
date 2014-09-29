using System;
using System.Collections.Generic;

namespace IngredientDAL.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String IngredientName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
