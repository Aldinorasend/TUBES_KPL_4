﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OrderProcessing
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        public List<MenuItem> Menu { get; set; }
        public string Description { get; set; }
        public OrderStatus Status { get; set; }

        public Order(int idOrder, List<MenuItem> menu, string description)
        {
            Id = idOrder;
            Menu = menu;
            Description = description;
            Status = OrderStatus.Pending;
        }
    }

    public class OrderProcessor<T> where T : Order
    {
        private List<T> orders = new List<T>();
        private int nextId = 1;
        private List<MenuItem> availableMenu;

        public OrderProcessor(List<MenuItem> menu)
        {
            availableMenu = menu;
        }

        public void CreateOrder(int[] menuIds, string description)
        {
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

            var order = (T)Activator.CreateInstance(typeof(T), nextId++, selectedMenu, description);
            orders.Add(order);
        }

        public List<T> GetActiveOrders()
        {
            return orders.Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Processing).ToList();
        }

        public void UpdateOrderStatus(int id, OrderStatus newStatus)
        {
            var order = orders.Find(o => o.Id == id);
            if (order != null)
            {
                order.Status = newStatus;
            }
            else
            {
                Console.WriteLine($"Pesanan dengan ID {id} tidak ditemukan.");
            }
        }

        public void DisplayOrders()
        {
            Console.WriteLine("Daftar Pesanan:");
            foreach (var order in orders)
            {
                Console.WriteLine($"ID: {order.Id}, Deskripsi: {order.Description}, Status: {order.Status}");
                Console.WriteLine(" Menu:");
                foreach (var menuItem in order.Menu)
                {
                    Console.WriteLine($"  - ID: {menuItem.Id}, Nama: {menuItem.Name}, Harga: {menuItem.Price:C}");
                }
            }
        }
    }

    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public MenuItem(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}