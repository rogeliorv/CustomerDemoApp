using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;
using CustomerDemoApp.Models;
using CustomerDemoApp.ServiceReference;

namespace CustomerDemoApp.DataAccess
{
    /// <summary>
    /// Represents a source of customers in the application.
    /// </summary>
    public class CustomerRepository
    {
        readonly List<FECustomer> _customers;
        private static CustomerServiceClient client = new CustomerServiceClient();

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
        public void AddCustomer(FECustomer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (!_customers.Contains(customer))
            {
                // TODO: Use static method
                client.addCustomer(new DBCustomer { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName, Curp = customer.Curp, Email = customer.Email} );
                _customers.Add(customer);


                if (this.CustomerAdded != null)
                    this.CustomerAdded(this, new CustomerAddedEventArgs(customer));
            }
        }

        /// <summary>
        /// Returns true if the specified customer exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool ContainsCustomer(FECustomer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            return _customers.Contains(customer);
        }

        /// <summary>
        /// Returns a shallow-copied list of all customers in the repository.
        /// </summary>
        public List<FECustomer> GetCustomers()
        {
            return new List<FECustomer>(_customers);
        }

        static List<FECustomer> LoadCustomers()
        {
            return (from e in client.getAllCustomers() select FECustomer.FromBaseCustomer(e)).ToList();
        }
    }
}