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

            Product p = new Product();

            Executables exq = new Executables();
            BusinessRulesEngineController controller = new BusinessRulesEngineController();

            // Act
            string strMessage = controller.ProcessOrder(orderId,IsExecute);

            // Assert
            Assert.AreEqual(strMessage, "Order Processed Sucessfully");
        }
    }
}
