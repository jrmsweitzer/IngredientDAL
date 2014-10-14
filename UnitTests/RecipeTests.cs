using IngredientDAL.Controllers;
using IngredientDAL.DAL;
using IngredientDAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
                _controller.GetAllRecipeItemsForRecipe(recipe).Count);

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

        [TestMethod]
        public void AppleCinnamonOatmeal()
        {
            // Blank DB
            Assert.AreEqual(ZERO, _controller.GetAllRecipes().Count);

            // Recipe created and saved to DB
            Recipe rAppleCinnamonOatmeal;
            Assert.AreEqual(ONE, 
                _controller.AddRecipe("Apple Cinnamon Oatmeal", 
                out rAppleCinnamonOatmeal).GetAllRecipes().Count);

            // Recipe has no steps
            Assert.AreEqual(ZERO, rAppleCinnamonOatmeal.Steps.Count);
            
            // Steps created and added to Recipe
            _controller.AddStep(rAppleCinnamonOatmeal, ONE,
                "Bring water to a rolling boil. Boil for about 5 minutes.");
            Assert.AreEqual(ONE, rAppleCinnamonOatmeal.Steps.Count);

            _controller.AddStep(rAppleCinnamonOatmeal, TWO,
                "Add boiling water to your oats. Mix in Cinnamon and Sugar, " +
                "and allow to thicken for about 5 minutes.");
            Assert.AreEqual(TWO, rAppleCinnamonOatmeal.Steps.Count);

            _controller.AddStep(rAppleCinnamonOatmeal, THREE,
                "Enjoy!");
            Assert.AreEqual(THREE, rAppleCinnamonOatmeal.Steps.Count);

            // No Recipe Ingredients
            Assert.AreEqual(ZERO, 
                rAppleCinnamonOatmeal.GetAllRecipeIngredients().Count);
            Assert.AreEqual(ZERO, 
                _controller.GetAllRecipeItemsForRecipe(rAppleCinnamonOatmeal)
                .Count);

            // Create the ingredients for the DB (they would already exist in 
            // actual code, along with nutritional information)
            Ingredient Water;
            _controller.AddIngredient("Water", out Water);

            Ingredient Oats;
            _controller.AddIngredient("Oats", out Oats);

            Ingredient Cinnamon;
            _controller.AddIngredient("Cinnamon", out Cinnamon);

            Ingredient Sugar;
            _controller.AddIngredient("Sugar", out Sugar);

            // Add a Recipe Item (1 cup water)
            _controller.AddRecipeItem(rAppleCinnamonOatmeal, ONE, Water, 
                ONE, "cup");
            // 1 cup Oats
            _controller.AddRecipeItem(rAppleCinnamonOatmeal, TWO, Oats,
                ONE, "cup");
            // 2 Tbs Sugar
            _controller.AddRecipeItem(rAppleCinnamonOatmeal, TWO, Sugar,
                TWO, "Tbs");
            // 1 Tbs Cinnamon
            _controller.AddRecipeItem(rAppleCinnamonOatmeal, TWO, Cinnamon,
                ONE, "Tbs");

            // Four Recipe Ingredients
            Assert.AreEqual(FOUR,
                rAppleCinnamonOatmeal.GetAllRecipeIngredients().Count);
            Assert.AreEqual(FOUR,
                _controller.GetAllRecipeItemsForRecipe(rAppleCinnamonOatmeal)
                .Count);

            // One for step 1
            Assert.AreEqual(ONE,
                (rAppleCinnamonOatmeal.Steps as List<Step>)[0].RecipeItems.Count);

            // And Three for step 2.
            Assert.AreEqual(THREE,
                (rAppleCinnamonOatmeal.Steps as List<Step>)[1].RecipeItems.Count);
        }
    }
}
