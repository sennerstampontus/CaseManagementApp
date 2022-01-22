﻿using CaseManagementApp.Models.Entity;
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
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : UserControl
    {
        SqlService sqlService = new();
        public CustomersView()
        {
            InitializeComponent();
            GetCustomerList();
        }

        /// <summary>
        /// GetCustomerList hämtar kunder från databasen och lägger till i ListView.
        /// </summary>
        private async void GetCustomerList()
        {


            var customerList = await sqlService.GetCustomersAsync();
            foreach(var item in customerList)
            {
               
                lvCustomers.Items.Add(item);
            }
            
           
        }
    }
}
