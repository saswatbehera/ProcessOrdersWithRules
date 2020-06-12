using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maersk_BusinessRulesEngine.Models
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
}