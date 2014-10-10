using IngredientDAL.Controllers;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class BaseController : Controller
    {
        public DatabaseRemote controller = new DatabaseRemote();
    }
}