using IngredientDAL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngredientDAL
{
    public class JSONifier
    {
        private IngredientController _controller;

        public JSONifier(IngredientController controller)
        {
            _controller = controller;
        }
    }
}
