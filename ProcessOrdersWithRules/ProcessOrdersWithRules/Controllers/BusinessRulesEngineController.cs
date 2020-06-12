using ProcessOrdersWithRules.Models;
using System.Linq;
using System.Web.Http;

namespace ProcessOrdersWithRules.Controllers
{
    public class BusinessRulesEngineController : ApiController
    {
        #region Routes
        [HttpPost]
        [Route("OrderProcess/ProcessSingleOrder")]
        public string ProcessSingleOrder(int OrderId, bool IsExecute = false)
        {
            BusinessLogic b = new BusinessLogic();

            Order o = new Order();
            o = b.GetOrders().orders.FirstOrDefault(x => x.OrderId == OrderId);

            Product p = new Product();
            p = b.GetProducts().products.FirstOrDefault(x => x.ProductId == o.ProductId);

            Executables exq = new Executables();
            b.MapExecutables(p, OrderId, IsExecute, ref exq);

            return "Order Processed Sucessfully";
        }

        #endregion Routes
    }
}
