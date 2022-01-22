using CaseManagementApp.Models;
using CaseManagementApp.Models.Entity;
using CaseManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for CaseDetailsView.xaml
    /// </summary>
    public partial class CaseDetailsView : UserControl
    {
        private readonly SqlService caseService = new();
        private readonly List<AdminEntity> _admins = new();


        public CaseDetailsView()
        {
            InitializeComponent();
            GetAdminListAsync();
            PopulateComboBoxes();          
        }


        /// <summary>
        /// <para>
        /// Anropar enskilda metoder för att fylla ComboBoxar med sin information.<br></br>
        /// Samt anropa en metod för att ordna upp informationen.
        /// </para>
        /// </summary>
        private async void PopulateComboBoxes()
        {
            await FillCaseOptionsAsync();          
            await FillStausOptionsAsync();
            FillAdminOptionsAsync();
            ArrengeComboBox();
        }


        /// <summary>
        /// Fyller Case ComboBoxen med dess information.
        /// </summary>
        /// <returns></returns>
        private async Task FillCaseOptionsAsync()
        {
            cbCaseSelection.Items.Clear();

            foreach (var _case in await caseService.GetCasesAsync())
            {
                cbCaseSelection.Items.Add(new KeyValuePair<int, string>(_case.CaseId, $"Case Id: {_case.CaseId} {_case.Subject} | [{_case.State}]"));
            }
        }

        /// <summary>
        /// Fyller Admin ComboBoxen med dess information.
        /// </summary>
        private void FillAdminOptionsAsync()
        {
            cbAdmins.Items.Clear();

            foreach (var admin in _admins)
            {
                cbAdmins.Items.Add(new KeyValuePair<int, string>(admin.Id, $"{admin.Id} : {admin.FirstName} {admin.LastName}"));
            }
        }


        /// <summary>
        /// Fyller Status ComboBoxen med dess information.
        /// </summary>
        /// <returns></returns>
        private async Task FillStausOptionsAsync()
        {
            cbState.Items.Clear();

            foreach (var _state in await Task.FromResult(Enum.GetValues(typeof(Status))))
            {
                cbState.Items.Add(new KeyValuePair<int, string>((int)_state, _state.ToString()));
            }
        }


        /// <summary>
        /// Sätter vilken information som ska visas på ComboBoxarna (Key, Value) pair.
        /// </summary>
        private void ArrengeComboBox()
        {
            cbCaseSelection.SelectedValuePath = "Key";
            cbCaseSelection.DisplayMemberPath = "Value";

            cbAdmins.SelectedValuePath = "Key";
            cbAdmins.DisplayMemberPath = "Value";

            cbState.SelectedValuePath = "Key";
            cbState.DisplayMemberPath = "Value";
        }


        /// <summary>
        /// Sätter informationen i alla contents utifrån det valda "Case'et".
        /// </summary>
        /// <param name="caseResult"></param>
        private void SetValues(CaseEntity caseResult)
        {
            lblCustomerName.Content = caseResult.Customer.FullName;
            lblCustomerEmail.Content = caseResult.Customer.Email;
            lblCustomerPhone.Content = caseResult.Customer.PhoneNumber;
            lblSubject.Content = caseResult.Subject;
            tbDescription.Text = caseResult.Description;
            lblAdmins.Content = caseResult.Admin.FullName;
            lblState.Content = caseResult.State;

        }


        /// <summary>
        /// <para>
        /// Hämtar ut ett Case utifrån det valda värdet i ComboBoxen "CaseSelection".
        /// </para>
        /// <see cref="SqlService.GetCaseAsync(int)"/>
        /// </summary>
        /// <returns></returns>
        private async Task<CaseEntity> GetCaseAsync()
        {
            var caseId = (int)cbCaseSelection.SelectedValue;
            var _case = await caseService.GetCaseAsync(caseId);

            SetValues(_case);

            return _case;
        }


        /// <summary>
        /// <para>
        /// Hämtar admins från databasen och sparar in i _admins.
        /// </para>
        /// <see cref="SqlService.GetAdminsAsync()"/>
        /// </summary>
        private async void GetAdminListAsync()
        {
            foreach (var admin in await caseService.GetAdminsAsync())
            {
                _admins.Add(admin);
            }
        }


        /// <summary>
        /// Skickar ut vilken admin det valda caset ska uppdateras till.
        /// </summary>
        /// <returns>int värdet -1 om ingen admin är vald.</returns>
        private int UpdateCaseAdmin()
        {          
            if (cbAdmins.SelectedValue != null)
            {
                var newAdminId = (int)cbAdmins.SelectedValue;
                return newAdminId;
            }

            else
                return -1;
        }


        /// <summary>
        /// <para>
        /// Kontrollerar valet för den nya statusen.
        /// </para>
        /// </summary>
        /// <returns>Samma status om inget är valt<br></br>annars returnerar det nya valet av status</returns>
        private async Task<string> UpdateCaseStateAsync()
        {
            if(cbState.SelectedValue == null)
            {
                var currentCase = await GetCaseAsync();
                string currentState = currentCase.State;
                cbState.SelectedValue = (Status)Enum.Parse(typeof(Status), currentState);
                return currentState;
            }

            else
            {
                var newCaseState = (Status)Enum.Parse(typeof(Status), cbState.SelectedValue.ToString());
                return newCaseState.ToString();
            }


        }

        private async Task UpdateCaseAsync(CaseEntity patchCase)
        {
            var _case = patchCase;
            int caseId = patchCase.CaseId;

            var newAdminId = UpdateCaseAdmin();
            string newState = await UpdateCaseStateAsync();

            if(newAdminId > 0)
            {
                var newAdmin = _admins.Where(x => x.Id == newAdminId).FirstOrDefault();

                CaseEntity updatedCase = new()
                {
                    Admin = newAdmin,
                    State = newState,
                    UpdatedDate = DateTime.Now
                };

                await caseService.UpdateCaseDbAsync(caseId, updatedCase);
                SetUpdatedValues(updatedCase);
            }

            else if(newAdminId == -1)
            {
                lblErrorAdmin.Content = "Please select an admin..";
            }

            else if (newState == null)
            {
                cbState.SelectedValue = patchCase.State;
            }

            else
            {
                CaseEntity updatedCase = new()
                {
                    Admin = patchCase.Admin,
                    State = newState
                };

               await caseService.UpdateCaseDbAsync(caseId, updatedCase);
               SetUpdatedValues(updatedCase);
            }

           

        }


        /// <summary>
        /// <para>
        /// Tar in information från <br></br>
        /// GetCase, UpdateCaseAdmin, UpdateCaseState och FillAdminOptionsAsync metoderna.<br></br>
        /// Sätter därefter nya värden till det hämtade "Case'et" och updaterar databasen.
        /// </para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSaveCase_Click(object sender, RoutedEventArgs e)
        {

            var assignCase = await GetCaseAsync();
            await UpdateCaseAsync(assignCase);
     
        }


        /// <summary>
        /// Sätter värden i Labels som indikerar vad som uppdaterats i caset.
        /// </summary>
        /// <param name="_case"></param>
        private void SetUpdatedValues(CaseEntity _case)
        {
            lblErrorAdmin.Content = "";

            lblUpdatedValuesLabel.Visibility = Visibility.Visible;
            lblNewAssignedAdminLabel.Visibility = Visibility.Visible;
            lblNewAssignedStatusLabel.Visibility = Visibility.Visible;

            lblNewAssignedAdmin.Content = _case.Admin.FullName;
            lblNewAssignedStatus.Content = _case.State.ToString();

        }

  
        /// <summary>
        /// Hämtar information från det valda "Case'et".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cbCaseSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _case = await GetCaseAsync();
            
            cbAdmins.SelectedValue = _case.Admin.Id;

        }
    }
}
