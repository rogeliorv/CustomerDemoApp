#pragma warning disable 1591

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ComponentModel;
using System;


// This code is an extension of the generated LINQ-to-SQL code
// Changes are mostly for the Customer class so the model can be reused 
// avoiding code duplication in backend-frontend
namespace CustomerDemoApp.Models
{
    [global::System.Data.Linq.Mapping.DatabaseAttribute(Name = "CustomerDemoApp")]
    public partial class CustomersDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void InsertCustomer(DBCustomer instance);
        partial void UpdateCustomer(DBCustomer instance);
        partial void DeleteCustomer(DBCustomer instance);
        #endregion

        public CustomersDataContext() :
            base(global::CustomerDemoApp.Properties.Settings.Default.CustomerDemoAppConnectionString, mappingSource)
        {
            OnCreated();
        }

        public CustomersDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public CustomersDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public CustomersDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public CustomersDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public System.Data.Linq.Table<DBCustomer> Customers
        {
            get
            {
                return this.GetTable<DBCustomer>();
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Customer")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class DBCustomer : INotifyPropertyChanging, INotifyPropertyChanged, ICustomerBase
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _FirstName;

        private string _LastName;

        private string _Email;

        private string _Curp;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnFirstNameChanging(string value);
        partial void OnFirstNameChanged();
        partial void OnLastNameChanging(string value);
        partial void OnLastNameChanged();
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        partial void OnCurpChanging(string value);
        partial void OnCurpChanged();
        #endregion

        public DBCustomer()
        {
            this.Initialize();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FirstName", DbType = "VarChar(255)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                if ((this._FirstName != value))
                {
                    this.OnFirstNameChanging(value);
                    this.SendPropertyChanging();
                    this._FirstName = value;
                    this.SendPropertyChanged("FirstName");
                    this.OnFirstNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastName", DbType = "VarChar(255)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                if ((this._LastName != value))
                {
                    this.OnLastNameChanging(value);
                    this.SendPropertyChanging();
                    this._LastName = value;
                    this.SendPropertyChanged("LastName");
                    this.OnLastNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "VarChar(255)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                if ((this._Email != value))
                {
                    this.OnEmailChanging(value);
                    this.SendPropertyChanging();
                    this._Email = value;
                    this.SendPropertyChanged("Email");
                    this.OnEmailChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Curp", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string Curp
        {
            get
            {
                return this._Curp;
            }
            set
            {
                if ((this._Curp != value))
                {
                    this.OnCurpChanging(value);
                    this.SendPropertyChanging();
                    this._Curp = value;
                    this.SendPropertyChanged("Curp");
                    this.OnCurpChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Initialize()
        {
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }

        public static DBCustomer FromFECustomer(FECustomer c)
        {
            return new DBCustomer { FirstName = c.FirstName, LastName = c.LastName, Email = c.Email, Curp = c.Curp };
        }
    }
}
#pragma warning restore 1591
