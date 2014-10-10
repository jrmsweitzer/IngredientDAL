using IngredientDAL.Controllers;
using IngredientDAL.DAL;
using IngredientDAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RecipeTests : TestConstants
    {
        private DatabaseRemote _controller;

        [TestInitialize]
        public void Setup()
        {
            var context = new FakeIngredientContext();
            _controller = new DatabaseRemote(context);
        }

        [TestMethod]
        public void AddRecipe()
        {
            Assert.AreEqual(_controller.GetAllRecipes().Count, ZERO);

            _controller.AddRecipe(APPLEBUTTERTOAST);

            Assert.AreEqual(_controller.GetAllRecipes().Count, ONE);
        }

        [TestMethod]
        public void AddStepsToRecipe()
        {
            Assert.AreEqual(_controller.GetAllSteps().Count, ZERO);

            Recipe recipe;
            _controller.AddRecipe(APPLEBUTTERTOAST, out recipe);
            const string step1Inst = "TOAST two slices of bread.";
            const string step2Inst = "SPREAD apple butter on one side of " +
                                     "each slice of toast";
            Step step1;
            _controller.AddStep(recipe, ONE, step1Inst, out step1);
            Step step2;
            _controller.AddStep(recipe, TWO, step2Inst, out step2);


            Assert.IsNotNull(step1);
            Assert.IsNotNull(step2);
            Assert.AreEqual(TWO,
                recipe.Steps.Count);

            var recipes = _controller.GetAllRecipes();
            Assert.AreEqual(recipes[ZERO].Name, APPLEBUTTERTOAST);
            Assert.AreEqual(TWO, recipes[ZERO].Steps.Count);
        }

        [TestMethod]
        public void AddRecipeItemsToRecipe()
        {
            Assert.AreEqual(_controller.GetAllSteps().Count, ZERO);
            // Create Recipe
            Recipe recipe;
            _controller.AddRecipe(APPLEBUTTERTOAST, out recipe);
            // Create Ingredients
            Ingredient bread;
            _controller.AddIngredient(WHOLE_WHEAT_BREAD, out bread);
            Ingredient applebutter;
            _controller.AddIngredient(APPLE_BUTTER, out applebutter);
            // Create the steps (need a recipe obj or recipeID to create steps)
            const string step1Inst = "TOAST two slices of bread.";
            const string step2Inst = "SPREAD apple butter on one side of " +
                                     "each slice of toast";
            Step step1;
            _controller.AddStep(recipe, ONE, step1Inst, out step1);
            Step step2;
            _controller.AddStep(recipe, TWO, step2Inst, out step2);
            // Create RecipeItems (need the step and the ingredient that this
            // recipe item attaches to)
            RecipeItem breadItem;
            _controller.AddRecipeItem(step1, bread, TWO, SLICES,
                out breadItem);
            _controller.AddRecipeItem(step2, applebutter, TWO, TABLESPOONS);

            // Assertions
            Assert.IsNotNull(step1);
            Assert.IsNotNull(step2);
            Assert.AreEqual(TWO,
                recipe.Steps.Count);

            Assert.IsNotNull(breadItem);
            Assert.AreEqual(ONE,
                step1.RecipeItems.Count);
            Assert.AreEqual(ONE,
                step2.RecipeItems.Count);
            Assert.AreEqual(TWO,
                _controller.GetAllRecipeItems().Count);

            var recipes = _controller.GetAllRecipes();
            Assert.AreEqual(recipes[ZERO].Name, APPLEBUTTERTOAST);
            Assert.AreEqual(TWO, recipes[ZERO].Steps.Count);

            foreach (var step in recipes[ZERO].Steps)
            {
                Assert.AreEqual(ONE,
                    step.RecipeItems.Count);
            }
        }

        [TestMethod]
        public void AddRecipePizza()
        {
            Assert.AreEqual(ZERO, 
                _controller.GetAllRecipes().Count);

            Recipe rPizza;
            Assert.AreEqual(ONE,
                _controller.AddRecipe(PIZZA, out rPizza)
                .GetAllRecipes().Count);

            Step step1;
            //Step step2;
            //Step step3;
            //Step step4;

            Assert.AreEqual(ZERO,
                rPizza.Steps.Count);

            _controller.AddStep(rPizza, ONE, "Preheat the oven to 425 F",
                out step1);
            Assert.AreEqual(ONE, 
                rPizza.Steps.Count);

            _controller.AddStep(rPizza, TWO, "Spread Tomato ");

        }
    }
}
