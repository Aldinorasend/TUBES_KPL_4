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
            // Precondition: File path must exist and be accessible.
            Contract.Requires(File.Exists(filePath));
            // Postcondition: Returns a non-null list of customers.
            Contract.Ensures(Contract.Result<List<Customer>>() != null);

            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Customer>>(jsonData);
        }

        public Customer GetById(int id)
        {
            var customers = GetAll();
            // Postcondition: Returns a customer with the specified id if found, otherwise returns null.
            Contract.Ensures(Contract.Result<Customer>() != null || Contract.Result<Customer>() == null);
            return customers.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Customer customer)
        {
            // Precondition: Customer must not be null.
            Contract.Requires(customer != null);
            Contract.Ensures(GetAll().Contains(customer));

            var customers = GetAll();
            customers.Add(customer);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(customers));
        }

        public void Update(Customer customer)
        {
            // Precondition: Customer must not be null.
            Contract.Requires(customer != null);
            // Postcondition: Customer information is updated.
            Contract.Ensures(GetById(customer.Id) == customer);

            var customers = GetAll();
            var index = customers.FindIndex(c => c.Id == customer.Id);
            if (index != -1)
            {
                customers[index] = customer;
                File.WriteAllText(filePath, JsonConvert.SerializeObject(customers));
            }
        }

        public void Delete(int id)
        {
            var customers = GetAll();
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
                // Postcondition: Customer with the specified id is removed from the repository.
                Contract.Assert(!GetAll().Contains(customer));
                File.WriteAllText(filePath, JsonConvert.SerializeObject(customers));
            }
        }
    }
}
