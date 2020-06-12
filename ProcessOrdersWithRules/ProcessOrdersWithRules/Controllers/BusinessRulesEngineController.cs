using ProcessOrdersWithRules.Models;
using System.Linq;
using System.Web.Http;

namespace ProcessOrdersWithRules.Controllers
{
    public class BusinessRulesEngineController : ApiController
    {
        public string ProcessOrder(int orderId, bool isExecute)
        {
            BusinessLogic b = new BusinessLogic();

            Order o = new Order();
            o = b.GetOrders().orders.FirstOrDefault(x => x.OrderId == orderId);

            Product p = new Product();
            p = b.GetProducts().products.FirstOrDefault(x => x.ProductId == o.ProductId);

            Executables exq = new Executables();
            b.MapExecutables(p, orderId, isExecute, ref exq);

            return "Order Processed Sucessfully";
        }

       
    }
}
