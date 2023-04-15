using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ProjectTourism.Utilities;

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
        public void SetGuest2(Guest2VM guest2)
        {
            Guest2 = guest2;
        }

        public ICommand HomeCommand { get; set; }
        public ICommand TicketsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand SuggestTourCommand { get; set; }
        public ICommand ComplexToursCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Tickets(object obj) => CurrentView = new TicketsVM();
        private void Vouchers(object obj) => CurrentView = new VouchersVM();
        //private void Profile(object obj) => CurrentView = new ProfileVM();
        //private void SuggestTour(object obj) => CurrentView = new SuggestTourVM();
        //private void ComplexTour(object obj) => CurrentView = new ComplexTourVM();


        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            TicketsCommand = new RelayCommand(Tickets);
            VouchersCommand = new RelayCommand(Vouchers);
            //ProfileCommand = new RelayCommand(Profile);
            //SuggestTourCommand = new RelayCommand(SuggestTour);
            //ComplexToursCommand = new RelayCommand(ComplexTour);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}
