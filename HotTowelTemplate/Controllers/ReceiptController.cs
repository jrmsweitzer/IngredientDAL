using IngredientDAL.Controllers;
using IngredientDAL.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotTowelTemplate.Controllers
{
    public class ReceiptController : ApiController
    {
        private IngredientController controller;

        public ReceiptController()
        {
            if (controller == null)
            {
                controller = new IngredientController();
            }
        }

        [HttpGet]
        [ActionName("GetAllIngredients")]
        [Route("api/receipt/GetAllIngredients")]
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return controller.GetAllIngredients();
        }

    }
}
