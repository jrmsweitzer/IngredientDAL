using System;
using System.Text;
using IngredientDAL.Models;

namespace IngredientDAL.Formatters
{
    public class IngredientFormatter
    {
        private const string Formatter = 
            "\n    Added Ingredient: {0}";

        private static StringBuilder _builder;

        public static string Format(params Ingredient[] args)
        {
            _builder = new StringBuilder();
            foreach (var arg in args)
            {
                _builder.Append(string.Format(Formatter,
                    arg.IngredientName)
                    .Replace("\n", Environment.NewLine));
            }
            return _builder.ToString();
        }
    }
}
