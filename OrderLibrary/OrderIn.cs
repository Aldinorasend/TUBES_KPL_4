﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ReportManagement;

namespace OrderLibrary
{
    public class OrderProcessor<T> where T : OrderIn
    {
        private List<T> orders = new List<T>();
        private List<MenuItem> availableMenu;
        private TransactionManager transactionManager;

        public OrderProcessor(List<MenuItem> menu, TransactionManager transactionMgr)
        {
            Contract.Requires(menu != null);
            Contract.Requires(transactionMgr != null);
            
            availableMenu = menu;
            transactionManager = transactionMgr;
        }

        public void CreateOrder(int orderId, int[] menuIds, string description)
        {
            Contract.Requires(menuIds != null && menuIds.Length > 0);
            Contract.Requires(!string.IsNullOrEmpty(description));
            Contract.Ensures(orders.Exists(o => o.Id == orderId));

            var selectedMenu = new List<MenuItem>();
            foreach (var id in menuIds)
            {
                var menu = availableMenu.FirstOrDefault(m => m.Id == id);
                if (menu != null)
                {
                    selectedMenu.Add(menu);
                }
                else
                {
                    Console.WriteLine($"Menu dengan ID {id} tidak ditemukan.");
                    return;
                }
            }

            var order = (T)Activator.CreateInstance(typeof(T), orderId, selectedMenu, description);
            orders.Add(order);
        }

        public List<T> GetActiveOrders()
        {
            Contract.Ensures(Contract.Result<List<T>>() != null);
            return orders.Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Processing).ToList();
        }

        public void ProcessOrderToTransaction(int orderId, int amountPaid)
        {
            Contract.Requires(amountPaid >= 0);

            var order = orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                var subtotal = order.Menu.Sum(m => m.Price);
                transactionManager.GenerateReport(orderId, DateTime.Now, orderId, subtotal, amountPaid);
            }
            else
            {
                Console.WriteLine($"Pesanan dengan ID {orderId} tidak ditemukan.");
            }
        }

        public void UpdateOrderStatus(int id, OrderEvent orderEvent)
        {
            Contract.Requires(orderEvent != null);

            var order = orders.Find(o => o.Id == id);
            if (order != null)
            {
                if (!order.ApplyEvent(orderEvent))
                {
                    Console.WriteLine($"Transisi status tidak valid untuk pesanan dengan ID {id}. Status saat ini: {order.Status}");
                }
            }
            else
            {
                Console.WriteLine($"Pesanan dengan ID {id} tidak ditemukan.");
            }
        }

        public void DisplayOrders()
        {
            Contract.Ensures(orders != null);
            Console.WriteLine("Daftar Pesanan:");
            foreach (var order in orders)
            {
                Console.WriteLine($"==================== Order ID {order.Id} ===============================");
                Console.WriteLine($"Deskripsi: {order.Description}, Status: {order.Status}");
                Console.WriteLine(" Menu:");
                foreach (var menuItem in order.Menu)
                {
                    Console.WriteLine($"  - ID: {menuItem.Id}, Nama: {menuItem.Name}, Harga: {menuItem.Price:C}");
                }
                Console.WriteLine($"===============================================================");
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(orders != null);
            Contract.Invariant(availableMenu != null);
            Contract.Invariant(transactionManager != null);
        }
    }

    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public MenuItem(int id, string name, int price)
        {
            Contract.Requires(id >= 0);
            Contract.Requires(!string.IsNullOrEmpty(name));
            Contract.Requires(price >= 0);

            Id = id;
            Name = name;
            Price = price;
        }
    }
}
