using CaseManagementApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Models.ViewModels
{
    internal class CreateNewUserViewModel : ObservableObject
    {
        public RelayCommand CreateCustomerCommand { get; set; }
        public CreateNewCustomerViewModel CreateNewCustomerViewModel { get; set; }
        public RelayCommand CreateAdminCommand { get; set; }
        public CreateNewAdminViewModel CreateNewAdminViewModel { get; set; }




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

        public CreateNewUserViewModel()
        {
            CreateNewCustomerViewModel = new CreateNewCustomerViewModel();
            CreateNewAdminViewModel = new CreateNewAdminViewModel();

            CurrentView = CreateNewCustomerViewModel;

            CreateCustomerCommand = new RelayCommand(x => CurrentView = CreateNewCustomerViewModel);
            CreateAdminCommand = new RelayCommand(x => CurrentView = CreateNewAdminViewModel);


        }

    }




}
