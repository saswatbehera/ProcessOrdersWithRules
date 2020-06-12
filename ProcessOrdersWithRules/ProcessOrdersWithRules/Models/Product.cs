using System.Collections.Generic;

namespace ProcessOrdersWithRules.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool IsPhysicalProduct { get; set; }
        public bool IsMembershipProduct { get; set; }
        public bool IsVideoProduct { get; set; }
        public bool IsBookProduct { get; set; }
        public int ProductPrice { get; set; }
        public string ProductMembershipType { get; set; }
    }

    public class Products
    {
        public List<Product> products { get; set; }
    }

    enum PackingSlip
    {
        CustomerShipping,
        RoyaltyDepartment
    }

    enum Membership
    {
        Activate,
        Upgrade
    }
}