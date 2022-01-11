using CaseManagementApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Models.ViewModels
{
    internal class MainWindowViewModel : ObservableObject
    {
        public RelayCommand CreateCustomerCommand { get; set; }
        public RelayCommand CreateAdminCommand { get; set; }
    }
}
