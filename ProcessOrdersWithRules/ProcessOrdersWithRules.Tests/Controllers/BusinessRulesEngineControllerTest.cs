using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessOrdersWithRules;
using ProcessOrdersWithRules.Controllers;
using ProcessOrdersWithRules.Models;

namespace ProcessOrdersWithRules.Tests.Controllers
{
    [TestClass]
    public class BusinessRulesEngineControllerTest
    {
        [TestMethod]
        public void ProcessOrder()
        {
            // Arrange
            int orderId = 1;
            bool IsExecute = false;

            Order o = new Order();
            o = FetchOrders().FirstOrDefault(x => x.OrderId == orderId);

            Product p = new Product();
            p = FetchProducts().FirstOrDefault(x => x.ProductId == o.ProductId);

            Executables exq = new Executables();
            BusinessRulesEngineController controller = new BusinessRulesEngineController();

            // Act
            controller.MapExecutables(p, orderId, IsExecute, ref exq);

            // Assert
            Assert.IsTrue(exq.IsPacking);
            Assert.IsNotNull(exq.Products);
            Assert.AreEqual(exq.Products.Count, 1);
            Assert.IsTrue(exq.IsPackingSlipRequired);
            Assert.IsNotNull(exq.PackingSlips);
            Assert.AreEqual(exq.PackingSlips.Count, 2);
            Assert.IsTrue(exq.IsPhysicalProduct);
            Assert.IsTrue(exq.AgentCommission > 0);
            Assert.IsFalse(exq.IsMembership);
            Assert.IsNull(exq.MembershipType);
        }


        #region PrivateMembers
        /// This needs to be fetched from somekind of Repository
        /// Here, Just mocked the data
        private List<Product> FetchProducts()
        {
            List<Product> products = new List<Product>();
            Product p = new Product
            {
                ProductId = 1,
                ProductName = "Meluha",
                IsPhysicalProduct = true,
                IsMembershipProduct = false,
                IsVideoProduct = false,
                IsBookProduct = true,
                ProductPrice = 700,
                ProductMembershipType = ""
            };
            products.Add(p);

            p = new Product
            {
                ProductId = 2,
                ProductName = "Sherlock Holmes",
                IsPhysicalProduct = true,
                IsMembershipProduct = false,
                IsVideoProduct = false,
                IsBookProduct = true,
                ProductPrice = 500,
                ProductMembershipType = ""
            };
            products.Add(p);

            p = new Product
            {
                ProductId = 3,
                ProductName = "Nagas",
                IsPhysicalProduct = true,
                IsMembershipProduct = false,
                IsVideoProduct = false,
                IsBookProduct = true,
                ProductPrice = 900,
                ProductMembershipType = ""
            };
            products.Add(p);

            p = new Product
            {
                ProductId = 4,
                ProductName = "Activate Membership",
                IsPhysicalProduct = false,
                IsMembershipProduct = true,
                IsVideoProduct = false,
                IsBookProduct = false,
                ProductPrice = 500,
                ProductMembershipType = "Activate"
            };
            products.Add(p);

            p = new Product
            {
                ProductId = 5,
                ProductName = "Upgrade Membership",
                IsPhysicalProduct = false,
                IsMembershipProduct = true,
                IsVideoProduct = false,
                IsBookProduct = false,
                ProductPrice = 100,
                ProductMembershipType = "Upgrade"
            };
            products.Add(p);

            p = new Product
            {
                ProductId = 6,
                ProductName = "Learning to Ski",
                IsPhysicalProduct = true,
                IsMembershipProduct = false,
                IsVideoProduct = true,
                IsBookProduct = false,
                ProductPrice = 200,
                ProductMembershipType = ""
            };
            products.Add(p);

            p = new Product
            {
                ProductId = 7,
                ProductName = "First Aid",
                IsPhysicalProduct = true,
                IsMembershipProduct = false,
                IsVideoProduct = true,
                IsBookProduct = false,
                ProductPrice = 50,
                ProductMembershipType = ""
            };
            products.Add(p);

            p = new Product
            {
                ProductId = 8,
                ProductName = "Sholay",
                IsPhysicalProduct = true,
                IsMembershipProduct = false,
                IsVideoProduct = true,
                IsBookProduct = false,
                ProductPrice = 100,
                ProductMembershipType = ""
            };
            products.Add(p);

            return products;
        }


        /// This needs to be fetched from somekind of Repository
        /// Here, Just mocked the data
        private List<Order> FetchOrders()
        {
            List<Order> orders = new List<Order>();
            Order o = new Order
            {
                OrderId = 1,
                OrderNumber = "ABC001",
                CustomerName = "Steve",
                CustomerDetails = "Address And Mobile",
                ProductId = 1,
                Quantity = 2
            };
            orders.Add(o);

            o = new Order
            {
                OrderId = 2,
                OrderNumber = "XYZ123",
                CustomerName = "Darren",
                CustomerDetails = "Address And Mobile",
                ProductId = 4,
                Quantity = 1
            };
            orders.Add(o);

            o = new Order
            {
                OrderId = 3,
                OrderNumber = "ABC456",
                CustomerName = "James",
                CustomerDetails = "Address And Mobile",
                ProductId = 6,
                Quantity = 1
            };
            orders.Add(o);

            o = new Order
            {
                OrderId = 4,
                OrderNumber = "PQR789",
                CustomerName = "Mark",
                CustomerDetails = "Address And Mobile",
                ProductId = 8,
                Quantity = 1
            };
            orders.Add(o);

            return orders;
        }
        #endregion PrivateMembers
    }
}
