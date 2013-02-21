using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CustomerDemoApp.Properties;
using System.Collections.Generic;

namespace CustomerDemoApp.Model
{




    /// <summary>
    /// Basic customer model with information for the Mexican market.
    /// </summary>
    public class Customer : IDataErrorInfo
    {

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Curp { get; set; }

        private Dictionary<string, Func<string>> validators;


        public Customer(string firstName, string lastName, string email, string curp) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Curp = curp;
        }

        public Customer()
        {
            validators = new Dictionary<string, Func<string>>()
            { 
                {"Email", ValidateEmail}, 
                {"FirstName", ValidateFirstName}, 
                {"LastName", ValidateLastName},
                {"Curp", ValidateCurp},
            };
        }


        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                // TODO: Use a Filter-Reduce scheme
                foreach (string validatedProperty in validators.Keys)
                    if (GetValidationError(validatedProperty) != null)
                        return false;

                return true;
            }
        }

        string GetValidationError(string propertyName)
        {
            Func<string> validator;
            if(validators.TryGetValue(propertyName, out validator)) {
                return validator();
            }

            return null;
        }

        
        string ValidateEmail()
        {
            if (IsStringMissing(this.Email))
            {
                return StringsConstants.Customer_Error_MissingEmail;
            }
            else if (!IsValidEmailAddress(this.Email))
            {
                return StringsConstants.Customer_Error_InvalidEmail;
            }
            return null;
        }

        string ValidateFirstName()
        {
            if (IsStringMissing(this.FirstName))
            {
                return StringsConstants.Customer_Error_MissingFirstName;
            }
            return null;
        }

        string ValidateLastName()
        {
            if (IsStringMissing(this.LastName))
            {
                return StringsConstants.Customer_Error_MissingLastName;
            }
            return null;
        }

        string ValidateCurp()
        {
            if(IsStringMissing(this.Curp))
            {
                return StringsConstants.Customer_Error_MissingCurp;
            }
            return null;
        }

        static bool IsStringMissing(string value)
        {
            return
                String.IsNullOrEmpty(value) ||
                value.Trim() == String.Empty;
        }

        static bool IsValidEmailAddress(string email)
        {
            if (IsStringMissing(email))
                return false;

            // Regex from: http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
            // If you really try you will probably break it
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return this.Curp.Equals(((Customer)obj).Curp);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}