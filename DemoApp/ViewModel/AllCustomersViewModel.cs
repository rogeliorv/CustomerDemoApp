using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CustomerDemoApp.DataAccess;
using CustomerDemoApp.Properties;

namespace CustomerDemoApp.ViewModel
{
    /// <summary>
    /// Represents a container of CustomerViewModel objects
    /// that has support for staying synchronized with the
    /// CustomerRepository.  This class also provides information
    /// related to multiple selected customers.
    /// </summary>
    public class AllCustomersViewModel : WorkspaceViewModel
    {
        readonly CustomerRepository _customerRepository;

        public AllCustomersViewModel(CustomerRepository customerRepository)
        {
            if (customerRepository == null)
                throw new ArgumentNullException("customerRepository");

            base.DisplayName = StringsConstants.AllCustomersViewModel_DisplayName;            

            _customerRepository = customerRepository;

            // Subscribe for notifications of when a new customer is saved.
            _customerRepository.CustomerAdded += this.OnCustomerAddedToRepository;

            // Populate the AllCustomers collection with CustomerViewModels.
            this.CreateAllCustomers();              
        }

        void CreateAllCustomers()
        {
            List<CustomerViewModel> all =
                (from cust in _customerRepository.GetCustomers()
                 select new CustomerViewModel(cust, _customerRepository)).ToList();

            foreach (CustomerViewModel cvm in all)
                cvm.PropertyChanged += this.OnCustomerViewModelPropertyChanged;

            this.AllCustomers = new ObservableCollection<CustomerViewModel>(all);
            this.AllCustomers.CollectionChanged += this.OnCollectionChanged;
        }

        /// <summary>
        /// Returns a collection of all the CustomerViewModel objects.
        /// </summary>
        public ObservableCollection<CustomerViewModel> AllCustomers { get; private set; }

        protected override void OnDispose()
        {
            foreach (CustomerViewModel custVM in this.AllCustomers)
                custVM.Dispose();

            this.AllCustomers.Clear();
            this.AllCustomers.CollectionChanged -= this.OnCollectionChanged;

            _customerRepository.CustomerAdded -= this.OnCustomerAddedToRepository;
        }


        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (CustomerViewModel custVM in e.NewItems)
                    custVM.PropertyChanged += this.OnCustomerViewModelPropertyChanged;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (CustomerViewModel custVM in e.OldItems)
                    custVM.PropertyChanged -= this.OnCustomerViewModelPropertyChanged;
        }

        void OnCustomerViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string IsSelected = "IsSelected";

            // Make sure that the property name we're referencing is valid.
            // This is a debugging technique, and does not execute in a Release build.
            (sender as CustomerViewModel).VerifyPropertyName(IsSelected);
            // Some logic can be done here
        }

        void OnCustomerAddedToRepository(object sender, CustomerAddedEventArgs e)
        {
            var viewModel = new CustomerViewModel(e.NewCustomer, _customerRepository);
            this.AllCustomers.Add(viewModel);
        }

    }
}