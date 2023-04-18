using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Observer;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class HomeVM : ViewModelBase
    {
        public CurrentUserService CurrentUserService { get; set; }
        public UserVM CurrentUser { get; set; }
        public Guest2VM Guest2 { get; set; }
        public TourService TourService { get; set; }
        public TourVM? SelectedTour { get; set; }
        public ObservableCollection<TourVM> Tours { get; set; }
        //public TourAppointmentService TourAppointmentService { get; set; }

        public string searchLocation { get; set; }
        public string searchLanguage { get; set; }
        public string searchDuration { get; set; }
        public string searchMaxNumberOfGuests { get; set; }

        public HomeVM()
        {
            SetGuest2();

            TourService = new TourService(new TourRepository());
            Tours = new ObservableCollection<TourVM>(TourService.GetAll().Select(x => new TourVM(x)).ToList());

            searchLocation = "";
            searchLanguage = "";
            searchDuration = "";
            searchMaxNumberOfGuests = "";

            //TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
        }
        public void SetGuest2()
        {
            CurrentUserService = new CurrentUserService(new CurrentUserRepository());
            CurrentUser = new UserVM(CurrentUserService.GetUser());
            Guest2 = new Guest2VM(CurrentUser.Username);
        }

        public void UpdateToursList(List<TourVM> tours)
        {
            Tours.Clear();
            foreach (TourVM tour in tours)
            {
                Tours.Add(tour);
            }
        }

        private void SearchLocation(ObservableCollection<TourVM> tours)
        {
            List<TourVM> toursList = new List<TourVM>();
            if (searchLocation != "")
            {
                string[] searchQuery = searchLocation.ToLower().Split(',');
                if (searchQuery.Length == 2)
                {
                    foreach (TourVM tour in tours)
                    {
                        if ((tour.Location.City.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && tour.Location.Country.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase))
                            || (tour.Location.Country.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && tour.Location.City.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase)))
                            toursList.Add(tour);
                    }
                }
                foreach (TourVM tour in tours)
                {
                    if ((tour.Location.City.Contains(searchLocation, StringComparison.OrdinalIgnoreCase)
                        || (tour.Location.Country.Contains(searchLocation, StringComparison.OrdinalIgnoreCase))))
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }
        private void SearchLanguage(ObservableCollection<TourVM> tours)
        {
            List<TourVM> toursList = new List<TourVM>();
            if (searchLanguage != "")
            {
                foreach (TourVM tour in tours)
                {
                    if (tour.Language.Contains(searchLanguage, StringComparison.OrdinalIgnoreCase))
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }

        private void SearchDuration(ObservableCollection<TourVM> tours)
        {
            List<TourVM> toursList = new List<TourVM>();
            if (searchDuration != "")
            {
                double WantedDuration = double.Parse(searchDuration);
                foreach (TourVM tour in tours)
                {
                    if (tour.Duration <= WantedDuration)
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }

        private void SearchMaxNumberOfGuests(ObservableCollection<TourVM> tours)
        {
            List<TourVM> toursList = new List<TourVM>();
            if (searchMaxNumberOfGuests != "")
            {
                int WantedMaxNumberOfGuests = int.Parse(searchMaxNumberOfGuests);
                foreach (TourVM tour in tours)
                {
                    if (tour.MaxNumberOfGuests <= WantedMaxNumberOfGuests)
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }

        private void SearchOne()
        {
            SearchLocation(Tours);
            SearchDuration(Tours);
            SearchLanguage(Tours);
            SearchMaxNumberOfGuests(Tours);
        }

        private ICommand _SearchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _SearchCommand ?? (_SearchCommand = new CommandHandler(() => Search_Click(), () => true));
            }
        }
        
        public void Search_Click()
        {
            UpdateToursList(TourService.GetAll().Select(x => new TourVM(x)).ToList());
            SearchOne();
        }

        private ICommand _BuyTicketCommand;
        public ICommand BuyTicketCommand
        {
            get
            {
                return _BuyTicketCommand ?? (_BuyTicketCommand = new CommandHandler(() => BuyTicket_Click(), () => CanBuy));
            }
        }
        public bool CanBuy
        {
            get
            {
                return (SelectedTour != null && Guest2 != null); 
            }
        }

        public void BuyTicket_Click()
        {
                CreateTicketWindow createTicketWidnow = new CreateTicketWindow(Guest2, SelectedTour);
                createTicketWidnow.ShowDialog();
        }
    }
}
