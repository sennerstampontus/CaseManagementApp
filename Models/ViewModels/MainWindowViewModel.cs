using CaseManagementApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Models.ViewModels
{
    internal class MainWindowViewModel : ObservableObject
    {

        #region Commands And Models
        public RelayCommand StartScreenCommand { get; set; }

        public RelayCommand CreateUserCommand { get; set; }


        public RelayCommand CustomersViewCommand { get; set; }

       
        public RelayCommand AdminsViewCommand { get; set; }

        public RelayCommand CreateCaseCommand { get; set; }
        public RelayCommand CasesViewCommand { get; set; }

        public RelayCommand CaseDetailsCommand { get; set; }


        public StartScreenViewModel StartScreenViewModel { get; set; }
        public CreateNewUserViewModel CreateNewUserViewModel { get; set; }

        public CustomersViewModel CustomersViewModel { get; set; }

       
        public AdminsViewModel AdminsViewModel { get; set; }

        public CreateNewCaseViewModel CreateNewCaseViewModel { get; set; }
        public CasesViewModel CasesViewModel { get; set; }
        
        public CaseDetailsViewModel CaseDetailsViewModel { get; set; }
        #endregion


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {

            StartScreenViewModel = new StartScreenViewModel();

            CreateNewUserViewModel = new CreateNewUserViewModel();
            
            CustomersViewModel = new CustomersViewModel();

            
          
            AdminsViewModel = new AdminsViewModel();
            
            CreateNewCaseViewModel = new CreateNewCaseViewModel();
            CasesViewModel = new CasesViewModel();
           
            CaseDetailsViewModel = new CaseDetailsViewModel();

            CurrentView = StartScreenViewModel;


            StartScreenCommand = new RelayCommand(x => CurrentView = StartScreenViewModel);

            CustomersViewCommand = new RelayCommand(x => CurrentView = CustomersViewModel);                

            AdminsViewCommand = new RelayCommand(x => CurrentView = AdminsViewModel);

            CreateUserCommand = new RelayCommand(x => CurrentView = CreateNewUserViewModel);
            CreateCaseCommand = new RelayCommand(x => CurrentView = CreateNewCaseViewModel);
            CasesViewCommand = new RelayCommand(x => CurrentView = CasesViewModel);

            CaseDetailsCommand = new RelayCommand(x => CurrentView = CaseDetailsViewModel);
        }

    }
}
