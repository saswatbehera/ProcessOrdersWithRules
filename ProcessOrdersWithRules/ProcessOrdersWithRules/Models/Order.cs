using System.Collections.Generic;

namespace ProcessOrdersWithRules.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDetails { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class Orders
    {
        public List<Order> orders { get; set; }
    }
}