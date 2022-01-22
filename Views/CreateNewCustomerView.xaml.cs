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
    /// Interaction logic for CreateNewCustomerView.xaml
    /// </summary>
    public partial class CreateNewCustomerView : UserControl
    {
        

       
        public CreateNewCustomerView()
        {
            InitializeComponent();          
        }

        private void CreateCustomer_btn_Click(object sender, RoutedEventArgs e)
        {

            CreateCustomer();
            
        }

        /// <summary>
        /// Skapar en ny Customer i databasen från de inmatade värdena.
        /// </summary>
        public void CreateCustomer()
        {

            Customer customer = new()
            {
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Email = tbEmail.Text,
                PhoneNumber = tbPhoneNumber.Text,
                Address = new Address()
                {
                    StreetName = tbStreetName.Text,
                    ZipCode = tbZipCode.Text,
                    City = tbCity.Text,
                    Country = tbCountry.Text
                },
        };

         

            if (tbFirstName.Text != "" && tbLastName.Text != "" && tbEmail.Text != "")
            {
                
                SqlService SqlService = new();
                
                int Exist = SqlService.CreateCustomer(customer);              
                if (Exist == -1)
                    tbErrorTxt.Content = "Customer with email already exists.";

                else
                    ClearFields();
            }

            else
                tbErrorTxt.Content = "Please check the fields.";
                


        }

        private void ClearFields()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbEmail.Text = "";
            tbPhoneNumber.Text = "";
            tbStreetName.Text = "";
            tbZipCode.Text = "";
            tbCity.Text = "";
            tbCountry.Text = "";
            tbErrorTxt.Content = "";
        }
    }
}
