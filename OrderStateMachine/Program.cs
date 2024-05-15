using MenuManager;
using OrderLibrary;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderStateMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menuManager = new MenuManager<MenuManager.MenuItem>();

            menuManager.AddItem(1, "Nasi Goreng", 15000);
            menuManager.AddItem(2, "Mie Goreng", 12000);
            menuManager.DisplayMenu();

            /*menuManager.RemoveItem(1);
            menuManager.DisplayMenu();*/
            // Convert MenuManager.MenuItem to OrderProcessing.MenuItem
            var menuItems = new List<OrderLibrary.MenuItem>();
            foreach (var item in menuManager.GetItems())
            {
                menuItems.Add(new OrderLibrary.MenuItem(item.Id, item.Name, item.Price));
            }

            var processor = new OrderProcessor<OrderLibrary.OrderIn>(menuItems);

            // Create orders
            processor.CreateOrder(new int[] { 1, 2 }, "Order 1");
            processor.CreateOrder(new int[] { 3 }, "Order 2");

            // Display active orders
            processor.DisplayOrders();

            // Update order status
            processor.UpdateOrderStatus(1, OrderLibrary.OrderEvent.FinishProcessing); // Processing
            processor.UpdateOrderStatus(1, OrderLibrary.OrderEvent.FinishProcessing); // Completed
            processor.UpdateOrderStatus(2, OrderLibrary.OrderEvent.CancelOrder);      // Cancelled

            // Display updated orders
            processor.DisplayOrders();
            Console.Read();

        }
    }
}