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
    /// Interaction logic for StartUpScreen.xaml
    /// </summary>
    public partial class StartUpScreen : UserControl
    {

        private readonly SqlService recentCases = new();
        private readonly List<CaseEntity> caseList = new();


        public StartUpScreen()
        {
            InitializeComponent();
            GetCases();
            SetCounterToLabels();
        }

        private void SetCounterToLabels()
        {
            lblTotalCases.Content = caseList.Count.ToString();
            lblCasesWaiting.Content = caseList.Where(x => x.State.Contains("Waiting")).Count().ToString();
            lblCasesOpened.Content = caseList.Where(x => x.State.Contains("Opened")).Count().ToString();
            lblCasesClosed.Content = caseList.Where(x => x.State.Contains("Closed")).Count().ToString();
        }

        private async void GetCases()
        {
            
            foreach(var _case in await recentCases.GetCasesAsync())
            {
                caseList.Add(_case);
            }

           

            List<CaseEntity> sortedList = caseList.OrderBy(x => x.CreatedDate).Reverse().Take(10).ToList();

            lvRecentCases.ItemsSource = sortedList;

        }
    }
}
