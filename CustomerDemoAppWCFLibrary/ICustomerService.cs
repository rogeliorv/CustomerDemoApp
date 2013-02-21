using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CustomerDemoApp.Models;


namespace CustomerDemoApp.Service
{
    [ServiceContract(Name = "CustomerService")]
    public interface ICustomerService
    {
        [OperationContract]
        List<DBCustomer> getAllCustomers();

        [OperationContract]
        void addCustomer(DBCustomer customer);

        [OperationContract]
        void deleteCustomer(DBCustomer customer);
    }
}