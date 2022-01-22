using CaseManagementApp.Helpers;
using CaseManagementApp.Models;
using CaseManagementApp.Models.Entity;
using CaseManagementApp.Models.ViewModels;
using CaseManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace CaseManagementApp.Views
{
    enum SortState
    {
        Default,
        NewestCreatedDate,
        NewestUpdatedDate,
        StatusWaiting,
        StatusOpened,
        StatusClosed,
        OldestCreatedDate,
        OldestUpdatedDate
    }

    /// <summary>
    /// Interaction logic for CasesView.xaml
    /// </summary>
    public partial class CasesView : UserControl
    {
        SqlService sqlService = new();
        private readonly List<CaseEntity> cases = new();
       
        public CasesView()
        {           
            InitializeComponent();
            Task.FromResult(GetCases());                              
        }

        /// <summary>
        /// <para>
        /// Hämtar ut en lista av Case från Databasen.
        /// </para>
        /// Se <see cref="SqlService.GetCasesAsync()"/>
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<CaseEntity>> GetCases()
        {
            var _case = await sqlService.GetCasesAsync();
            foreach (var item in _case)
            {
                cases.Add(item);              
            }
            
            lvCases.ItemsSource = cases;

            return _case;
        }       
    }
}
