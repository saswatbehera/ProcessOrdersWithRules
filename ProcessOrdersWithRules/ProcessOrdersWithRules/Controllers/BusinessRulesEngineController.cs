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
        public string ProcessOrder()
        {
            return "Order Processed Sucessfully";
        }
    }
}
