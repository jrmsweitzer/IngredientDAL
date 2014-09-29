using System;

namespace IngredientDAL.Models
{
    public class ReceiptItem
    {
        public int ReceiptItemId { get; set; }
        public String StoreName { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int ProductId { get; set; }
        public double IngredientPrice { get; set; }

        public virtual Product Product { get; set; }
    }
}
