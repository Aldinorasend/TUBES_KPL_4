using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Tests
{
    [TestClass()]
    public class OrderInTests
    {
        [TestMethod()]
        public void OrderInTest()
        {
            // Arrange
            var menu = new List<MenuItem>();
            var order = new OrderIn(1, menu, "Test Description");

            // Act
            var status = order.Status;

            // Assert
            Assert.AreEqual(OrderStatus.Pending, status);
        }

        [TestMethod]
        public void OrderIn_ApplyEvent_ShouldChangeStatus()
        {
            // Arrange
            var menu = new List<MenuItem>();
            var order = new OrderIn(1, menu, "Test Description");

            // Act
            order.ApplyEvent(OrderEvent.FinishProcessing);

            // Assert
            Assert.AreEqual(OrderStatus.Completed, order.Status);
        }
    }
}