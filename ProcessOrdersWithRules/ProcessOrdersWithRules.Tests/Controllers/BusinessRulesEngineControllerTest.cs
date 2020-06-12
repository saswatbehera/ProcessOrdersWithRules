using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessOrdersWithRules.Models;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace ProcessOrdersWithRules.Tests.Controllers
{
    [TestClass]
    public class BusinessRulesEngineControllerTest
    {
        #region Public Methods

        [TestMethod]
        public void ProcessSingleOrderTest()
        {
            // Arrange
            int orderId = 1;
            bool IsExecute = false;

            Order o = new Order();
            o = GetOrders().orders.FirstOrDefault(x => x.OrderId == orderId);

            Product p = new Product();
            p = GetProducts().products.FirstOrDefault(x => x.ProductId == o.ProductId);

            Executables exq = new Executables();
            BusinessLogic b = new BusinessLogic();

            // Act
            b.MapExecutables(p, orderId, IsExecute, ref exq);


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

        #endregion Public Methods

        #region PrivateMembers

        /// <summary>
        /// This needs to be fetched from somekind of Repository
        /// Here, Just mocked the data
        /// </summary>
        /// <returns>List<Order></returns>
        private Orders GetOrders()
        {
            string json = "{\"orders\":[" +
                "{\"OrderId\":1,\"OrderNumber\":\"ABC001\",\"CustomerName\":\"Steve\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":1,\"Quantity\":2}," +
                "{\"OrderId\":2,\"OrderNumber\":\"XYZ123\",\"CustomerName\":\"Darren\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":4,\"Quantity\":1}," +
                "{\"OrderId\":3,\"OrderNumber\":\"ABC456\",\"CustomerName\":\"James\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":6,\"Quantity\":1}," +
                "{\"OrderId\":4,\"OrderNumber\":\"PQR789\",\"CustomerName\":\"Mark\",\"CustomerDetails\":\"Address And Mobile\",\"ProductId\":8,\"Quantity\":1}]}";

            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(json));
            ms.Position = 0;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Orders));
            Orders orders = (Orders)ser.ReadObject(ms);
            return orders;
        }


        /// <summary>
        /// This needs to be fetched from somekind of Repository
        /// Here, Just mocked the data
        /// </summary>
        /// <returns>List<Products></returns>
        private Products GetProducts()
        {
            string json = "{\"products\":[" +
                "{\"ProductId\":1,\"ProductName\":\"Meluha\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":false,\"IsBookProduct\":true,\"ProductPrice\":700,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":2,\"ProductName\":\"Sherlock Holmes\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":false,\"IsBookProduct\":true,\"ProductPrice\":500,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":3,\"ProductName\":\"Nagas\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":false,\"IsBookProduct\":true,\"ProductPrice\":900,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":4,\"ProductName\":\"Activate Membership\",\"IsPhysicalProduct\":false,\"IsMembershipProduct\":true,\"IsVideoProduct\":false,\"IsBookProduct\":false,\"ProductPrice\":500,\"ProductMembershipType\":\"Activate\"}," +
                "{\"ProductId\":5,\"ProductName\":\"Upgrade Membership\",\"IsPhysicalProduct\":false,\"IsMembershipProduct\":true,\"IsVideoProduct\":false,\"IsBookProduct\":false,\"ProductPrice\":100,\"ProductMembershipType\":\"Upgrade\"}," +
                "{\"ProductId\":6,\"ProductName\":\"Learning to Ski\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":true,\"IsBookProduct\":false,\"ProductPrice\":200,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":7,\"ProductName\":\"First Aid\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":true,\"IsBookProduct\":false,\"ProductPrice\":50,\"ProductMembershipType\":\"\"}," +
                "{\"ProductId\":8,\"ProductName\":\"Sholay\",\"IsPhysicalProduct\":true,\"IsMembershipProduct\":false,\"IsVideoProduct\":true,\"IsBookProduct\":false,\"ProductPrice\":100,\"ProductMembershipType\":\"\"}]}";

            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(json));
            ms.Position = 0;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Products));
            Products products = (Products)ser.ReadObject(ms);
            return products;
        }

        #endregion PrivateMembers
    }
}
