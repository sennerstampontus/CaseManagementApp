using CaseManagementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Models.ViewModels
{
    internal class CustomerViewInfo
    {
        public AddressEntity Address { get; set; }
        public CustomerEntity Customer { get; set; }
        
    }
}
