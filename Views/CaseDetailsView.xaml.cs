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

    enum ToggleState
    {
        EditCase,
        SaveCase
    }

    /// <summary>
    /// Interaction logic for CaseDetailsView.xaml
    /// </summary>
    public partial class CaseDetailsView : UserControl
    {

        private readonly SqlService customerService = new();
        private readonly SqlService adminService = new();
        private readonly SqlService caseService = new();


        public CaseDetailsView()
        {
            InitializeComponent();
            GetLists();
            PopulateComboBoxes();
        }

      

        private async void PopulateComboBoxes()
        {
            await FillCustomerOptionsAsync();
            await FillAdminOptionsAsync();
            await FillStausOptionsAsync();
            ArrengeComboBox();
        }

        private void GetLists()
        {
            GetCustomerList();
            GetAdminList();
        }

        private void SetValues(CaseEntity caseResult)
        {
            lblCaseId.Content = caseResult.CaseId;
            lblSubject.Content = caseResult.Subject;
            tbDescription.Text = caseResult.Description;
            lblAdmins.Content = caseResult.Admin.FullName;
            lblState.Content = caseResult.State;

        }

        private void GetCase()
        {
            var caseId = (int)cbCustomersCase.SelectedValue;
            var _case = caseService.GetCase(caseId);

            SetValues(_case);

        }

        private async void GetCustomerList()
        {
            List<CustomerEntity> customerList = new();
            foreach(var customer in await customerService.GetCustomersAsync())
            {
                customerList.Add(customer);
            }
        }

        private async void GetAdminList()
        {
            List<AdminEntity> adminList = new();
            foreach (var admin in await adminService.GetAdminsAsync())
            {
                adminList.Add(admin);
            }
        }

        private async Task FillCustomerOptionsAsync()
        {
            cbCustomersCase.Items.Clear();

            foreach (var customer in await customerService.GetCustomersAsync())
            {
                cbCustomersCase.Items.Add(new KeyValuePair<int, string>(customer.Id, customer.FirstName));
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

        private async Task FillStausOptionsAsync()
        {
            cbState.Items.Clear();

            foreach (var _state in await Task.FromResult(Enum.GetValues(typeof(Status))))
            {
                cbState.Items.Add(new KeyValuePair<int, string>((int)_state, _state.ToString()));
            }
        }

        private void ArrengeComboBox()
        {
            cbCustomersCase.SelectedValuePath = "Key";
            cbCustomersCase.DisplayMemberPath = "Value";

            cbAdmins.SelectedValuePath = "Key";
            cbAdmins.DisplayMemberPath = "Value";

            cbState.SelectedValuePath = "Key";
            cbState.DisplayMemberPath = "Value";
        }

        private void btnSaveCase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetCase();
        }
    }
}
