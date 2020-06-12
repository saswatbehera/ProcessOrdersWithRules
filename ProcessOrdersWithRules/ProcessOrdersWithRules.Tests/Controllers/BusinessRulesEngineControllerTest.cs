using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessOrdersWithRules;
using ProcessOrdersWithRules.Controllers;

namespace ProcessOrdersWithRules.Tests.Controllers
{
    [TestClass]
    public class BusinessRulesEngineControllerTest
    {
        [TestMethod]
        public void ProcessOrder()
        {
            // Arrange
            BusinessRulesEngineController controller = new BusinessRulesEngineController();

            // Act
            string strMessage = controller.ProcessOrder();

            // Assert
            Assert.AreEqual(strMessage, "Order Processed Sucessfully");
        }
    }
}
