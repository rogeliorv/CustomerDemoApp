using System;
using CustomerDemoApp.Models;

namespace CustomerDemoApp.DataAccess
{
    /// <summary>
    /// Event arguments used by CustomerRepository's CustomerAdded event.
    /// </summary>
    public class CustomerAddedEventArgs : EventArgs
    {
        public CustomerAddedEventArgs(FECustomer newCustomer)
        {
            this.NewCustomer = newCustomer;
        }

        public FECustomer NewCustomer { get; private set; }
    }
}