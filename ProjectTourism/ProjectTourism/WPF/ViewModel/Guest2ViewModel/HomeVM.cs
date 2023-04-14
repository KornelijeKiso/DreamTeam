using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Observer;
using ProjectTourism.Utilities;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    class HomeVM : Utilities.ViewModelBase
    {
        public Guest2VM Guest2 { get; set; }
        public Guest2Service GuestService { get; set; }
        public TourService TourService { get; set; }
        public Tour? SelectedTour { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        //public TourAppointmentService TourAppointmentService { get; set; }

        public string searchLocation { get; set; }
        public string searchLanguage { get; set; }
        public string searchDuration { get; set; }
        public string searchMaxNumberOfGuests { get; set; }

        public HomeVM()
        {
            GuestService = new Guest2Service(new Guest2Repository());
            //Guest2 = GuestService.GetOne(username);
            TourService = new TourService(new TourRepository());
            Tours = new ObservableCollection<Tour>(TourService.GetAll());

            searchLocation = "";
            searchLanguage = "";
            searchDuration = "";
            searchMaxNumberOfGuests = "";

            //TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
        }

        public void UpdateToursList(List<Tour> tours)
        {
            Tours.Clear();
            foreach (Tour tour in tours)
            {
                Tours.Add(tour);
            }
        }

        private void SearchLocation(ObservableCollection<Tour> tours)
        {
            List<Tour> toursList = new List<Tour>();
            if (searchLocation != "")
            {
                string[] searchQuery = searchLocation.ToLower().Split(',');
                if (searchQuery.Length == 2)
                {
                    foreach (Tour tour in tours)
                    {
                        if ((tour.Location.City.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && tour.Location.Country.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase))
                            || (tour.Location.Country.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && tour.Location.City.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase)))
                            toursList.Add(tour);
                    }
                }
                foreach (Tour tour in tours)
                {
                    if ((tour.Location.City.Contains(searchLocation, StringComparison.OrdinalIgnoreCase)
                        || (tour.Location.Country.Contains(searchLocation, StringComparison.OrdinalIgnoreCase))))
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }
        private void SearchLanguage(ObservableCollection<Tour> tours)
        {
            List<Tour> toursList = new List<Tour>();
            if (searchLanguage != "")
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Language.Contains(searchLanguage, StringComparison.OrdinalIgnoreCase))
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }

        private void SearchDuration(ObservableCollection<Tour> tours)
        {
            List<Tour> toursList = new List<Tour>();
            if (searchDuration != "")
            {
                double WantedDuration = double.Parse(searchDuration);
                foreach (Tour tour in tours)
                {
                    if (tour.Duration <= WantedDuration)
                        toursList.Add(tour);
                }
                UpdateToursList(toursList);
            }
        }

        private void SearchMaxNumberOfGuests(ObservableCollection<Tour> tours)
        {
            List<Tour> toursList = new List<Tour>();
            if (searchMaxNumberOfGuests != "")
            {
                int WantedMaxNumberOfGuests = int.Parse(searchMaxNumberOfGuests);
                foreach (Tour tour in tours)
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            UpdateToursList(TourService.GetAll());
            SearchOne();
        }

        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                CreateTicketWindow createTicketWidnow = new CreateTicketWindow(Guest2.Username, SelectedTour.Id);
                createTicketWidnow.ShowDialog();
            }
        }
    }
}
