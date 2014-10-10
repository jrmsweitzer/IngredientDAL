using IngredientDAL.Controllers;
using IngredientDAL.Models;
using OpenQA.Selenium;
using SeleniumCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionHunter
{
    class Program
    {
        static void Main(string[] args)
        {
            var _driver = new Driver();
            var _remote = new DatabaseRemote();

            List<Ingredient> ingredientsToUpdate = _remote.GetAllIngredients()
                .Where(i => i.HasFoundNutrients == false).ToList();

            for (int index = 0; index < ingredientsToUpdate.Count(); index++)
            {
                _driver.Navigate().GoToUrl("http://www.foodfacts.com/");
                _driver.FindElement(By.Id("search-text"))
                    .SendKeys(ingredientsToUpdate[index].IngredientName);
                _driver.FindElement(By.Id("btnNutritionSearch")).Click();
            }

        }
    }
}
