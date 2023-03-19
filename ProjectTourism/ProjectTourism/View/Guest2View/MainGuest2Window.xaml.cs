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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.View.Guest2View.TicketView;

namespace ProjectTourism.View.Guest2View
{
    /// <summary>
    /// Interaction logic for MainGuest2Window.xaml
    /// </summary>
    public partial class MainGuest2Window : Window, INotifyPropertyChanged, IObserver
    {
        public User User { get; set; }
        public UserController UserController { get; set; }
        public Guest2 Guest { get; set; }
        public Guest2Controller GuestController { get; set; }
        public TourController TourController { get; set; }
        public Tour? SelectedTour { get; set; }
        public TicketController TicketController { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        //public GuideController GuideController { get; set; }

        public string searchLocation { get; set; }
        public string searchLanguage { get; set; }
        public string searchDuration { get; set; }
        public string searchMaxNumberOfGuests { get; set; }


        public MainGuest2Window(string username)
        {
            InitializeComponent();
            DataContext = this;
            UserController = new UserController();
            User = UserController.GetOne(username);
            GuestController = new Guest2Controller();
            Guest = GuestController.GetOne(username);
            //GuideController = new GuideController();
            TourController = new TourController();
            Tours = new ObservableCollection<Tour>(TourController.GetAll());

            searchLocation = "";
            searchLanguage = "";
            searchDuration = "";
            searchMaxNumberOfGuests = "";

            TicketController = new TicketController();

        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void UpdateToursList()
        {
            Tours.Clear();
            foreach (Tour tour in TourController.GetAll())
            {
                Tours.Add(tour);
            }
        }

        public void Update()
        {
            UpdateToursList();
            throw new NotImplementedException();
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
                        || (tour.Location.Country.Contains(searchLocation, StringComparison.OrdinalIgnoreCase)))   )
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
                foreach (Tour tour in tours)
                {
                    if (tour.Duration.ToString().Contains(searchDuration, StringComparison.OrdinalIgnoreCase))
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
                foreach (Tour tour in tours)
                {
                    if (tour.MaxNumberOfGuests.ToString().Contains(searchMaxNumberOfGuests, StringComparison.OrdinalIgnoreCase))
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

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            UpdateToursList(TourController.GetAll());
            SearchOne();
        }

        private void BuyTicket(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                {
                    CreateTicketWindow createTicketWindow = new CreateTicketWindow(Guest.Username, SelectedTour.Id);
                    if (createTicketWindow.dates.Count == 0)
                    {
                        MessageBox.Show("No available seats for this Tour.");
                    } else
                        createTicketWindow.ShowDialog();
                }
                
                
            }
            else
                MessageBox.Show("Please select the tour.");
        }

        private void ShowTickets(object sender, RoutedEventArgs e)
        {
            TicketOverviewWindow ticketOverviewWindow = new TicketOverviewWindow(Guest.Username);
            ticketOverviewWindow.ShowDialog();
        }

        //private void ResetSearch(object sender, RoutedEventArgs e)
        //{
        //    UpdateToursList(TourController.GetAll());
        //    searchLocation = "";
        //    searchLanguage = "";
        //    searchDuration = "";
        //    searchMaxNumberOfGuests = "";
        //    tbLocation.Clear();
        //    tbLanguage.Clear();
        //    tbDuration.Clear();
        //    tbMaxNumberOfGuests.Clear();
        //}

    }
}
