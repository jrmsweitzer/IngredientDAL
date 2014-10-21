using System.Collections.Generic;

namespace IngredientDAL.Models
{
    public class Product
    {
        public int ProductId {get; set;}
        public int IngredientId { get; set; }
        public string BrandName { get; set; }
        public double ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string ProductReceiptText { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; }

    }
}
