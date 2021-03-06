﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Windows;
using CustomerDemoApp.Models;

namespace CustomerDemoApp.Service
{
    public class CustomerService : ICustomerService
    {

        private CustomersDataContext db; 

        public CustomerService()
            : base()
        {
            db = new CustomersDataContext();
        }

        public List<DBCustomer> getAllCustomers()
        {
            var i = (from customer in db.Customers
                     select customer);
            return i.ToList();
        }

        public void addCustomer(DBCustomer c)
        {
            db.Customers.InsertOnSubmit(c);
            db.SubmitChanges();
        }

        public void deleteCustomer(DBCustomer c)
        {
            if (c != null)
            {
                db.Customers.Attach(c);
                db.Customers.DeleteOnSubmit(c);
                db.SubmitChanges();
            }
        }
    }
}
