using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemoApp.Models
{
    public interface ICustomerBase
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Curp { get; set; }
    }
}
