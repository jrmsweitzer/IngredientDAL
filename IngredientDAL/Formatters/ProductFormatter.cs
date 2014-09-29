using System;
using System.Text;
using IngredientDAL.Models;

namespace IngredientDAL.Formatters
{
    public class ProductFormatter
    {
        private const string Formatter =
            "\n    Added Product: {0} {1} {2} {3}";

        private static StringBuilder _builder;

        public static string Format(params Product[] args)
        {
            _builder = new StringBuilder();
            foreach (var arg in args)
            {
                _builder.Append(string.Format(Formatter,
                    arg.BrandName,
                    arg.Ingredient.IngredientName,
                    arg.ProductQuantity,
                    arg.ProductUnit)
                    .Replace("\n", Environment.NewLine));
            }
            return _builder.ToString();
        }
    }
}
