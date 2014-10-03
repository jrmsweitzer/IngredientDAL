using System.Globalization;
using IngredientDAL.Controllers;
using IngredientDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace IngredientDAL.Bots
{
    public class SearchBot
    {
        internal List<Ingredient> SortIngredientsByIngredientName()
        {
            return RobotController.INGREDIENTS
                .OrderBy(i => i.IngredientName).ToList();
        }

        internal List<Product> SortProductsByIngredientName()
        {
            return RobotController.PRODUCTS
                .OrderBy(i => i.Ingredient.IngredientName).ToList();
        }

        internal List<Product> SortProductsByBrandName(List<Product> list)
        {
            return list.OrderBy(i => i.BrandName).ToList();
        }

        internal List<ReceiptItem> SortReceiptsByIngredientName()
        {
            return RobotController.RECEIPT
                .OrderBy(i => i.Product.Ingredient.IngredientName).ToList();
        }

        internal List<ReceiptItem> SortReceiptsByBrandName()
        {
            return RobotController.RECEIPT
                .OrderBy(i => i.Product.BrandName).ToList();
        }

        internal List<ReceiptItem> SortReceiptsByPrice()
        {
            return RobotController.RECEIPT
                .OrderBy(i => i.IngredientPrice).ToList();
        }

        internal Ingredient FindIngredientByName(string ingredientName)
        {
            return RobotController.INGREDIENTS.FirstOrDefault(i => 
                i.IngredientName.Equals(ingredientName));
        }

        internal Product FindProductByIngredientName(string ingredientName)
        {
            return RobotController.PRODUCTS.FirstOrDefault(i =>
                i.Ingredient.IngredientName.Equals(ingredientName));
        }

        internal Product FindProductByBrandName(string brandName)
        {
            return RobotController.PRODUCTS.FirstOrDefault(i =>
                i.BrandName.Equals(brandName));
        }

        internal List<Product> FilterProductsByBrandNameContaining(
            List<Product> products, string substring)
        {
            return products.Where(i =>
            i.BrandName.ToLower()
                .Contains(substring.ToLower())).ToList();
        }

        internal List<Product> FilterProductsByIngredientNameContaining(
            List<Product> products, string substring)
        {
            return products.Where(i =>
                i.Ingredient.IngredientName.ToLower().
                    Contains(substring.ToLower())).ToList();
        }

        internal List<Product> FilterProductsByQuantityContaining(
            List<Product> products, string substring)
        {
            return products.Where(i =>
                i.ProductQuantity.ToString(CultureInfo.InvariantCulture)
                .Contains(substring)).ToList();
        }

        internal List<Product> FilterProductsByUnitContaining(
            List<Product> products, string substring)
        {
            return products.Where(i =>
                i.ProductUnit.ToLower()
                .Contains(substring.ToLower())).ToList();
        }
    }
}
