using CaseManagementApp.Helpers;
using CaseManagementApp.Models;
using CaseManagementApp.Models.Entity;
using CaseManagementApp.Models.ViewModels;
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
    /// Interaction logic for CasesView.xaml
    /// </summary>
    public partial class CasesView : UserControl
    {
        SqlService sqlService = new();

        public CasesView()
        {
            
            InitializeComponent();
            GetCases();
        }

        private async void GetCases()
        {
            var _case = await sqlService.GetCasesAsync();
            foreach (var item in _case)
            {
                lvCases.Items.Add(item);
                
            }

            
        }

        private void ShowCaseDetails_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
