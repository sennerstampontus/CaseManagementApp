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
    /// Interaction logic for AdminsView.xaml
    /// </summary>
    public partial class AdminsView : UserControl
    {
        public AdminsView()
        {
            InitializeComponent();
            GetAdminList();
        }

        /// <summary>
        /// Hämtar alla admins från Databasen och sätter ListView.
        /// </summary>
        private async void GetAdminList()
        {
            SqlService adminService = new();

            var adminList = await adminService.GetAdminsAsync();
            foreach (var item in adminList)
            {
                lvAdmins.Items.Add(item);
            }          

        }
    }
}
