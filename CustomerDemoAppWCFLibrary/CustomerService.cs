using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Windows;

namespace CustomerDemoApp.Service
{
    public class CustomerService : ICustomerService
    {
        CustomersDataContext db = new CustomersDataContext();
        public CustomerService()
            : base()
        {

        }

        public List<Customer> getAllCustomers()
        {
            var i = (from customer in db.Customers
                     select customer);
            return i.ToList();
        }

        public void addCustomer(Customer customer)
        {
            db.Customers.InsertOnSubmit(customer);
            db.SubmitChanges();
        }

        public void deleteCustomer(Customer customer)
        {
            db.Customers.DeleteOnSubmit(customer);
            db.SubmitChanges();
        }
    }
}
