using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class HomeVM : ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public TourService TourService { get; set; }
        public TourVM? SelectedTour { get; set; }
        public ObservableCollection<TourVM> AvailableTours { get; set; }
        //public ObservableCollection<TourVM> ReservedTours { get; set; }
        public ObservableCollection<TourVM> UnavailableTours { get; set; }
        public ObservableCollection<TourVM> OldTours { get; set; }
        public string searchLocation { get; set; }
        public string searchLanguage { get; set; }
        public string searchDuration { get; set; }
        public string searchMaxNumberOfGuests { get; set; }
        
        public HomeVM() { }
        public HomeVM(Guest2VM guest2)
        {
            Guest2 = guest2;

            TourService = new TourService();
            //ReservedTours = SetAlreadyReservedTours(RemoveOldTours(Guest2.Tours));
            OldTours = SetOldTours(Guest2.Tours);
            UnavailableTours = SetUnavailableTours(RemoveOldTours(Guest2.Tours));
            AvailableTours = SetAvailableTours(RemoveOldTours(Guest2.Tours));

            searchLocation = "";
            searchLanguage = "";
            searchDuration = "";
            searchMaxNumberOfGuests = "";
        }
        
        public void UpdateToursList(List<TourVM> tours)
        {
            AvailableTours.Clear();
            foreach (TourVM tour in tours)
            {
                AvailableTours.Add(tour);
            }
        }
        
        private bool IsOldTour(TourVM tour)
        {
            bool result = false;
            foreach (TourAppointmentVM tourApp in tour.TourAppointments)
            {
                if (tourApp.TourDateTime >= DateTime.Now)
                    return false;
                else
                    result = true;
            }
            return result;
        }
        private ObservableCollection<TourVM> SetOldTours(ObservableCollection<TourVM> tours)
        {
            ObservableCollection<TourVM> oldTours = new ObservableCollection<TourVM>();
            foreach (TourVM tour in tours)
            {
                if (IsOldTour(tour))
                    oldTours.Add(tour);
            }
            return oldTours;
        }
        private ObservableCollection<TourVM> RemoveOldTours(ObservableCollection<TourVM> tours)
        {
            ObservableCollection<TourVM> onlyNewTours = new ObservableCollection<TourVM>();
            foreach (TourVM tour in tours)
            {
                if (!IsOldTour(tour))
                    onlyNewTours.Add(tour);
            }
            return onlyNewTours;
        }

        //private bool IsReserved(TourVM tour)
        //{
        //    foreach (var ticket in Guest2.Tickets)
        //    {   
        //        foreach (var tourApp in tour.TourAppointments)
        //        {
        //            if (!IsOldTour(tour) 
        //                && ticket.TourAppointment.Id == tourApp.Id 
        //                && ticket.TourAppointment.State == TOURSTATE.READY)
        //                return true;
        //        }
        //    }    
        //    return false;
        //}
        //private ObservableCollection<TourVM> SetAlreadyReservedTours(ObservableCollection<TourVM> tours)
        //{
        //    ObservableCollection<TourVM> reservedTours = new ObservableCollection<TourVM>();
        //    foreach(TourVM tour in tours)
        //    {
        //        if (IsReserved(tour))
        //            reservedTours.Add(tour);
        //    }
        //    return reservedTours;
        //}

        private bool IsAvailable(TourVM tour)
        {
            foreach (TourAppointmentVM tourApp in tour.TourAppointments)
            {
                if (tourApp.State == TOURSTATE.READY && tourApp.AvailableSeats != 0 && tourApp.TourDateTime >= DateTime.Now)
                    return true;
            }
            return false;
        }
        private ObservableCollection<TourVM> SetUnavailableTours(ObservableCollection<TourVM> tours)
        {
            ObservableCollection<TourVM> unavailableTours = new ObservableCollection<TourVM>();
            foreach (TourVM tour in tours)
            {
                if (!IsAvailable(tour))
                    unavailableTours.Add(tour);
            }
            return unavailableTours;
        }
        private ObservableCollection<TourVM> SetAvailableTours(ObservableCollection<TourVM> tours)
        {
            ObservableCollection<TourVM> availableTours = new ObservableCollection<TourVM>();
            foreach (TourVM tour in tours)
            {
                if (IsAvailable(tour))
                    availableTours.Add(tour);
            }
            return availableTours;
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
            SearchLocation(AvailableTours);
            SearchDuration(AvailableTours);
            SearchLanguage(AvailableTours);
            SearchMaxNumberOfGuests(AvailableTours);
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
            UpdateToursList(Guest2.Tours.ToList());
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

        private ICommand _PictureCommand;
        public ICommand PictureCommand
        {
            get
            {
                return _PictureCommand ?? (_PictureCommand = new CommandHandler(() => Picture_Click(), () => true));
            }
        }

        public void Picture_Click()
        {

        }
    }
}
