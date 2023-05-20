using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ProjectTourism.Utilities;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        
        public Guest2VM Guest2 { get; set; }
        public string Username { get; set; }

        //public RelayCommand NavigateHomeCommand { get; set; }
        //public RelayCommand NavigateTicketsCommand { get; set; }
        //public RelayCommand NavigateVouchersCommand { get; set; }
        //public RelayCommand NavigateProfileCommand { get; set; }
        //public RelayCommand NavigateTourRequestsCommand { get; set; }
        //public RelayCommand NavigateComplexToursCommand { get; set; }
       
        public ICommand HomeCommand { get; set; }
        public ICommand TicketsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand TourRequestsCommand { get; set; }
        public ICommand ComplexToursCommand { get; set; }
        public ICommand NotificationsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM(Guest2);
        private void Tickets(object obj) => CurrentView = new TicketsVM(Guest2);
        private void Vouchers(object obj) => CurrentView = new VouchersVM(Guest2);
        private void Profile(object obj) => CurrentView = new ProfileVM(Guest2);
        private void TourRequests(object obj) => CurrentView = new TourRequestsVM(Guest2);
        //private void ComplexTour(object obj) => CurrentView = ComplexTourVM(Guest2);
        private void Notifications(object obj)
        {
            NotificationsWindow notificationWindow = new NotificationsWindow(Guest2);
            notificationWindow.ShowDialog();
            Guest2.HasNewNotifications = false;
            Guest2.NumberOfNotifications = 0;
        }

        public NavigationVM(string username)
        {
            Username = username;
            Guest2 = new Guest2VM(Username);

            HomeCommand = new RelayCommand(Home);
            TicketsCommand = new RelayCommand(Tickets);
            VouchersCommand = new RelayCommand(Vouchers);
            ProfileCommand = new RelayCommand(Profile);
            TourRequestsCommand = new RelayCommand(TourRequests);
            //ComplexToursCommand = new RelayCommand(ComplexTour);
            NotificationsCommand = new RelayCommand(Notifications);

            // Startup Page
            CurrentView = new HomeVM(Guest2);
        }

        public void DisplayNotifications()
        {
            NotificationsWindow notificationWindow = new NotificationsWindow(Guest2);
            notificationWindow.ShowDialog();
            Guest2.HasNewNotifications = false;
            Guest2.NumberOfNotifications = 0;
        }
    }
}
