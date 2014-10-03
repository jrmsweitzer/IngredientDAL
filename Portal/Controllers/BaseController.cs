using IngredientDAL.Controllers;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class BaseController : Controller
    {
        public RobotController controller = new RobotController();
    }
}