using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CustomerDemoApp.Service
{
    [ServiceContract(Name = "CustomerService")]
    public interface ICustomerService
    {
        [OperationContract]
        List<Customer> getAllCustomers();

        [OperationContract]
        void addCustomer(Customer customer);

        [OperationContract]
        void deleteCustomer(Customer customer);
    }
}