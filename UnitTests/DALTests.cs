using System;
using System.Collections.Generic;
using System.Linq;
using IngredientDAL.Controllers;
using IngredientDAL.DAL;
using IngredientDAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DalTests : TestConstants
    {
        private DateTime _date;
        private RobotController _controller;

        #region Test Variables

        // ReSharper disable InconsistentNaming
        private const int NUMBER_OF_INGREDIENTS_IN_FAKE_DATABASE = 4;
        private const int NUMBER_OF_PRODUCTS_IN_FAKE_DATABASE = 4;
        private const int NUMBER_OF_RECEIPTS_IN_FAKE_DATABASE = 12;
        private const double PRICE_OF_BANANAS_AT_GERESBECKS = 1.59;
        private const double PRICE_OF_BANANAS_AT_REDNERS = 1.69;
        private const double PRICE_OF_BANANAS_AT_SHOPPERS = 1.89;
        private const double PRICE_OF_JUICE_AT_GERESBECKS = 3.59;
        private const double PRICE_OF_JUICE_AT_REDNERS = 4.29;
        private const double PRICE_OF_JUICE_AT_SHOPPERS = 3.89;
        private const double PRICE_OF_MILK_AT_GERESBECKS = 3.09;
        private const double PRICE_OF_MILK_AT_REDNERS = 2.99;
        private const double PRICE_OF_MILK_AT_SHOPPERS = 3.19;
        private const double PRICE_OF_SUGAR_AT_GERESBECKS = 3.49;
        private const double PRICE_OF_SUGAR_AT_REDNERS = 3.99;
        private const double PRICE_OF_SUGAR_AT_SHOPPERS = 3.19;
        // ReSharper restore InconsistentNaming

        #endregion

        [TestInitialize]
        public void Setup()
        {
            var context = new FakeIngredientContext();
            _controller = new RobotController(context);
            _date = DateTime.Now;
            Assert.IsNotNull(_controller);
        }

        [TestMethod]
        public void AddIngredient()
        {
            Assert.AreEqual(ZERO, 
                _controller
                    .GetAllIngredients().Count);

            // Adding an ingredient with a string
            Assert.AreEqual(ONE,
                _controller.AddIngredient(TOMATO)
                    .GetAllIngredients().Count);
            
            // Adding an ingredient with a string, 
            // and passing it 'out' to an Ingredient object.
            // There should be two ingredients after this transaction.
            Ingredient potato;
            Assert.AreEqual(TWO,
                _controller.AddIngredient(POTATOS, out potato)
                    .GetAllIngredients().Count);


            Assert.IsTrue(_controller.GetAllIngredients().Contains(potato));
            Assert.IsTrue(_controller.GetAllIngredients().Contains(
                _controller.GetAllIngredients().Single(i => 
                    i.IngredientName.Equals(TOMATO))));
        }

        [TestMethod]
        public void AddIngredientIfAlreadyInDatabase()
        {
            Assert.AreEqual(ZERO,
                _controller
                    .GetAllIngredients().Count);

            // Adding an ingredient
            Assert.AreEqual(ONE,
                _controller.AddIngredient(TOMATO)
                    .GetAllIngredients().Count);

            // Adding  it again to verify there is still only one in the
            // database
            Assert.AreEqual(ONE,
                _controller.AddIngredient(TOMATO)
                    .GetAllIngredients().Count);
        }

        [TestMethod]
        public void AddProductFromStrings()
        {
            Assert.AreEqual(ZERO, 
                _controller.GetAllProducts().Count);
            Assert.AreEqual(ZERO, 
                _controller.GetAllIngredients().Count);

            Assert.AreEqual(ONE,
                _controller.AddProduct(SUGAR, IN_THE_RAW, TWO, POUNDS)
                    .GetAllProducts().Count);
            Assert.AreEqual(ONE, 
                _controller.GetAllIngredients().Count);

            Assert.AreEqual(SUGAR, 
                _controller.GetAllProducts()[ZERO].Ingredient.IngredientName);
            Assert.AreEqual(IN_THE_RAW, 
                _controller.GetAllProducts()[ZERO].BrandName);
            Assert.AreEqual(TWO, 
                _controller.GetAllProducts()[ZERO].ProductQuantity);
            Assert.AreEqual(POUNDS, 
                _controller.GetAllProducts()[ZERO].ProductUnit);
        }

        [TestMethod]
        public void AddProductFromIngredient()
        {
            Assert.AreEqual(ZERO, 
                _controller.GetAllProducts().Count);
            Assert.AreEqual(ZERO, 
                _controller.GetAllIngredients().Count);

            Ingredient inSugar;
            Assert.AreEqual(ONE, 
                _controller.AddIngredient(SUGAR, out inSugar)
                    .GetAllIngredients().Count);
            Assert.AreEqual(ONE,
                _controller.AddProduct(inSugar, IN_THE_RAW, TWO, POUNDS)
                    .GetAllProducts().Count);

            Assert.AreEqual(ONE, 
                _controller.GetAllProducts().Count);
            Assert.AreEqual(ONE, 
                _controller.GetAllIngredients().Count);

            Assert.AreEqual(SUGAR,
                _controller.GetAllProducts()[ZERO].Ingredient.IngredientName);
            Assert.AreEqual(IN_THE_RAW,
                _controller.GetAllProducts()[ZERO].BrandName);
            Assert.AreEqual(TWO,
                _controller.GetAllProducts()[ZERO].ProductQuantity);
            Assert.AreEqual(POUNDS,
                _controller.GetAllProducts()[ZERO].ProductUnit);
            Assert.AreEqual(inSugar.IngredientId,
                _controller.GetAllProducts()[ZERO].IngredientId);
        }

        [TestMethod]
        public void AddProductIfAlreadyInDatabase()
        {
            Assert.AreEqual(ZERO, _controller.GetAllProducts().Count);
            Assert.AreEqual(ONE,
                _controller.AddProduct(SUGAR, IN_THE_RAW, TWO, POUNDS)
                .GetAllProducts().Count);
            Assert.AreEqual(ONE,
                _controller.AddProduct(SUGAR, IN_THE_RAW, TWO, POUNDS)
                .GetAllProducts().Count);
        }

        [TestMethod]
        public void AddReceiptFromStrings()
        {
            Assert.AreEqual(ZERO, _controller.GetAllProducts().Count);
            Assert.AreEqual(ZERO, _controller.GetAllIngredients().Count);
            Assert.AreEqual(ZERO, _controller.GetAllReceiptItems().Count);


            Assert.AreEqual(ONE, 
                _controller.AddReceiptItem(SUGAR, IN_THE_RAW, TWO, POUNDS, 
                                           REDNERS, _date, 3.99)
                .GetAllReceiptItems(). Count);
            Assert.AreEqual(ONE, 
                _controller.GetAllProducts().Count);
            Assert.AreEqual(ONE, 
                _controller.GetAllIngredients().Count);

            Assert.AreEqual(SUGAR,
                _controller.GetAllReceiptItems()[ZERO].Product
                    .Ingredient.IngredientName);
            Assert.AreEqual(IN_THE_RAW,
                _controller.GetAllReceiptItems()[ZERO].Product.BrandName);
            Assert.AreEqual(TWO,
                _controller.GetAllReceiptItems()[ZERO].Product.ProductQuantity);
            Assert.AreEqual(POUNDS,
                _controller.GetAllReceiptItems()[ZERO].Product.ProductUnit);
            Assert.AreEqual(REDNERS,
                _controller.GetAllReceiptItems()[ZERO].StoreName);
            Assert.AreEqual(_date,
                _controller.GetAllReceiptItems()[ZERO].ReceiptDate);
            Assert.AreEqual(3.99,
                _controller.GetAllReceiptItems()[ZERO].IngredientPrice);
        }

        [TestMethod]
        public void AddReceiptFromProductNoIngredient()
        {
            Assert.AreEqual(ZERO, _controller.GetAllProducts().Count);
            Assert.AreEqual(ZERO, _controller.GetAllIngredients().Count);
            Assert.AreEqual(ZERO, _controller.GetAllReceiptItems().Count);

            Product prInTheRawSugar;
            Assert.AreEqual(ONE, 
                _controller.AddProduct(SUGAR, IN_THE_RAW, TWO, POUNDS, 
                    out prInTheRawSugar)
                .GetAllProducts().Count);
            Assert.AreEqual(ONE, 
                _controller.AddReceiptItem(prInTheRawSugar, 
                    REDNERS, _date, 3.99)
                .GetAllReceiptItems().Count);
            Assert.AreEqual(ONE, 
                _controller.GetAllIngredients().Count, ONE);

            Assert.AreEqual(SUGAR,
                _controller.GetAllReceiptItems()[ZERO].Product
                    .Ingredient.IngredientName);
            Assert.AreEqual(IN_THE_RAW,
                _controller.GetAllReceiptItems()[ZERO].Product.BrandName);
            Assert.AreEqual(TWO,
                _controller.GetAllReceiptItems()[ZERO].Product.ProductQuantity);
            Assert.AreEqual(POUNDS,
                _controller.GetAllReceiptItems()[ZERO].Product.ProductUnit);
            Assert.AreEqual(REDNERS,
                _controller.GetAllReceiptItems()[ZERO].StoreName);
            Assert.AreEqual(_date,
                _controller.GetAllReceiptItems()[ZERO].ReceiptDate);
            Assert.AreEqual(3.99,
                _controller.GetAllReceiptItems()[ZERO].IngredientPrice);
        }

        [TestMethod]
        public void AddReceiptFromIngredientNoProduct()
        {
            Assert.AreEqual(ZERO, _controller.GetAllProducts().Count);
            Assert.AreEqual(ZERO, _controller.GetAllIngredients().Count);
            Assert.AreEqual(ZERO, _controller.GetAllReceiptItems().Count);

            Ingredient inSugar;
            Assert.AreEqual(ONE,
                _controller.AddIngredient(SUGAR, out inSugar)
                    .GetAllIngredients().Count);
            Assert.AreEqual(ONE,
                _controller.AddReceiptItem(inSugar, 
                IN_THE_RAW, TWO, POUNDS, REDNERS, _date, 3.99)
                    .GetAllReceiptItems().Count);
            Assert.AreEqual(ONE, _controller.GetAllProducts().Count);

            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.Ingredient.IngredientName, SUGAR);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.BrandName, IN_THE_RAW);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.ProductQuantity, TWO);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.ProductUnit, POUNDS);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].StoreName, REDNERS);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].ReceiptDate, _date);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].IngredientPrice, 3.99);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.Ingredient.IngredientId,
                inSugar.IngredientId);
        }

        [TestMethod]
        public void AddReceiptFromProductAndIngredient()
        {
            Assert.AreEqual(ZERO, _controller.GetAllProducts().Count);
            Assert.AreEqual(ZERO, _controller.GetAllIngredients().Count);
            Assert.AreEqual(ZERO, _controller.GetAllReceiptItems().Count);

            Ingredient inSugar;
            Assert.AreEqual(ONE,
                _controller.AddIngredient(SUGAR, out inSugar)
                    .GetAllIngredients().Count);

            Product prSugarInTheRaw;
            Assert.AreEqual(ONE,
                _controller.AddProduct(inSugar, IN_THE_RAW, TWO, POUNDS, 
                    out prSugarInTheRaw)
                    .GetAllProducts().Count);

            Assert.AreEqual(ONE, 
                _controller.AddReceiptItem(prSugarInTheRaw, 
                REDNERS, _date, 3.99)
                    .GetAllReceiptItems().Count);

            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.Ingredient.IngredientName, SUGAR);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.BrandName, IN_THE_RAW);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.ProductQuantity, TWO);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.ProductUnit, POUNDS);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].StoreName, REDNERS);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].ReceiptDate, _date);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].IngredientPrice, 3.99);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.ProductId, prSugarInTheRaw.ProductId);
            Assert.AreEqual(_controller.GetAllReceiptItems()[ZERO].Product.Ingredient.IngredientId,
                inSugar.IngredientId);
        }

        [TestMethod]
        public void SortIngredientsByName()
        {
            Assert.AreEqual(ZERO, 
                _controller.GetAllIngredients().Count);
            
            FillDatabaseWithFakeData();

            Assert.AreEqual(NUMBER_OF_INGREDIENTS_IN_FAKE_DATABASE,
                _controller.GetAllIngredients().Count);

            List<Ingredient> ingredients;
            _controller.SortIngredientsByIngredientName(out ingredients);

            Assert.AreEqual(ingredients[ZERO].IngredientName, ALMONDCOCONUTMILK);
            Assert.AreEqual(ingredients[ONE].IngredientName, APPLEJUICE);
            Assert.AreEqual(ingredients[TWO].IngredientName, BANANAS);
            Assert.AreEqual(ingredients[THREE].IngredientName, SUGAR);
        }

        [TestMethod]
        public void SortProductsByIngredientName()
        {
            Assert.AreEqual(_controller.GetAllProducts().Count, ZERO);
            FillDatabaseWithFakeData();
            Assert.AreEqual(_controller.GetAllProducts().Count,
                NUMBER_OF_PRODUCTS_IN_FAKE_DATABASE);

            List<Product> products;
            _controller.SortProductsByIngredientName(out products);

            Assert.AreEqual(products[ZERO].Ingredient.IngredientName, ALMONDCOCONUTMILK);
            Assert.AreEqual(products[ONE].Ingredient.IngredientName, APPLEJUICE);
            Assert.AreEqual(products[TWO].Ingredient.IngredientName, BANANAS);
            Assert.AreEqual(products[THREE].Ingredient.IngredientName, SUGAR);
        }

        [TestMethod]
        public void SortProductsByBrandName()
        {
            Assert.AreEqual(_controller.GetAllProducts().Count, ZERO);
            FillDatabaseWithFakeData();
            Assert.AreEqual(_controller.GetAllProducts().Count,
                NUMBER_OF_PRODUCTS_IN_FAKE_DATABASE);

            List<Product> products;
            _controller.SortProductsByBrandName(out products);

            Assert.AreEqual(products[ZERO].BrandName, ALMONDBREEZE);
            Assert.AreEqual(products[ONE].BrandName, DOLE);
            Assert.AreEqual(products[TWO].BrandName, IN_THE_RAW);
            Assert.AreEqual(products[THREE].BrandName, MOTTS);
        }

        [TestMethod]
        public void SortReceiptItemsByIngredientName()
        {
            Assert.AreEqual(_controller.GetAllReceiptItems().Count, ZERO);
            FillDatabaseWithFakeData();
            Assert.AreEqual(_controller.GetAllReceiptItems().Count,
                NUMBER_OF_RECEIPTS_IN_FAKE_DATABASE);

            List<ReceiptItem> receipt = 
                _controller.SortReceiptsByIngredientName();

            Assert.AreEqual(receipt[ZERO].Product.Ingredient.IngredientName, 
                ALMONDCOCONUTMILK);
            Assert.AreEqual(receipt[ONE].Product.Ingredient.IngredientName, 
                ALMONDCOCONUTMILK);
            Assert.AreEqual(receipt[TWO].Product.Ingredient.IngredientName, 
                ALMONDCOCONUTMILK);
            Assert.AreEqual(receipt[THREE].Product.Ingredient.IngredientName, 
                APPLEJUICE);
            Assert.AreEqual(receipt[FOUR].Product.Ingredient.IngredientName, 
                APPLEJUICE);
            Assert.AreEqual(receipt[FIVE].Product.Ingredient.IngredientName, 
                APPLEJUICE);
            Assert.AreEqual(receipt[SIX].Product.Ingredient.IngredientName, 
                BANANAS);
            Assert.AreEqual(receipt[SEVEN].Product.Ingredient.IngredientName, 
                BANANAS);
            Assert.AreEqual(receipt[EIGHT].Product.Ingredient.IngredientName, 
                BANANAS);
            Assert.AreEqual(receipt[NINE].Product.Ingredient.IngredientName, 
                SUGAR);
            Assert.AreEqual(receipt[TEN].Product.Ingredient.IngredientName, 
                SUGAR);
            Assert.AreEqual(receipt[ELEVEN].Product.Ingredient.IngredientName, 
                SUGAR);
        }

        [TestMethod]
        public void SortReceiptItemsByBrandName()
        {
            Assert.AreEqual(_controller.GetAllReceiptItems().Count, ZERO);
            FillDatabaseWithFakeData();
            Assert.AreEqual(_controller.GetAllReceiptItems().Count,
                NUMBER_OF_RECEIPTS_IN_FAKE_DATABASE);

            List<ReceiptItem> receipt = _controller.SortReceiptsByBrandName();

            Assert.AreEqual(receipt[ZERO].Product.BrandName, ALMONDBREEZE);
            Assert.AreEqual(receipt[ONE].Product.BrandName, ALMONDBREEZE);
            Assert.AreEqual(receipt[TWO].Product.BrandName, ALMONDBREEZE);
            Assert.AreEqual(receipt[THREE].Product.BrandName, DOLE);
            Assert.AreEqual(receipt[FOUR].Product.BrandName, DOLE);
            Assert.AreEqual(receipt[FIVE].Product.BrandName, DOLE);
            Assert.AreEqual(receipt[SIX].Product.BrandName, IN_THE_RAW);
            Assert.AreEqual(receipt[SEVEN].Product.BrandName, IN_THE_RAW);
            Assert.AreEqual(receipt[EIGHT].Product.BrandName, IN_THE_RAW);
            Assert.AreEqual(receipt[NINE].Product.BrandName, MOTTS);
            Assert.AreEqual(receipt[TEN].Product.BrandName, MOTTS);
            Assert.AreEqual(receipt[ELEVEN].Product.BrandName, MOTTS);
        }

        [TestMethod]
        public void SortReceiptItemsByPrice()
        {
            Assert.AreEqual(_controller.GetAllReceiptItems().Count, ZERO);
            FillDatabaseWithFakeData();
            Assert.AreEqual(_controller.GetAllReceiptItems().Count,
                NUMBER_OF_RECEIPTS_IN_FAKE_DATABASE);

            var receipt = _controller.SortReceiptByPrice();

            Assert.AreEqual(receipt[ZERO].Product.Ingredient.IngredientName,
                BANANAS);
            Assert.AreEqual(receipt[ZERO].StoreName,
                GERESBECKS);
            Assert.AreEqual(receipt[ZERO].IngredientPrice,
                PRICE_OF_BANANAS_AT_GERESBECKS);

            Assert.AreEqual(receipt[ONE].Product.Ingredient.IngredientName,
                BANANAS);
            Assert.AreEqual(receipt[ONE].StoreName,
                REDNERS);
            Assert.AreEqual(receipt[ONE].IngredientPrice,
                PRICE_OF_BANANAS_AT_REDNERS);

            Assert.AreEqual(receipt[TWO].Product.Ingredient.IngredientName,
                BANANAS);
            Assert.AreEqual(receipt[TWO].StoreName,
                SHOPPERS);
            Assert.AreEqual(receipt[TWO].IngredientPrice,
                PRICE_OF_BANANAS_AT_SHOPPERS);

            Assert.AreEqual(receipt[THREE].Product.Ingredient.IngredientName,
                ALMONDCOCONUTMILK);
            Assert.AreEqual(receipt[THREE].StoreName,
                REDNERS);
            Assert.AreEqual(receipt[THREE].IngredientPrice,
                PRICE_OF_MILK_AT_REDNERS);

            Assert.AreEqual(receipt[FOUR].Product.Ingredient.IngredientName,
                ALMONDCOCONUTMILK);
            Assert.AreEqual(receipt[FOUR].StoreName,
                GERESBECKS);
            Assert.AreEqual(receipt[FOUR].IngredientPrice,
                PRICE_OF_MILK_AT_GERESBECKS);

            Assert.AreEqual(receipt[FIVE].Product.Ingredient.IngredientName,
                SUGAR);
            Assert.AreEqual(receipt[FIVE].StoreName,
                SHOPPERS);
            Assert.AreEqual(receipt[FIVE].IngredientPrice,
                PRICE_OF_SUGAR_AT_SHOPPERS);

            Assert.AreEqual(receipt[SIX].Product.Ingredient.IngredientName,
                ALMONDCOCONUTMILK);
            Assert.AreEqual(receipt[SIX].StoreName,
                SHOPPERS);
            Assert.AreEqual(receipt[SIX].IngredientPrice,
                PRICE_OF_MILK_AT_SHOPPERS);

            Assert.AreEqual(receipt[SEVEN].Product.Ingredient.IngredientName,
                SUGAR);
            Assert.AreEqual(receipt[SEVEN].StoreName,
                GERESBECKS);
            Assert.AreEqual(receipt[SEVEN].IngredientPrice,
                PRICE_OF_SUGAR_AT_GERESBECKS);

            Assert.AreEqual(receipt[TEN].Product.Ingredient.IngredientName,
                SUGAR);
            Assert.AreEqual(receipt[TEN].StoreName,
                REDNERS);
            Assert.AreEqual(receipt[TEN].IngredientPrice,
                PRICE_OF_SUGAR_AT_REDNERS);
        }

        [TestMethod]
        public void FindIngredientByName()
        {
            FillDatabaseWithFakeData();
            Ingredient milk = 
                _controller.FindIngredientByName(ALMONDCOCONUTMILK);
            Assert.AreEqual(milk.IngredientName, ALMONDCOCONUTMILK);

            Ingredient sugar =
                _controller.FindIngredientByName(SUGAR);
            Assert.AreEqual(sugar.IngredientName, SUGAR);

            Ingredient bananas =
                _controller.FindIngredientByName(BANANAS);
            Assert.AreEqual(bananas.IngredientName, BANANAS);

            Ingredient juice =
                _controller.FindIngredientByName(APPLEJUICE);
            Assert.AreEqual(juice.IngredientName, APPLEJUICE);
        }

        [TestMethod]
        public void FindProductByIngredientName()
        {
            FillDatabaseWithFakeData();
            Product milk =
                _controller.FindProductByIngredientName(ALMONDCOCONUTMILK);
            Assert.AreEqual(milk.Ingredient.IngredientName,
                ALMONDCOCONUTMILK);

            Product sugar =
                _controller.FindProductByIngredientName(SUGAR);
            Assert.AreEqual(sugar.Ingredient.IngredientName,
                SUGAR);

            Product bananas =
                _controller.FindProductByIngredientName(BANANAS);
            Assert.AreEqual(bananas.Ingredient.IngredientName,
                BANANAS);

            Product juice =
                _controller.FindProductByIngredientName(APPLEJUICE);
            Assert.AreEqual(juice.Ingredient.IngredientName,
                APPLEJUICE);
        }

        [TestMethod]
        public void FindProductByBrandName()
        {
            FillDatabaseWithFakeData();
            Product milk =
                _controller.FindProductByBrandName(ALMONDBREEZE);
            Assert.AreEqual(milk.BrandName, ALMONDBREEZE);

            Product sugar =
                _controller.FindProductByBrandName(IN_THE_RAW);
            Assert.AreEqual(sugar.BrandName, IN_THE_RAW);

            Product bananas =
                _controller.FindProductByBrandName(DOLE);
            Assert.AreEqual(bananas.BrandName, DOLE);
        }

        [TestMethod]
        public void FilterProductsByIngredientName()
        {
            FillDatabaseWithFakeData();
            Product milk = _controller.FindProductByIngredientName(ALMONDCOCONUTMILK);
            Product sugar = _controller.FindProductByIngredientName(SUGAR);
            Product juice = _controller.FindProductByIngredientName(APPLEJUICE);
            Product bananas = _controller.FindProductByIngredientName(BANANAS);

            List<Product> productsWithTheLetterL =
                _controller.FilterProductsByIngredientNameContaining("L");
            Assert.IsTrue(productsWithTheLetterL.Contains(milk));
            Assert.IsTrue(productsWithTheLetterL.Contains(juice));
            Assert.IsFalse(productsWithTheLetterL.Contains(bananas));
            Assert.IsFalse(productsWithTheLetterL.Contains(sugar));

            List<Product> productsWithTheLetterN =
                _controller.FilterProductsByIngredientNameContaining("N");
            Assert.IsTrue(productsWithTheLetterN.Contains(milk));
            Assert.IsFalse(productsWithTheLetterN.Contains(juice));
            Assert.IsTrue(productsWithTheLetterN.Contains(bananas));
            Assert.IsFalse(productsWithTheLetterN.Contains(sugar));
        }

        [TestMethod]
        public void FilterProductsByBrandName()
        {
            FillDatabaseWithFakeData();
            Product milk = _controller.FindProductByIngredientName(ALMONDCOCONUTMILK);
            Product sugar = _controller.FindProductByIngredientName(SUGAR);
            Product juice = _controller.FindProductByIngredientName(APPLEJUICE);
            Product bananas = _controller.FindProductByIngredientName(BANANAS);

            List<Product> productsWithBrandWithLetterN =
                _controller.FilterProductsByBrandNameContaining("N");
            Assert.IsTrue(productsWithBrandWithLetterN.Contains(milk));
            Assert.IsFalse(productsWithBrandWithLetterN.Contains(juice));
            Assert.IsFalse(productsWithBrandWithLetterN.Contains(bananas));
            Assert.IsTrue(productsWithBrandWithLetterN.Contains(sugar));

            List<Product> productsWithBrandWithLetterT =
                _controller.FilterProductsByBrandNameContaining("T");
            Assert.IsFalse(productsWithBrandWithLetterT.Contains(milk));
            Assert.IsTrue(productsWithBrandWithLetterT.Contains(juice));
            Assert.IsFalse(productsWithBrandWithLetterT.Contains(bananas));
            Assert.IsTrue(productsWithBrandWithLetterT.Contains(sugar));
        }

        [TestMethod]
        public void FilterProductsByBrandNameAndIngredientName()
        {
            FillDatabaseWithFakeData();
            Product milk = _controller.FindProductByIngredientName(ALMONDCOCONUTMILK);
            Product sugar = _controller.FindProductByIngredientName(SUGAR);
            Product juice = _controller.FindProductByIngredientName(APPLEJUICE);
            Product bananas = _controller.FindProductByIngredientName(BANANAS);

            //Only Almond _B_reeze Almo_n_d Coconut Milk
            List<Product> group1 =
                _controller.FilterProductsByIngredientNameContaining("N");
            group1 = _controller.FilterProductsByBrandNameContaining(group1, "B");
            Assert.IsTrue(group1.Contains(milk));
            Assert.IsFalse(group1.Contains(juice));
            Assert.IsFalse(group1.Contains(bananas));
            Assert.IsFalse(group1.Contains(sugar));

            //Al_m_ond Breeze A_l_mond Coconut Milk
            //_M_ott's App_l_e Juice
            List<Product> group2 =
                _controller.FilterProductsByIngredientNameContaining("L");
            group2 = _controller.FilterProductsByBrandNameContaining(group2, "M");
            Assert.IsTrue(group2.Contains(milk));
            Assert.IsTrue(group2.Contains(juice));
            Assert.IsFalse(group2.Contains(bananas));
            Assert.IsFalse(group2.Contains(sugar));
        }

        [TestMethod]
        public void FilterProductsByQuantity()
        {
            FillDatabaseWithFakeData();
            Product milk = _controller.FindProductByIngredientName(ALMONDCOCONUTMILK);
            Product sugar = _controller.FindProductByIngredientName(SUGAR);
            Product juice = _controller.FindProductByIngredientName(APPLEJUICE);
            Product bananas = _controller.FindProductByIngredientName(BANANAS);

            // Two quarts milk
            // Two pounds sugar
            // 32 oz. Apple Juice
            List<Product> group1 =
                _controller.FilterProductsByQuantityContaining("2");
            Assert.IsTrue(group1.Contains(milk));
            Assert.IsTrue(group1.Contains(juice));
            Assert.IsFalse(group1.Contains(bananas));
            Assert.IsTrue(group1.Contains(sugar));
        }

        [TestMethod]
        public void FilterProductsByUnits()
        {
            FillDatabaseWithFakeData();
            Product milk = _controller.FindProductByIngredientName(ALMONDCOCONUTMILK);
            Product sugar = _controller.FindProductByIngredientName(SUGAR);
            Product juice = _controller.FindProductByIngredientName(APPLEJUICE);
            Product bananas = _controller.FindProductByIngredientName(BANANAS);

            List<Product> group1 =
                _controller.FilterProductsByUnitContaining(POUNDS);
            Assert.IsFalse(group1.Contains(milk));
            Assert.IsFalse(group1.Contains(juice));
            Assert.IsFalse(group1.Contains(bananas));
            Assert.IsTrue(group1.Contains(sugar));

            List<Product> group2 =
                _controller.FilterProductsByUnitContaining(OUNCES);
            Assert.IsFalse(group2.Contains(milk));
            Assert.IsTrue(group2.Contains(juice));
            Assert.IsFalse(group2.Contains(bananas));
            Assert.IsFalse(group2.Contains(sugar));

        }

        #region private methods
        private void FillDatabaseWithFakeData()
        {
            _controller.AddReceiptItem(SUGAR, IN_THE_RAW, TWO, POUNDS,
                REDNERS, _date, PRICE_OF_SUGAR_AT_REDNERS);
            _controller.AddReceiptItem(BANANAS, DOLE, SIX, BANANAS,
                REDNERS, _date, PRICE_OF_BANANAS_AT_REDNERS);
            _controller.AddReceiptItem(APPLEJUICE, MOTTS, THIRTYTWO, OUNCES,
                REDNERS, _date, PRICE_OF_JUICE_AT_REDNERS);
            _controller.AddReceiptItem(ALMONDCOCONUTMILK, ALMONDBREEZE, TWO, QUARTS,
                REDNERS, _date, PRICE_OF_MILK_AT_REDNERS);
            _controller.AddReceiptItem(SUGAR, IN_THE_RAW, TWO, POUNDS,
                SHOPPERS, _date, PRICE_OF_SUGAR_AT_SHOPPERS);
            _controller.AddReceiptItem(BANANAS, DOLE, SIX, BANANAS,
                SHOPPERS, _date, PRICE_OF_BANANAS_AT_SHOPPERS);
            _controller.AddReceiptItem(APPLEJUICE, MOTTS, THIRTYTWO, OUNCES,
                SHOPPERS, _date, PRICE_OF_JUICE_AT_SHOPPERS);
            _controller.AddReceiptItem(ALMONDCOCONUTMILK, ALMONDBREEZE, TWO, QUARTS,
                SHOPPERS, _date, PRICE_OF_MILK_AT_SHOPPERS);
            _controller.AddReceiptItem(SUGAR, IN_THE_RAW, TWO, POUNDS,
                GERESBECKS, _date, PRICE_OF_SUGAR_AT_GERESBECKS);
            _controller.AddReceiptItem(BANANAS, DOLE, SIX, BANANAS,
                GERESBECKS, _date, PRICE_OF_BANANAS_AT_GERESBECKS);
            _controller.AddReceiptItem(APPLEJUICE, MOTTS, THIRTYTWO, OUNCES,
                GERESBECKS, _date, PRICE_OF_JUICE_AT_GERESBECKS);
            _controller.AddReceiptItem(ALMONDCOCONUTMILK, ALMONDBREEZE, TWO, QUARTS,
                GERESBECKS, _date, PRICE_OF_MILK_AT_GERESBECKS);
        }
        #endregion
    }
}