using ProcessOrdersWithRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ProcessOrdersWithRules.Controllers
{
    public class BusinessRulesEngineController : ApiController
    {
        public string ProcessOrder(int OrderId, bool IsExecute)
        {
            Order o = new Order();

            Product p = new Product();

            Executables exq = new Executables();
            MapExecutables(p, OrderId, IsExecute, ref exq);
            return "Order Processed Sucessfully";
        }

        private void MapExecutables(Product p, int orderId, bool isExecute, ref Executables exq)
        {
            throw new NotImplementedException();
        }
    }
}
