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


        public CreateCaseView()
        {
            InitializeComponent();
            Task.FromResult(PopulateOptionsAsync());
            ArrengeComboBox();
            GetCustomerList();
            
            
        }

        public async void GetCustomerList()
        {
            List<CustomerEntity> customerList = new();

            foreach(var customer in await customerService.GetCustomersAsync())
            {
                customerList.Add(customer);
            }

        }

        private async void CreateCase_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (cbState.SelectedValue == null)
            {
                lblError.Content = "Please select status.";
            }

            else if(cbCustomers.SelectedIndex == -1 && tbSubject.Text == "" && tbDescription.Text == "")
            {
                lblError.Content = "Pleae check the fields.";
            }

            else
            {
                await CreateCaseAsync();
            }
        }

        private async Task CreateCaseAsync()
        {

           
            var customerId = (int)cbCustomers.SelectedValue;
            var customer = customerService.GetCustomer(customerId);

            var adminId = (int)cbAdmins.SelectedValue;
            var admin = adminService.GetAdmin(adminId);
            

            Case _case = new()
            {
                Customer = customer,
                Admin = admin,
                Subject = tbSubject.Text,
                Description = tbDescription.Text,              
                State = (Status)Enum.Parse(typeof(Status),cbState.SelectedValue.ToString())
            };

                SqlService sqlService = new();
                sqlService.CreateCase(_case);
                ClearFields();
    
        }

        private async Task PopulateOptionsAsync()
        {
            await FillCustomerOptionsAsync();
            await FillAdminOptionsAsync();
            await FillStausOptionsAsync();
        }

        private void ArrengeComboBox()
        {
            cbCustomers.SelectedValuePath = "Key";
            cbCustomers.DisplayMemberPath = "Value";

            cbAdmins.SelectedValuePath = "Key";
            cbAdmins.DisplayMemberPath = "Value";

            cbState.SelectedValuePath = "Key";
            cbState.DisplayMemberPath= "Value";
        }


        private async Task FillStausOptionsAsync()
        {
            cbState.Items.Clear();
            foreach(var _state in await Task.FromResult(Enum.GetValues(typeof(Status))))
            {
                cbState.Items.Add(new KeyValuePair<int, string>((int)_state, _state.ToString()));
            }
        }

        private async Task FillCustomerOptionsAsync()
        {
            cbCustomers.Items.Clear();
            
            foreach (var customer in await customerService.GetCustomersAsync())
            {
                cbCustomers.Items.Add(new KeyValuePair<int, string>(customer.Id, customer.FirstName));      
                //cbCustomers.Items.Add(customer);
            }

            
            
        }

        private async Task FillAdminOptionsAsync()
        {
            cbAdmins.Items.Clear();
            
            
            foreach (var admin in await adminService.GetAdminsAsync())
            {
                cbAdmins.Items.Add(new KeyValuePair<int, string>(admin.Id, $"{admin.Id} : {admin.FirstName} {admin.LastName}"));
                //cbAdmins.Items.Add(admin);
            }
            
        }

        private void ClearFields()
        {
            tbSubject.Text = "";
            tbDescription.Text = "";
            cbCustomers.SelectedIndex = -1;
            cbAdmins.SelectedIndex = -1;
            cbState.SelectedIndex = -1;
            lblError.Content = string.Empty;
        }
        
    }
}
