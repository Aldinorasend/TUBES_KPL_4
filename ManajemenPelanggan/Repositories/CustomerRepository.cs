using ManajemenPelanggan.Models;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;


namespace ManajemenPelanggan.Repositories
{
    public class CustomerRepository
    {
        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "customers.json");

        public List<Customer> GetAll()
        {
            Contract.Requires(File.Exists(filePath));
            Contract.Ensures(Contract.Result<List<Customer>>() != null);

            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Customer>>(jsonData);
        }


        public void Add(Customer customer)
        {
            Contract.Requires(customer != null);
            Contract.Ensures(GetAll().Contains(customer));

            var customers = GetAll();
            customers.Add(customer);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(customers));
        }
    }
}