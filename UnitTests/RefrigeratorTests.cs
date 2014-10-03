using System;
using IngredientDAL.Controllers;
using IngredientDAL.DAL;
using IngredientDAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RefrigeratorTests : TestConstants
    {
        private RobotController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var context = new FakeIngredientContext();
            _controller = new RobotController(context);
        }

        [TestMethod]
        public void AddProductsToRefrigerator()
        {
            Assert.IsNotNull(_controller.GetRefrigerator());

            Assert.AreEqual(ZERO, _controller.GetAllProducts().Count);

            Ingredient inSugar;
            _controller.AddIngredient(SUGAR, out inSugar);

            Product prSugar;
            Assert.AreEqual(ONE, _controller.AddProduct(inSugar,
                IN_THE_RAW, TWO, POUNDS, out prSugar)
                .GetAllProducts().Count);

            Assert.AreEqual(ZERO,
                _controller.GetAllProductsInFridge().Count);
            Assert.AreEqual(ONE,
                _controller.AddProductToFridge(prSugar, DateTime.Now)
                .GetAllProductsInFridge().Count);
        }

        [TestMethod]
        public void UseItemsInRefrigerator()
        {
            Assert.IsNotNull(_controller.GetRefrigerator());

            Assert.AreEqual(ZERO, _controller.GetAllProducts().Count);

            Ingredient inSugar;
            _controller.AddIngredient(SUGAR, out inSugar);

            Product prSugar;
            Assert.AreEqual(ONE, _controller.AddProduct(inSugar,
                IN_THE_RAW, TWO, POUNDS, out prSugar)
                .GetAllProducts().Count);

            Assert.AreEqual(ZERO,
                _controller.GetAllProductsInFridge().Count);
            Assert.AreEqual(ONE,
                _controller.AddProductToFridge(prSugar, DateTime.Now)
                .GetAllProductsInFridge().Count);

            RefrigeratedProduct fpSugar;
            _controller.GetProductFromFridge(prSugar, out fpSugar);

            Assert.AreEqual(TWO, fpSugar.QuantityLeft);
            Assert.AreEqual(POUNDS, fpSugar.UnitsLeft);

            _controller.UseProductInFridge(fpSugar, ONE, POUNDS, out fpSugar);

            Assert.AreEqual(ONE,
                _controller.GetAllProductsInFridge().Count);

            Assert.AreEqual(ONE, fpSugar.QuantityLeft);
            Assert.AreEqual(POUNDS, fpSugar.UnitsLeft);
        }

        [TestMethod]
        public void AddingSixBananasToFridgeThenEatingTwo()
        {
            Assert.IsNotNull(_controller.GetRefrigerator());
            //We start with an empty refrigerator
            Assert.AreEqual(ZERO, 
                _controller.GetRefrigerator().ProductsInFridge.Count);

            // Then we create six bananas as a product
            Product sixbananas;
            _controller.AddProduct(BANANAS, DOLE, SIX, WHOLE_FRUIT, out sixbananas);

            // Then throw them into the fridge. There should now be 1 item
            // in the fridge.
            RefrigeratedProduct bananasInFridge;
            Assert.AreEqual(ONE, 
                _controller.AddProductToFridge(sixbananas, DateTime.Now,
                    out bananasInFridge)
                .GetAllProductsInFridge().Count);

            // And we'll confirm that there are six in the fridge
            Assert.AreEqual(SIX, bananasInFridge.QuantityLeft);

            // Now we'll 'eat' two of them.
            _controller.UseProductInFridge(bananasInFridge, TWO, WHOLE_FRUIT,
                out bananasInFridge);

            // There should still be 1 item left in the fridge, the bananas.
            // There should only be four bananas left.
            Assert.AreEqual(ONE, _controller.GetAllProductsInFridge().Count);
            Assert.AreEqual(FOUR, bananasInFridge.QuantityLeft);

            // Now we'll 'eat' the other four. We should be left with no items
            // left in the fridge

            _controller.UseProductInFridge(bananasInFridge, FOUR, WHOLE_FRUIT,
                out bananasInFridge);

            Assert.AreEqual(ZERO, _controller.GetAllProductsInFridge().Count);
            Assert.AreEqual(ZERO, bananasInFridge.QuantityLeft);
        }
    }
}
