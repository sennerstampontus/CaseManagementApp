using CaseManagementApp.Models;
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
    /// Interaction logic for CreateNewAdminView.xaml
    /// </summary>
    public partial class CreateNewAdminView : UserControl
    {
        public CreateNewAdminView()
        {
            InitializeComponent();
        }

        private void CreateAdmin_btn_Click(object sender, RoutedEventArgs e)
        {
            CreateAdmin();
            
        }

        /// <summary>
        /// Skapar en ny Amdin med de inmatade värdena.
        /// </summary>
        public async void CreateAdmin()
        {
            Admin admin = new()
            {
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Email = tbEmail.Text,
                Address = new Address()
                {
                    StreetName = tbStreetName.Text,
                    ZipCode = tbZipCode.Text,
                    City = tbCity.Text,
                    Country = tbCountry.Text
                }
            };

            SqlService sqlService = new();
            int Exist = await sqlService.SaveDbAdminAsync(admin);
            if(Exist == -1)
                tbError.Content = "An admin with the same email aldready exists";
            
            else
                ClearFields();
        }

        private void ClearFields()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbEmail.Text = "";
            tbStreetName.Text = "";
            tbZipCode.Text = "";
            tbCity.Text = "";
            tbCountry.Text ="";
            tbError.Content = "";
        }

    }
}
