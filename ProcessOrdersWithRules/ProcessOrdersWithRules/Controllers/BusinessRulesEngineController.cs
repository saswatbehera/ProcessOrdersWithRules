using ProcessOrdersWithRules.Models;
using System.Collections.Generic;
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


        [HttpGet]
        [Route("OrderProcess/GetProducts")]
        public Products GetProducts(int ProductId = 0)
        {
            BusinessLogic b = new BusinessLogic();

            Products ps = new Products();
            if (ProductId > 0)
            {
                Product p = new Product();
                p = b.GetProducts().products.FirstOrDefault(x => x.ProductId == ProductId);
                ps.products = new List<Product>();
                ps.products.Add(p);
            }
            else
            {
                ps = b.GetProducts();
            }

            return ps;
        }


        [HttpGet]
        [Route("OrderProcess/GetOrders")]
        public Orders GetOrders(int OrderId = 0)
        {
            BusinessLogic b = new BusinessLogic();

            Orders os = new Orders();
            if (OrderId > 0)
            {
                Order o = new Order();
                o = b.GetOrders().orders.FirstOrDefault(x => x.OrderId == OrderId);
                os.orders = new List<Order>();
                os.orders.Add(o);
            }
            else
            {
                os = b.GetOrders();
            }

            return os;
        }
        #endregion Routes
    }
}
