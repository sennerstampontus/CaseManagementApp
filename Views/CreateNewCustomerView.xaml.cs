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
    /// Interaction logic for CreateNewCustomerView.xaml
    /// </summary>
    public partial class CreateNewCustomerView : UserControl
    {
       
        public CreateNewCustomerView()
        {
            InitializeComponent();
        }

        private async void CreateCustomer_btn_Click(object sender, RoutedEventArgs e)
        {

            await CreateCustomerAsync();
            
        }

        public async Task CreateCustomerAsync()
        {
            Customer customer = new Customer()
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
                }
            };

            if (tbFirstName.Text != "" && tbLastName.Text != "" && tbEmail.Text != "")
            {
                SqlService sqlService = new SqlService();
                int exist = await Task.FromResult(sqlService.CreateCustomer(customer));
                if (exist == -1)
                {
                    tbErrorTxt.Content = "Customer with email already exists.";
                }
            }

            else
                tbErrorTxt.Content = "";


        }
    }
}
