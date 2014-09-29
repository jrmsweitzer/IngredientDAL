using System;

namespace IngredientDAL.Models
{
    public class RefrigeratedProduct
    {
        public int RefrigeratedProductId { get; set; }
        public int RefrigeratorId { get; set; }
        public int ProductId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double QuantityLeft { get; set; }
        public string UnitsLeft { get; set; }

        public virtual Refrigerator Refrigerator { get; set; }
        public virtual Product Product { get; set; }
    }
}
