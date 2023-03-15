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
        public RouteController RouteController { get; set; }
        public Route? SelectedRoute { get; set; }
        public TicketController TicketController { get; set; }
        public ObservableCollection<Route> Routes { get; set; }
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
            RouteController = new RouteController();
            Routes = new ObservableCollection<Route>(RouteController.GetAll());

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


        private void UpdateRoutesList()
        {
            Routes.Clear();
            foreach (Route route in RouteController.GetAll())
            {
                Routes.Add(route);
            }
        }

        public void Update()
        {
            UpdateRoutesList();
            throw new NotImplementedException();
        }

        public void UpdateRoutesList(List<Route> routes)
        {
            Routes.Clear();
            foreach (Route route in routes)
            {
                Routes.Add(route);
            }
        }

        private void SearchLocation(ObservableCollection<Route> routes)
        {
            List<Route> routesList = new List<Route>();
            if (searchLocation != "")
            {
                string[] searchQuery = searchLocation.ToLower().Split(',');
                if (searchQuery.Length == 2)
                {
                    foreach (Route r in routes)
                    {
                        if ((r.Location.City.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && r.Location.Country.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase))
                            || (r.Location.Country.Contains(searchQuery[0].Trim(), StringComparison.OrdinalIgnoreCase)
                                && r.Location.City.Contains(searchQuery[1].Trim(), StringComparison.OrdinalIgnoreCase))) 
                                routesList.Add(r);
                    }
                }
                foreach (Route r in routes)
                {
                    if ((r.Location.City.Contains(searchLocation, StringComparison.OrdinalIgnoreCase) 
                        || (r.Location.Country.Contains(searchLocation, StringComparison.OrdinalIgnoreCase)))   )
                        routesList.Add(r);
                }
                UpdateRoutesList(routesList);
            }
        }
        private void SearchLanguage(ObservableCollection<Route> routes)
        {
            List<Route> routesList = new List<Route>();
            if (searchLanguage != "")
            {
                foreach (Route r in routes)
                {
                    if (r.Language.Contains(searchLanguage, StringComparison.OrdinalIgnoreCase))
                        routesList.Add(r);
                }
                UpdateRoutesList(routesList);
            }
        }

        private void SearchDuration(ObservableCollection<Route> routes)
        {
            List<Route> routesList = new List<Route>();
            if (searchDuration != "")
            {
                foreach (Route r in routes)
                {
                    if (r.Duration.ToString().Contains(searchDuration, StringComparison.OrdinalIgnoreCase))
                        routesList.Add(r);
                }
                UpdateRoutesList(routesList);
            }
        }

        private void SearchMaxNumberOfGuests(ObservableCollection<Route> routes)
        {
            List<Route> routesList = new List<Route>();
            if (searchMaxNumberOfGuests != "")
            {
                foreach (Route r in routes)
                {
                    if (r.MaxNumberOfGuests.ToString().Contains(searchMaxNumberOfGuests, StringComparison.OrdinalIgnoreCase))
                        routesList.Add(r);
                }
                UpdateRoutesList(routesList);
            }
        }

        private void SearchOne()
        {
            SearchLocation(Routes);
            SearchDuration(Routes);
            SearchLanguage(Routes);
            SearchMaxNumberOfGuests(Routes);
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            UpdateRoutesList(RouteController.GetAll());
            SearchOne();
        }

        private void BuyTicket(object sender, RoutedEventArgs e)
        {
            if (SelectedRoute != null)
            {
                Ticket ticket = TicketController.GetGuest2Ticket(Guest, SelectedRoute);

                if (ticket == null)
                {
                    CreateTicketWindow createTicketWindow = new CreateTicketWindow(Guest.Username, SelectedRoute.Id);
                    createTicketWindow.ShowDialog();
                }
                else
                {
                    UpdateTicketWindow updateTicketWindow = new UpdateTicketWindow(Guest.Username, SelectedRoute.Id);
                    updateTicketWindow.ShowDialog();
                }
                
            }
            else
                MessageBox.Show("Please select the route.");
        }

        //private void ResetSearch(object sender, RoutedEventArgs e)
        //{
        //    UpdateRoutesList(RouteController.GetAll());
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
