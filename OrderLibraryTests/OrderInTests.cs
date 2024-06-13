using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace OrderLibrary.Tests
{
    [TestClass]
    public class OrderInTests
    {
        [TestMethod]
        public void OrderIn_Constructor_ValidParameters_ShouldInitializeCorrectly()
        {
            // Arrange
            int idOrder = 1;
            var menu = new List<MenuItem> { new MenuItem(1, "Pizza", 10) };
            string description = "Test Order";

            // Act
            var order = new OrderIn(idOrder, menu, description);

            // Assert
            Assert.AreEqual(idOrder, order.Id);
            CollectionAssert.AreEqual(menu, order.Menu);
            Assert.AreEqual(description, order.Description);
            Assert.AreEqual(OrderStatus.Pending, order.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderIn_Constructor_InvalidId_ShouldThrowException()
        {
            // Arrange
            int idOrder = 0; // Invalid ID
            var menu = new List<MenuItem> { new MenuItem(1, "Pizza", 10) };
            string description = "Test Order";

            // Act
            var order = new OrderIn(idOrder, menu, description);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrderIn_Constructor_NullMenu_ShouldThrowException()
        {
            // Arrange
            int idOrder = 1;
            List<MenuItem> menu = null; // Null menu
            string description = "Test Order";

            // Act
            var order = new OrderIn(idOrder, menu, description);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderIn_Constructor_EmptyDescription_ShouldThrowException()
        {
            // Arrange
            int idOrder = 1;
            var menu = new List<MenuItem> { new MenuItem(1, "Pizza", 10) };
            string description = ""; // Invalid description

            // Act
            var order = new OrderIn(idOrder, menu, description);
        }

        [TestMethod]
        public void ApplyEvent_ValidEvent_ShouldTransitionState()
        {
            // Arrange
            int idOrder = 1;
            var menu = new List<MenuItem> { new MenuItem(1, "Pizza", 10) };
            string description = "Test Order";
            var order = new OrderIn(idOrder, menu, description);

            // Act
            bool result = order.ApplyEvent(OrderEvent.FinishProcessing);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(OrderStatus.Completed, order.Status);
        }

        [TestMethod]
        public void ApplyEvent_InvalidEvent_ShouldNotTransitionState()
        {
            // Arrange
            int idOrder = 1;
            var menu = new List<MenuItem> { new MenuItem(1, "Pizza", 10) };
            string description = "Test Order";
            var order = new OrderIn(idOrder, menu, description);

            // Act
            bool result = order.ApplyEvent((OrderEvent)999); // Invalid event

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(OrderStatus.Pending, order.Status);
        }
    }
}
