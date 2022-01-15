using CaseManagementApp.Models;
using CaseManagementApp.Models.Entity;
using CaseManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaseManagementApp.Views
{
    /// <summary>
    /// Interaction logic for CreateCaseView.xaml
    /// </summary>
    public partial class CreateCaseView : UserControl
    { 
        private readonly SqlService customerService = new();
        private readonly SqlService adminService = new();
        private readonly SqlService caseService = new();
        public enum states
        {
            Waiting = 0,
            Opened = 1,
            Closed = 2
        }
        

        public CreateCaseView()
        {
            InitializeComponent();
            Task.FromResult(FillCustomerOptions());
            Task.FromResult(FillAdminOptions());
            FillStausOptions();
            ArrengeComboBox();  
            
        }


        private async void CreateCase_btn_Click(object sender, RoutedEventArgs e)
        {
            await CreateCaseAsync();
        }

        private async Task CreateCaseAsync()
        {
            Case _case = new()
            {
                CustomerId = cbCustomers.SelectedIndex,
                AdminId = cbAdmins.SelectedIndex,
                Subject = tbSubject.Text,
                Description = tbDescription.Text,
                State = (Status)Enum.Parse(typeof(Status),cbState.SelectedValuePath)
            };

            if(cbCustomers.SelectedIndex != -0 && tbSubject.Text != "" && tbDescription.Text != "")
            {
                SqlService sqlService = new SqlService();
                sqlService.CreateCase(_case);
            }

        }

        private async Task PopulateOptions()
        {
            await FillCustomerOptions();
            await FillAdminOptions();
        }

        private void ArrengeComboBox()
        {
            cbCustomers.SelectedValuePath = "Key";
            cbCustomers.DisplayMemberPath = "Value";

            cbAdmins.SelectedValuePath = "Key";
            cbAdmins.DisplayMemberPath = "Value";

            cbState.SelectedValuePath = "Key";
            cbState.DisplayMemberPath = "Value";
           
        }


        private async Task FillStausOptions()
        {
            cbState.Items.Clear();
            foreach(var _state in Enum.GetValues(typeof(states)))
            {
                cbState.Items.Add(new KeyValuePair<int, string>((int)_state, _state.ToString()));
            }
        }

        private async Task FillCustomerOptions()
        {
            cbCustomers.Items.Clear();
            
            foreach(var customer in await customerService.GetCustomers())
            {
               await Task.FromResult(cbCustomers.Items.Add(new KeyValuePair<int, string>(customer.Id, $"{customer.FullName} <{customer.Email}>")));
            }
            
        }

        private async Task FillAdminOptions()
        {
            cbAdmins.Items.Clear();
            
            foreach (var admin in await adminService.GetAdmins())
            {
               await Task.FromResult(cbAdmins.Items.Add(new KeyValuePair<int, string>(admin.Id, $"{admin.Id} : {admin.FullName}")));
            }

        }

        
    }
}
