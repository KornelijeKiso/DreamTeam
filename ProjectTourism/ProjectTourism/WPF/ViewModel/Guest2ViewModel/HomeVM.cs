using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;
using System.Windows;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class HomeVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public TourService TourService { get; set; }
        public TourDTO? SelectedTour { get; set; }
        public ObservableCollection<TourDTO> AvailableTours { get; set; }
        //public ObservableCollection<TourDTO> ReservedTours { get; set; }
        public ObservableCollection<TourDTO> UnavailableTours { get; set; }
        public ObservableCollection<TourDTO> OldTours { get; set; }
        public string searchLocation { get; set; }
        public string searchLanguage { get; set; }
        public string searchDuration { get; set; }
        public string searchMaxNumberOfGuests { get; set; }
        
        public HomeVM() { }
        public HomeVM(Guest2DTO guest2)
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
        
        public void UpdateToursList(List<TourDTO> tours)
        {
            AvailableTours.Clear();
            foreach (TourDTO tour in tours)
            {
                AvailableTours.Add(tour);
            }
        }
        
        private bool IsOldTour(TourDTO tour)
        {
            bool result = false;
            foreach (TourAppointmentDTO tourApp in tour.TourAppointments)
            {
                if (tourApp.TourDateTime >= DateTime.Now)
                    return false;
                else
                    result = true;
            }
            return result;
        }
        private ObservableCollection<TourDTO> SetOldTours(ObservableCollection<TourDTO> tours)
        {
            ObservableCollection<TourDTO> oldTours = new ObservableCollection<TourDTO>();
            foreach (TourDTO tour in tours)
            {
                if (IsOldTour(tour))
                    oldTours.Add(tour);
            }
            return oldTours;
        }
        private ObservableCollection<TourDTO> RemoveOldTours(ObservableCollection<TourDTO> tours)
        {
            ObservableCollection<TourDTO> onlyNewTours = new ObservableCollection<TourDTO>();
            foreach (TourDTO tour in tours)
            {
                if (!IsOldTour(tour))
                    onlyNewTours.Add(tour);
            }
            return onlyNewTours;
        }

        //private bool IsReserved(TourDTO tour)
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
        //private ObservableCollection<TourDTO> SetAlreadyReservedTours(ObservableCollection<TourDTO> tours)
        //{
        //    ObservableCollection<TourDTO> reservedTours = new ObservableCollection<TourDTO>();
        //    foreach(TourVM tour in tours)
        //    {
        //        if (IsReserved(tour))
        //            reservedTours.Add(tour);
        //    }
        //    return reservedTours;
        //}

        private bool IsAvailable(TourDTO tour)
        {
            foreach (TourAppointmentDTO tourApp in tour.TourAppointments)
            {
                if (tourApp.IsAvailable)
                    return true;
            }
            return false;
        }
        private ObservableCollection<TourDTO> SetUnavailableTours(ObservableCollection<TourDTO> tours)
        {
            ObservableCollection<TourDTO> unavailableTours = new ObservableCollection<TourDTO>();
            foreach (TourDTO tour in tours)
            {
                if (!IsAvailable(tour))
                    unavailableTours.Add(tour);
            }
            return unavailableTours;
        }
        private ObservableCollection<TourDTO> SetAvailableTours(ObservableCollection<TourDTO> tours)
        {
            ObservableCollection<TourDTO> availableTours = new ObservableCollection<TourDTO>();
            foreach (TourDTO tour in tours)
            {
                if (IsAvailable(tour))
                    availableTours.Add(tour);
            }
            return availableTours;
        }


        private void SearchLocation(ObservableCollection<TourDTO> tours)
        {
            List<TourDTO> toursList = new List<TourDTO>();
            if (searchLocation != "")
            {
                string[] searchQuery = searchLocation.ToLower().Split(',');
                if (searchQuery.Length == 2)
                {
                    foreach (TourDTO tour in tours)
                    {
                        if ((tour.Location.City.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && tour.Location.Country.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase))
                            || (tour.Location.Country.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && tour.Location.City.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase)))
                            toursList.Add(tour);
                    }
                }
                foreach (TourDTO tour in tours)
                {
                    if ((tour.Location.City.Contains(searchLocation, StringComparison.OrdinalIgnoreCase)
                        || (tour.Location.Country.Contains(searchLocation, StringComparison.OrdinalIgnoreCase))))
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }
        private void SearchLanguage(ObservableCollection<TourDTO> tours)
        {
            List<TourDTO> toursList = new List<TourDTO>();
            if (searchLanguage != "")
            {
                foreach (TourDTO tour in tours)
                {
                    if (tour.Language.Contains(searchLanguage, StringComparison.OrdinalIgnoreCase))
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }

        private void SearchDuration(ObservableCollection<TourDTO> tours)
        {
            List<TourDTO> toursList = new List<TourDTO>();
            if (searchDuration != "")
            {
                double WantedDuration = double.Parse(searchDuration);
                foreach (TourDTO tour in tours)
                {
                    if (tour.Duration <= WantedDuration)
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }

        private void SearchMaxNumberOfGuests(ObservableCollection<TourDTO> tours)
        {
            List<TourDTO> toursList = new List<TourDTO>();
            if (searchMaxNumberOfGuests != "")
            {
                int WantedMaxNumberOfGuests = int.Parse(searchMaxNumberOfGuests);
                foreach (TourDTO tour in tours)
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

        public ICommand BuyTicketCommand
        {
            get
            {
                //NavigationVM navigationVM = new NavigationVM(Guest2.Username);
                //navigationVM.Guest2.SelectedTour = SelectedTour;
                //navigationVM.CreateTicketCommand = new RelayCommand(CreateTicket);
                //return navigationVM.CreateTicketCommand;
                //return new RelayCommand(CreateTicket);
            }
            //get => new RelayCommand(CreateTicket);
        }
        private void CreateTicket(object obj)
        {
            if (SelectedTour != null)
            {
                //Guest2.SelectedTour = SelectedTour;
                //NavigationVM navigationVM = new NavigationVM(Guest2.Username);
                //navigationVM.Guest2.SelectedTour = SelectedTour;
                //navigationVM.CurrentView = new CreateTicketVM(Guest2);
                ////navigationVM.CreateTicketCommand = new RelayCommand();
                //navigationVM.CreateTicketCommand.CanExecute(obj);
            }
            else
            {
                MessageBox.Show("Please select the tour you would like to make a reservationa ! ");
            }
        }

        //private ICommand _PictureForwardCommand;
        //public ICommand PictureForwardCommand
        //{
        //    get
        //    {
        //        return _PictureForwardCommand ?? (_PictureForwardCommand = new CommandHandler(() => PictureForwardClick(), () => true));
        //    }
        //}
        //public void PictureForwardClick()
        //{

        //}

        //private ICommand _PictureBackwardCommand;
        //public ICommand PictureBackwardCommand
        //{
        //    get
        //    {
        //        return _PictureBackwardCommand ?? (_PictureBackwardCommand = new CommandHandler(() => PictureBackwardClick(), () => true));
        //    }
        //}
        //public void PictureBackwardClick()
        //{
         
        //}

        private ICommand _PictureCommand;
        public ICommand PictureCommand
        {
            get
            {
                return _PictureCommand ?? (_PictureCommand = new CommandHandler(() => PictureClick(), () => CanPictureClick()));
            }
        }

        private bool CanPictureClick()
        {
            return (/*SelectedTour != null &&*/ !SelectedTour.ArePicturesEmpty);
        }
        public void PictureClick()
        {
            // TO DO -> Create PictureDisplayUserControl
            PictureDisplayWindow pictureDisplayWindow = new PictureDisplayWindow(SelectedTour);
            pictureDisplayWindow.ShowDialog();
        }
    }
}
