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
        }


        /// <summary>
        /// Hämtar ut den markerade Customer i ComboBoxen från Databasen.
        /// </summary>
        /// <returns></returns>
        private async Task<CustomerEntity> GetCustomerAsync()
        {
            var customerId = (int)cbCustomers.SelectedValue;
            var customer = await customerService.GetCustomerDbAsync(customerId);

            return customer;
        }


        /// <summary>
        /// Hämtar ut den markerade Admin i ComboBoxen från Databasen.
        /// </summary>
        /// <returns></returns>
        private async Task<AdminEntity> GetAdminAsync()
        {
            var adminId = (int)cbAdmins.SelectedValue;
            var admin = await adminService.GetAdminDbAsync(adminId);

            return admin;
        }

        private async void CreateCase_btn_Click(object sender, RoutedEventArgs e)
        {
            var customer = await GetCustomerAsync();
            var admin = await GetAdminAsync();

            if (cbState.SelectedValue == null)
            {
                lblError.Content = "Please select status.";
            }

            else if(cbCustomers.SelectedValue == null && tbSubject.Text == "" && tbDescription.Text == "")
            {
                lblError.Content = "Pleae check the fields.";
            }

            else
            {
                await CreateCaseAsync(customer, admin);
            }
        }

        /// <summary>
        /// Skapar ett nytt Case med de inmatade värdena.<br></br>
        /// Samt tar in vilken kund och handläggare ärendet tillhör.
        /// </summary>
        private async Task CreateCaseAsync(CustomerEntity customer, AdminEntity admin)
        {
            Case _case = new()
            {
                Customer = customer,
                Admin = admin,
                Subject = tbSubject.Text,
                Description = tbDescription.Text,              
                State = (Status)Enum.Parse(typeof(Status),cbState.SelectedValue.ToString())
            };

                SqlService sqlService = new();
                await sqlService.SaveDbCaseAsync(_case);
                ClearFields();
    
        }


        /// <summary>
        /// Anropar alla metoder som populerar ComboBoxarna.
        /// </summary>
        /// <returns></returns>
        private async Task PopulateOptionsAsync()
        {
            await FillCustomerOptionsAsync();
            await FillAdminOptionsAsync();
            await FillStausOptionsAsync();
        }


        /// <summary>
        /// Ordnar upp vyn för ComboBoxarna. 
        /// </summary>
        private void ArrengeComboBox()
        {
            cbCustomers.SelectedValuePath = "Key";
            cbCustomers.DisplayMemberPath = "Value";

            cbAdmins.SelectedValuePath = "Key";
            cbAdmins.DisplayMemberPath = "Value";

            cbState.SelectedValuePath = "Key";
            cbState.DisplayMemberPath= "Value";
        }

        /// <summary>
        /// Fyller på med alla Status state i ComboBoxen.
        /// </summary>
        /// <returns></returns>
        private async Task FillStausOptionsAsync()
        {
            cbState.Items.Clear();
            foreach(var _state in await Task.FromResult(Enum.GetValues(typeof(Status))))
            {
                cbState.Items.Add(new KeyValuePair<int, string>((int)_state, _state.ToString()));
            }
        }

        /// <summary>
        /// Fyller på med alla Customers i ComboBoxen.
        /// </summary>
        /// <returns></returns>
        private async Task FillCustomerOptionsAsync()
        {
            cbCustomers.Items.Clear();
            
            foreach (var customer in await customerService.GetCustomersAsync())
            {
                cbCustomers.Items.Add(new KeyValuePair<int, string>(customer.Id, $"{customer.FullName} <{customer.Email}>"));      
            }                  
        }

        /// <summary>
        /// Fyller på med alla admins i ComboBoxen.
        /// </summary>
        /// <returns></returns>
        private async Task FillAdminOptionsAsync()
        {
            cbAdmins.Items.Clear();
            
            
            foreach (var admin in await adminService.GetAdminsAsync())
            {
                cbAdmins.Items.Add(new KeyValuePair<int, string>(admin.Id, $"{admin.Id} : {admin.FirstName} {admin.LastName}"));
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
