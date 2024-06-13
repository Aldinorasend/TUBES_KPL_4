using System;
using System.Collections.Generic;

namespace MenuManager
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public MenuItem() { }
        public MenuItem(int id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
    public class MenuManager<T> where T : MenuItem
    {
        private List<T> items = new List<T>();
        private int nextId = 1;

        public void AddItem(int v, string name, int price)
        {
            var item = (T)Activator.CreateInstance(typeof(T));
            item.Id = nextId++;
            item.Name = name;
            item.Price = price;
            items.Add(item);
        }

        public void UpdateItem(int id, string name, int price)
        {
            var item = items.Find(i => i.Id == id);
            if (item != null)
            {
                item.Name = name;
                item.Price = price;
            }
            else
            {
                Console.WriteLine($"Item dengan ID {id} tidak ditemukan.");
            }
        }

        public List<T> GetItems()
        {
            return items;
        }

        public void RemoveItem(int id)
        {
            var item = items.Find(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
            }
            else
            {
                Console.WriteLine($"Item dengan ID {id} tidak ditemukan.");
            }
        }

        public void DisplayMenu()
        {
            Console.WriteLine("Daftar Menu:");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Nama: {item.Name}, Harga: {item.Price:C}");
            }
        }
    }
}
