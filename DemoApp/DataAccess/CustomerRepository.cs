using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;
using CustomerDemoApp.Model;


namespace CustomerDemoApp.DataAccess
{
    /// <summary>
    /// Represents a source of customers in the application.
    /// </summary>
    public class CustomerRepository
    {
        readonly List<CustomerDemoApp.Model.Customer> _customers;

        /// <summary>
        /// Creates a new repository of customers.
        /// </summary>
        /// <param name="customerDataFile">The relative path to an XML resource file that contains customer data.</param>
        public CustomerRepository()
        {
            _customers = LoadCustomers();
        }


        /// <summary>
        /// Raised when a customer is placed into the repository.
        /// </summary>
        public event EventHandler<CustomerAddedEventArgs> CustomerAdded;

        /// <summary>
        /// Places the specified customer into the repository.
        /// If the customer is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (!_customers.Contains(customer))
            {
                // TODO: Make a common interface for wpf model and linqsql model
                CustomerDemoApp.ServiceReference.CustomerServiceClient client = new CustomerDemoApp.ServiceReference.CustomerServiceClient();

                client.addCustomer(new CustomerDemoApp.ServiceReference.Customer { FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email, Curp = customer.Curp });
                _customers.Add(customer);


                if (this.CustomerAdded != null)
                    this.CustomerAdded(this, new CustomerAddedEventArgs(customer));
            }
        }

        /// <summary>
        /// Returns true if the specified customer exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            return _customers.Contains(customer);
        }

        /// <summary>
        /// Returns a shallow-copied list of all customers in the repository.
        /// </summary>
        public List<Customer> GetCustomers()
        {
            return new List<Customer>(_customers);
        }

        static List<Customer> LoadCustomers()
        {
            CustomerDemoApp.ServiceReference.CustomerServiceClient client = new CustomerDemoApp.ServiceReference.CustomerServiceClient();
            return (from e in client.getAllCustomers()
                    select new Customer { FirstName = e.FirstName, LastName = e.LastName, Email = e.Email, Curp = e.Curp }).ToList();
        }
    }
}