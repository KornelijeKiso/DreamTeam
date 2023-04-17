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
            homeVM.SetGuest2(guest2);
            ticketsVM.SetGuest2(guest2);
            vouchersVM.SetGuest2(guest2);
            //profileVM.SetGuest2(guest2);
            //suggestTourVM.SetGuest2(guest2);
            //complexTourVM.SetGuest2(guest2);
        }

        public ICommand HomeCommand { get; set; }
        public ICommand TicketsCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand SuggestTourCommand { get; set; }
        public ICommand ComplexToursCommand { get; set; }

        private HomeVM homeVM = new HomeVM();
        private TicketsVM ticketsVM = new TicketsVM();
        private VouchersVM vouchersVM = new VouchersVM();
        //private ProfileVM profileVM = new ProfileVM();
        //private SuggestTourVM suggestTourVM = new SuggestTourVM();
        //private ComplexTourVM complexTourVM = new ComplexTourVM();

        private void Home(object obj) => CurrentView = homeVM;
        private void Tickets(object obj) => CurrentView = ticketsVM;
        private void Vouchers(object obj) => CurrentView = vouchersVM;
        //private void Profile(object obj) => CurrentView = profileVM;
        //private void SuggestTour(object obj) => CurrentView = suggestTourVM;
        //private void ComplexTour(object obj) => CurrentView = complexTourVM;
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            TicketsCommand = new RelayCommand(Tickets);
            VouchersCommand = new RelayCommand(Vouchers);
            //ProfileCommand = new RelayCommand(Profile);
            //SuggestTourCommand = new RelayCommand(SuggestTour);
            //ComplexToursCommand = new RelayCommand(ComplexTour);

            // Startup Page
            //CurrentView = new HomeVM();
            CurrentView = new TicketsVM();
        }
    }
}
