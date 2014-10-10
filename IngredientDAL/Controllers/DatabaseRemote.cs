using System.Linq;
using IngredientDAL.Bots;
using IngredientDAL.DAL;
using IngredientDAL.Models;
using System;
using System.Collections.Generic;

namespace IngredientDAL.Controllers
{
    /**
     * This is our controller class. This is not a controller for an MVC
     * project, but rather a controller in the sens that this is what controls
     * all of our database "stuff". To initialize this controller, simply
     * create a new IngredientController(). By default, it will pass in the
     * IngredientContext by itself. To test our controller, and it's method
     * calls, we would pass in a new FakeIngredientContext. It would look 
     * like this:
     * var controller = new IngredientController(new FakeIngredientContext());
     * 
     * The controller takes care of all of our robot classes, and distributes
     * the method calls appropriately.
     * */
    public class DatabaseRemote
    {
        //DALBot - handles any and all database calls
        private DalBot _dalbot;

        //SearchBot - holds the data from the DALBot. Used to search and sort
        //our data without actually making calls to the database.
        private SearchBot _searchbot;

        //Our Data
        internal static List<ReceiptItem> RECEIPT;
        internal static List<Product> PRODUCTS;
        internal static List<Ingredient> INGREDIENTS;
        internal static List<Recipe> RECIPES;
        internal static List<RecipeItem> RECIPE_ITEMS;
        internal static List<Step> STEPS;
        internal static Refrigerator REFRIGERATOR;
        internal static List<RefrigeratedProduct> PRODUCTSINFRIDGE; 

        /**
         * Our default constructor
         * */
        public DatabaseRemote()
        {
            Initialize(new IngredientContext());
        }

        /**
         * Our constructor used for mocking. Just pass in a new 
         * FakeIngredientContext()
         * */
        public DatabaseRemote(IIngredientContext context)
        {
            Initialize(context);
        }

        /**
         * Our initialization method is used as a way to overload the contructors
         * */
        private void Initialize(IIngredientContext context)
        {
            _dalbot = new DalBot(context);
            RECEIPT = _dalbot.GetAllReceiptItems();
            PRODUCTS = _dalbot.GetAllProducts();
            INGREDIENTS = _dalbot.GetAllIngredients();
            RECIPES = _dalbot.GetAllRecipes();
            RECIPE_ITEMS = _dalbot.GetAllRecipeItems();
            STEPS = _dalbot.GetAllSteps();
            REFRIGERATOR = _dalbot.GetRefrigerator();
            PRODUCTSINFRIDGE = _dalbot.GetAllProductsInFridge();
            if (REFRIGERATOR == null)
            {
                REFRIGERATOR = _dalbot.StartNewRefrigerator();
            }
            _searchbot = new SearchBot();
        }

        #region GetAll Methods
        public List<Ingredient> GetAllIngredients()
        {
            return INGREDIENTS;
        }

        public List<Product> GetAllProducts()
        {
            return SortProductsByBrandName(PRODUCTS);
        }

        public List<ReceiptItem> GetAllReceiptItems()
        {
            return RECEIPT;
        }

        public List<Step> GetAllSteps()
        {
            return STEPS;
        }

        public List<Recipe> GetAllRecipes()
        {
            return RECIPES;
        }

        public List<RecipeItem> GetAllRecipeItems()
        {
            return RECIPE_ITEMS;
        }
        #endregion

        #region Add Methods
        /**
         * Allows us to add an ingredient using the ingredient name
         * */
        public DatabaseRemote AddIngredient(string ingredientName, 
            out Ingredient ingredient)
        {
            ingredient = _dalbot.AddIngredient(ingredientName);
            INGREDIENTS = _dalbot.GetAllIngredients();
            return this;
        }

        public DatabaseRemote AddIngredient(string ingredientName)
        {
            Ingredient ingredient;
            return AddIngredient(ingredientName, out ingredient);
        }

        public DatabaseRemote AddProductToFridge(Product prSugar,
            DateTime expirationDate,
            out RefrigeratedProduct productInFridge)
        {
            productInFridge = _dalbot.AddProductToFridge(
                GetRefrigerator(), prSugar, expirationDate);
            return this;
        }

        public DatabaseRemote AddProductToFridge(Product prSugar, 
            DateTime expirationDate)
        {
            RefrigeratedProduct productInFridge;
            return AddProductToFridge(prSugar, expirationDate,
                out productInFridge);
        }

        /**
         * Allows us to add a product using the ingredient name
         * */
        public DatabaseRemote AddProduct(string ingredientName, 
            string brandName, int productQuantity, string productUnits,
            out Product product)
        {
            product = _dalbot.AddProduct(ingredientName, brandName,
                productQuantity, productUnits);
            INGREDIENTS = _dalbot.GetAllIngredients();
            PRODUCTS = _dalbot.GetAllProducts();
            return this;
        }

        public DatabaseRemote AddProduct(string ingredientName,
            string brandName, int productQuantity, string productUnits)
        {
            Product product;
            return AddProduct(ingredientName, brandName, productQuantity,
                productUnits, out product);
        }

        /**
         * Allows us to add a product using the ingredient class
         * */
        public DatabaseRemote AddProduct(Ingredient ingredient, 
            string brandName, int productQuantity, string productUnits,
            out Product product)
        {
            product = _dalbot.AddProduct(ingredient, brandName,
                productQuantity, productUnits);
            INGREDIENTS = _dalbot.GetAllIngredients();
            PRODUCTS = _dalbot.GetAllProducts();
            return this;
        }

        public DatabaseRemote AddProduct(Ingredient ingredient,
            string brandName, int productQuantity, string productUnits)
        {
            Product product;
            return AddProduct(ingredient, brandName, productQuantity,
                productUnits, out product);
        }

        /**
         * Allows us to add a receipt using the ingredient name, and strings
         * for the product details.
         * */
        public DatabaseRemote AddReceiptItem(string ingredientName, 
            string brandName, int productQuantity, string productUnits, 
            string storeName, DateTime dateOfReceipt, double priceOfProduct,
            out ReceiptItem receiptItem)
        {
            receiptItem = _dalbot.AddReceiptItem(ingredientName,
                brandName, productQuantity, productUnits, storeName, dateOfReceipt,
                priceOfProduct);
            INGREDIENTS = _dalbot.GetAllIngredients();
            PRODUCTS = _dalbot.GetAllProducts();
            RECEIPT = _dalbot.GetAllReceiptItems();
            return this;
        }

        public DatabaseRemote AddReceiptItem(string ingredientName,
            string brandName, int productQuantity, string productUnits,
            string storeName, DateTime dateOfReceipt, double priceOfProduct)
        {
            ReceiptItem receiptItem;
            return AddReceiptItem(ingredientName, brandName, productQuantity,
                productUnits, storeName, dateOfReceipt, priceOfProduct,
                out receiptItem);
        }

        /**
         * Allows us to add a receipt using the product class
         * */
        public DatabaseRemote AddReceiptItem(Product product,
            string storeName, DateTime dateOfReceipt, double priceOfProduct,
            out ReceiptItem receiptItem)
        {
            receiptItem = _dalbot.AddReceiptItem(product, storeName,
                dateOfReceipt, priceOfProduct);
            INGREDIENTS = _dalbot.GetAllIngredients();
            PRODUCTS = _dalbot.GetAllProducts();
            RECEIPT = _dalbot.GetAllReceiptItems();
            return this;
        }

        public DatabaseRemote AddReceiptItem(Product product,
            string storeName, DateTime dateOfReceipt, double priceOfProduct)
        {
            ReceiptItem receiptItem;
            return AddReceiptItem(product, storeName, dateOfReceipt,
                priceOfProduct, out receiptItem);
        }

        /**
         * Allows us to add a receipt using the ingredient class, and strings
         * for the product details.
         * */
        public DatabaseRemote AddReceiptItem(Ingredient ingredient,
            string brandName, int productQuantity, string productUnits,
            string storeName, DateTime dateOfReceipt, double priceOfProduct,
            out ReceiptItem receiptItem)
        {
            receiptItem = _dalbot.AddReceiptItem(ingredient, brandName,
                productQuantity, productUnits, storeName, dateOfReceipt,
                priceOfProduct);
            INGREDIENTS = _dalbot.GetAllIngredients();
            PRODUCTS = _dalbot.GetAllProducts();
            RECEIPT = _dalbot.GetAllReceiptItems();
            return this;
        }

        public DatabaseRemote AddReceiptItem(Ingredient ingredient,
            string brandName, int productQuantity, string productUnits,
            string storeName, DateTime dateOfReceipt, double priceOfProduct)
        {
            ReceiptItem receiptItem;
            return AddReceiptItem(ingredient, brandName, productQuantity,
                productUnits, storeName, dateOfReceipt, priceOfProduct,
                out receiptItem);
        }

        /**
         * Allows us to add a new Recipe.
         * */
        public DatabaseRemote AddRecipe(string recipeName,
            out Recipe recipe)
        {
            recipe = _dalbot.AddRecipe(recipeName);
            RECIPES = _dalbot.GetAllRecipes();
            return this;
        }

        public DatabaseRemote AddRecipe(string recipeName)
        {
            Recipe recipe;
            return AddRecipe(recipeName, out recipe);
        }

        /**
         * Allows us to add a new step to an already-defined recipe
         * */
        public DatabaseRemote AddStep(Recipe recipe, int stepNum, 
            string instructions, out Step step)
        {
            step = _dalbot.AddStep(recipe, stepNum, instructions);
            STEPS = _dalbot.GetAllSteps();
            return this;
        }

        public DatabaseRemote AddStep(Recipe recipe, int stepNum,
            string instructions)
        {
            Step step;
            return AddStep(recipe, stepNum, instructions, out step);
        }

        /**
         * Allows us to define a unit of an ingredient, and attach it to a step.
         * For example, half cup of water for step 2, and
         * two tablespoons of water for step 5, of the same recipe.
         * */
        public DatabaseRemote AddRecipeItem(Step step, 
            Ingredient ingredient, int quantity, string unit,
            out RecipeItem recipeItem)
        {
            recipeItem = _dalbot.AddRecipeItem(step, ingredient,
                quantity, unit);
            INGREDIENTS = _dalbot.GetAllIngredients();
            STEPS = _dalbot.GetAllSteps();
            RECIPE_ITEMS = _dalbot.GetAllRecipeItems();
            return this;
        }

        public DatabaseRemote AddRecipeItem(Step step,
            Ingredient ingredient, int quantity, string unit)
        {
            RecipeItem recipeItem;
            return AddRecipeItem(step, ingredient, quantity, unit,
                out recipeItem);
        }
        #endregion

        #region Sort Methods

        public DatabaseRemote SortIngredientsByIngredientName(
            out List<Ingredient> ingredients)
        {
            ingredients = _searchbot.SortIngredientsByIngredientName();
            return this;
        }

        public DatabaseRemote SortProductsByIngredientName(
            out List<Product> products )
        {
            products = _searchbot.SortProductsByIngredientName();
            return this;
        }

        public DatabaseRemote SortProductsByBrandName(
            out List<Product> products )
        {
            products = SortProductsByBrandName(PRODUCTS);
            return this;
        }

        public List<Product> SortProductsByBrandName(List<Product> list)
        {
            return _searchbot.SortProductsByBrandName(list);
        }

        public List<ReceiptItem> SortReceiptsByIngredientName()
        {
            return _searchbot.SortReceiptsByIngredientName();
        }

        public List<ReceiptItem> SortReceiptsByBrandName()
        {
            return _searchbot.SortReceiptsByBrandName();
        }

        public List<ReceiptItem> SortReceiptByPrice()
        {
            return _searchbot.SortReceiptsByPrice();
        }
        #endregion

        #region Find Methods
        public Ingredient FindIngredientByName(string ingredientName)
        {
            return _searchbot.FindIngredientByName(ingredientName);
        }

        public Product FindProductByIngredientName(string ingredientName)
        {
            return _searchbot.FindProductByIngredientName(ingredientName);
        }

        public Product FindProductByBrandName(string brandName)
        {
            return _searchbot.FindProductByBrandName(brandName);
        }
        #endregion

        #region Filter Methods
        public List<Product> FilterProductsByIngredientNameContaining(
            string substring)
        {
            return FilterProductsByIngredientNameContaining(PRODUCTS,
                substring);
        }

        public List<Product> FilterProductsByIngredientNameContaining(
            List<Product> products, string substring)
        {
            return _searchbot
                .FilterProductsByIngredientNameContaining(
                products, substring);
        }

        public List<Product> FilterProductsByBrandNameContaining(
            string substring)
        {
            return FilterProductsByBrandNameContaining(PRODUCTS,
                substring);
        }

        public List<Product> FilterProductsByBrandNameContaining(
            List<Product> products, string substring)
        {
            return _searchbot
                .FilterProductsByBrandNameContaining(
                products, substring);
        }

        public List<Product> FilterProductsByQuantityContaining(
            string substring)
        {
            return FilterProductsByQuantityContaining(PRODUCTS,
                substring);
        }

        public List<Product> FilterProductsByQuantityContaining(
            List<Product> products, string substring)
        {
            return _searchbot
                .FilterProductsByQuantityContaining(
                    products, substring);
        }

        public List<Product> FilterProductsByUnitContaining(
            string substring)
        {
            return FilterProductsByUnitContaining(PRODUCTS, substring);
        }

        public List<Product> FilterProductsByUnitContaining(
            List<Product> products, string substring)
        {
            return _searchbot
                .FilterProductsByUnitContaining(products, substring);
        }
        #endregion

        public Refrigerator GetRefrigerator()
        {
            return _dalbot.GetRefrigerator();
        }

        public List<RefrigeratedProduct> GetAllProductsInFridge()
        {
            return _dalbot.GetAllProductsInFridge();
        }

        public DatabaseRemote GetProductFromFridge(Product product,
            out RefrigeratedProduct rProduct)
        {
            rProduct = REFRIGERATOR.ProductsInFridge.FirstOrDefault(p =>
                p.ProductId == product.ProductId);
            return this;
        }

        public DatabaseRemote UseProductInFridge(
            RefrigeratedProduct refrigeratedProductBefore, 
            double quantityUsed, string unitsUsed, 
            out RefrigeratedProduct refrigeratedProductAfter)
        {
            double quantityBefore = refrigeratedProductBefore.QuantityLeft;
            double quantityAfter = quantityBefore - quantityUsed;
            refrigeratedProductAfter = new RefrigeratedProduct
            {
                ExpirationDate = refrigeratedProductBefore.ExpirationDate,
                Product = refrigeratedProductBefore.Product,
                ProductId = refrigeratedProductBefore.ProductId,
                QuantityLeft = quantityAfter,
                RefrigeratedProductId = refrigeratedProductBefore.RefrigeratedProductId,
                Refrigerator = refrigeratedProductBefore.Refrigerator,
                RefrigeratorId = refrigeratedProductBefore.RefrigeratorId,
                UnitsLeft = refrigeratedProductBefore.UnitsLeft
            };
            _dalbot.UpdateProductQuantityInFridge(refrigeratedProductAfter);
            PRODUCTSINFRIDGE = GetRefrigerator().ProductsInFridge.ToList();
            return this;
        }
    }
}
