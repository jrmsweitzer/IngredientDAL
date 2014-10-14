using IngredientDAL.Controllers;
using IngredientDAL.DAL;
using IngredientDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IngredientDAL.Bots
{
    public class DalBot
    {
        private readonly IIngredientContext _db;

        public DalBot(IIngredientContext context)
        {
            _db = context;
        }

        internal Ingredient AddIngredient(string ingredientName)
        {
            var ingredient = _db.Ingredients.FirstOrDefault(i =>
                i.IngredientName == ingredientName);
            if (ingredient == null)
            {
                ingredient = _db.Ingredients.Add(new Ingredient
                    {
                        IngredientName = ingredientName,
                        HasFoundNutrients = false,
                        ServingSizeQuantity = 0,
                        ServingSizeUnits = "",
                        CaloriesPerServing = 0,
                        FatPerServing = 0,
                        CholesterolPerServing = 0,
                        SodiumPerServing = 0,
                        PotassiumPerServing = 0,
                        CarbohydratesPerServing = 0,
                        SugarsPerServing = 0,
                        ProteinPerServing = 0
                    });
            }

            _db.SaveChanges();

            return ingredient;
        }

        internal Recipe AddRecipe(string recipeName)
        {
            var recipe = _db.Recipes.Add(new Recipe
            {
                Name = recipeName,
                Steps = new List<Step>()
            });
            _db.SaveChanges();
            return recipe;
        }

        internal Product AddProduct(string ingredientName, string brandName, 
            double quantity, string unit)
        {
            var ingredientInDb = _db.Ingredients.FirstOrDefault(i => 
                i.IngredientName == ingredientName) ??
                                 AddIngredient(ingredientName);

            return AddProduct(ingredientInDb, brandName, quantity, unit);
        }

        internal Step AddStep(Recipe recipe, int stepNum, string instructions)
        {
            var step = new Step
            {
                Recipe = recipe,
                RecipeId = recipe.RecipeId,
                StepNum = stepNum,
                StepInstructions = instructions,
                RecipeItems = new List<RecipeItem>()
            };
            _db.Steps.Add(step);
            _db.Recipes.Single(i => i.RecipeId == recipe.RecipeId)
                .Steps.Add(step);
            _db.SaveChanges();
            return step;
        }

        internal RefrigeratedProduct AddProductToFridge(Refrigerator fridge,
            Product product, DateTime expirationDate)
        {
            var rproduct = new RefrigeratedProduct
            {
                Product = product,
                ProductId = product.ProductId,
                Refrigerator = fridge,
                RefrigeratorId = fridge.RefrigeratorId,
                ExpirationDate = expirationDate,
                QuantityLeft = product.ProductQuantity,
                UnitsLeft = product.ProductUnit
            };
            _db.ProductsInFridge.Add(rproduct);
            fridge.ProductsInFridge.Add(rproduct);
            _db.SaveChanges();
            return rproduct;
        }

        internal Product AddProduct(Ingredient ingredient, string brandName, 
            double quantity, string unit)
        {
            Product product;
            var prodInDb = 
                DatabaseRemote.PRODUCTS
                    .FirstOrDefault(i => i.IngredientId == ingredient.IngredientId &&
                                         i.BrandName == brandName &&
                                         i.ProductUnit == unit &&
                    // ReSharper disable CompareOfFloatsByEqualityOperator
                                         i.ProductQuantity == quantity);
                    // ReSharper restore CompareOfFloatsByEqualityOperator
            if (prodInDb == null)
            {
                product = new Product
                {
                    Ingredient = ingredient,
                    IngredientId = ingredient.IngredientId,
                    BrandName = brandName,
                    ProductQuantity = quantity,
                    ProductUnit = unit
                };
                _db.Products.Add(product);
            }
            else
            {
                product = prodInDb;
            }
            _db.SaveChanges();

            return product;
        }

        internal ReceiptItem AddReceiptItem(string ingredientName, string brandName, double quantity, string unit, string storeName, DateTime dateOfReceipt, double priceOfItem)
        {
            Ingredient ingredient = _db.Ingredients.FirstOrDefault(i => 
                i.IngredientName.Equals(ingredientName)) ?? 
                    AddIngredient(ingredientName);

            return AddReceiptItem(ingredient, brandName, quantity, unit, 
                storeName, dateOfReceipt, priceOfItem);
        }

        public ReceiptItem AddReceiptItem(Ingredient ingredient, 
            string brandName, double quantity, string unit, 
            string storeName, DateTime dateOfReceipt, double priceOfItem)
        {
            var productInDb = _db.Products.FirstOrDefault(i => 
                i.IngredientId == ingredient.IngredientId &&
                i.BrandName.Equals(brandName) &&
// ReSharper disable CompareOfFloatsByEqualityOperator
                i.ProductQuantity == quantity &&
// ReSharper restore CompareOfFloatsByEqualityOperator
                i.ProductUnit.Equals(unit)) ?? 
                AddProduct(ingredient, brandName, quantity, unit);

            return AddReceiptItem(productInDb, 
                storeName, dateOfReceipt, priceOfItem);
        }

        internal ReceiptItem AddReceiptItem(Product product, string storeName, DateTime dateOfReceipt, double priceOfItem)
        {
            var item = new ReceiptItem
            {
                StoreName = storeName,
                ReceiptDate = dateOfReceipt,
                ProductId = product.ProductId,
                IngredientPrice = priceOfItem,
                Product = product
            };

            _db.ReceiptItems.Add(item);
            _db.SaveChanges();
            return item;
        }

        internal RecipeItem AddRecipeItem(Step step, Ingredient ingredient, 
            double quantity, string unit)
        {
            var recipeItem = new RecipeItem
            {
                Ingredient = ingredient,
                IngredientId = ingredient.IngredientId,
                IngredientQuantity = quantity,
                IngredientUnit = unit,
                Step = step,
                StepId = step.StepId
            };

            step.RecipeItems.Add(recipeItem);
            _db.RecipeItems.Add(recipeItem);
            _db.SaveChanges();
            return recipeItem;
        }

        internal List<ReceiptItem> GetAllReceiptItems()
        {
            return _db.ReceiptItems.ToList();
        }

        internal List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        internal List<Ingredient> GetAllIngredients()
        {
            return _db.Ingredients.ToList();
        }

        internal List<Recipe> GetAllRecipes()
        {
            return _db.Recipes.ToList();
        }

        internal List<RecipeItem> GetAllRecipeItems()
        {
            return _db.RecipeItems.ToList();
        }

        internal List<Step> GetAllSteps()
        {
            return _db.Steps.ToList();
        }

        internal Refrigerator GetRefrigerator()
        {
            if (_db.Refrigerator.Count() == 0)
            {
                _db.Refrigerator.Add(new Refrigerator());
                _db.SaveChanges();
            }
            return _db.Refrigerator.FirstOrDefault();
        }


        internal List<RefrigeratedProduct> GetAllProductsInFridge()
        {
            return _db.ProductsInFridge.ToList();
        }

        internal Refrigerator StartNewRefrigerator()
        {
            var fridge = new Refrigerator
            {
                ProductsInFridge = GetAllProductsInFridge()
            };
            _db.Refrigerator.Add(fridge);
            _db.SaveChanges();
            return fridge;
        }

        internal void UpdateProductQuantityInFridge(RefrigeratedProduct product)
        {
            var productInDb = 
                _db.ProductsInFridge.Single(p =>
                p.RefrigeratedProductId ==
                product.RefrigeratedProductId);
            if (product.QuantityLeft == 0)
            {
                _db.ProductsInFridge.Remove(productInDb);
                GetRefrigerator().ProductsInFridge = 
                    _db.ProductsInFridge.ToList();
            }
        }
    }
}
