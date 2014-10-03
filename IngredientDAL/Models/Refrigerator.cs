using System.Collections.Generic;

namespace IngredientDAL.Models
{
    public class Refrigerator
    {
        public int RefrigeratorId { get; set; }

        public virtual ICollection<RefrigeratedProduct> ProductsInFridge 
        { get; set; }

        public Refrigerator()
        {
            ProductsInFridge = new List<RefrigeratedProduct>();
        }
    }
}
