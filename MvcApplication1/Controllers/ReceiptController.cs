using IngredientDAL.Controllers;
using IngredientDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class ReceiptController : ApiController
    {
        private IngredientController controller;
        public ReceiptController()
        {
            controller = new IngredientController();
        }

        [HttpGet]
        [ActionName("GetAllIngredients")]
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return controller.GetAllIngredients();
        }
    }
}
