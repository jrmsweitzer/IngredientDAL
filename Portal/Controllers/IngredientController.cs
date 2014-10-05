using IngredientDAL.Controllers;
using IngredientDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class IngredientController : BaseController
    {
        // GET: Ingredient
        public ActionResult Index()
        {
            ViewBag.AllReceiptItems = controller.GetAllReceiptItems();
            return View();
        }
    }
}