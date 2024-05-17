using Microsoft.VisualStudio.TestTools.UnitTesting;
using MenuManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuManager.Tests
{
    [TestClass()]
    public class MenuManagerTests
    {
        private MenuManager<MenuItem> itemManager;
        [TestInitialize]
        public void Initialize()
        {
            itemManager = new MenuManager<MenuItem>();
        }

        [TestMethod()]
        public void AddItemTest_CorrectValues()
        {
            // Arrange
            var id = 1;
            var name = "Test Item";
            var price = 100;

            // Act
            itemManager.AddItem(id, name, price);
            var items = itemManager.GetItems();

            // Assert
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(1, items[0].Id);
            Assert.AreEqual(name, items[0].Name);
            Assert.AreEqual(price, items[0].Price);
        }

        [TestMethod()]
        public void AddItemTestHarusIncrement()
        {
           
            var id1 = 1;
            
            var name1 = "Test Item 1";
            var price1 = 100;
            var id2 = 2;
            var name2 = "Test Item 2";
            var price2 = 200;

            
            itemManager.AddItem(id1,name1, price1);
            itemManager.AddItem(id1,name2, price2);
            var items = itemManager.GetItems();

            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(1, items[0].Id);
            Assert.AreEqual(2, items[1].Id);
        }

      

    
}

   
}