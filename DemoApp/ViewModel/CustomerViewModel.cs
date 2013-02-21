using System;
using System.ComponentModel;
using System.Windows.Input;
using CustomerDemoApp.DataAccess;
using CustomerDemoApp.Models;
using CustomerDemoApp.Properties;

namespace CustomerDemoApp.ViewModel
{
    /// <summary>
    /// A UI-friendly wrapper for a Customer object.
    /// </summary>
    public class CustomerViewModel : WorkspaceViewModel, IDataErrorInfo
    {

        readonly FECustomer _customer;
        readonly CustomerRepository _customerRepository;
        RelayCommand _saveCommand;


        public CustomerViewModel(FECustomer customer, CustomerRepository customerRepository)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (customerRepository == null)
                throw new ArgumentNullException("customerRepository");

            _customer = customer;
            _customerRepository = customerRepository;
        }

        public string Email
        {
            get { return _customer.Email; }
            set
            {
                _customer.Email = value;
                base.OnPropertyChanged("Email");
            }
        }

        public string FirstName
        {
            get { return _customer.FirstName; }
            set
            {
                _customer.FirstName = value;

                base.OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return _customer.LastName; }
            set
            {
                _customer.LastName = value;

                base.OnPropertyChanged("LastName");
            }
        }

        public string Curp
        {
            get { return _customer.Curp; }
            set
            {
                _customer.Curp = value;
                base.OnPropertyChanged("LastName");
            }
        }

        public override string DisplayName
        {
            get
            {
                if (this.IsNewCustomer)
                {
                    return StringsConstants.CustomerViewModel_DisplayName;
                }
                else
                {
                    return String.Format("{0}, {1}", _customer.LastName, _customer.FirstName);
                }
            }
        }

        /// <summary>
        /// Returns a command that saves the customer.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.Save(),
                        param => this.CanSave
                        );
                }
                return _saveCommand;
            }
        }

        /// <summary>
        /// Saves the customer to the repository.  This method is invoked by the SaveCommand.
        /// </summary>
        public void Save()
        {
            if (!_customer.IsValid)
                throw new InvalidOperationException(StringsConstants.CustomerViewModel_Exception_CannotSave);

            if (this.IsNewCustomer)
                _customerRepository.AddCustomer(_customer);
            
            base.OnPropertyChanged("DisplayName");
        }

        /// <summary>
        /// Returns true if this customer was created by the user and it has not yet
        /// been saved to the customer repository.
        /// </summary>
        bool IsNewCustomer
        {
            get { return !_customerRepository.ContainsCustomer(_customer); }
        }

        /// <summary>
        /// Returns true if the customer is valid and can be saved.
        /// </summary>
        bool CanSave
        {
            get {
                return _customer.IsValid; 
            }
        }

        string IDataErrorInfo.Error
        {
            get { return (_customer as IDataErrorInfo).Error; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = (_customer as IDataErrorInfo)[propertyName];
                // Dirty the commands registered with CommandManager,
                // such as our Save command, so that they are queried
                // to see if they can execute now.
                CommandManager.InvalidateRequerySuggested();

                return error;
            }
        }
    }
}