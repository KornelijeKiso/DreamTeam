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
using ProjectTourism.ModelDAO;          /// OBRISI kad namestis BuyTicket kako treba

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

        public string search { get; set; }

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
            search = "";

            TicketController = new TicketController();

        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string[] RemoveWhiteSpaces(string search)
        {
            if (search != "")
            {
                //string lowerLetters = search.ToLower().Trim();
                string[] searchQueryWhiteSpaces = search.ToLower().Split(',');
                string[] searchQuery = searchQueryWhiteSpaces;
                int i = 0;
                foreach (string word in searchQueryWhiteSpaces)
                {
                    string noWhiteSpace = word.Trim();
                    if (noWhiteSpace != "")
                    {
                        searchQuery[i] = noWhiteSpace;
                        i++;
                    }
                }
                return searchQuery;
            }
            return null;
        }
        private void Search(object sender, RoutedEventArgs e)
        {
            string[] searchQuery = RemoveWhiteSpaces(search);

            List<Route> routes = new List<Route>();
            if (search != "")
            {
                foreach (Route r in RouteController.GetAll())       //
                {
                    if (searchQuery.Length == 1)
                    {
                        if (IsOneWordSearch(r, searchQuery[0]))         // Language, Duration, MaxNumberOfGuests
                        {
                            routes.Add(r);
                        }
                    }
                    else if (searchQuery.Length == 2)
                    {
                        if (IsTwoWordsSearch(r, searchQuery[0], searchQuery[1]))    // City, Country   || Country, City
                        {
                            routes.Add(r);
                        }
                    }
                    else routes.Clear();
                }

                Routes.Clear();
                foreach (Route route in routes)
                {
                    Routes.Add(route);
                }
            }
            else
            {
                UpdateRoutesList();
            }

        }

        private bool IsOneWordSearch(Route r, string search)
        {   // Language, Duration, MaxNumberOfGuests
            return (r.Language.Contains(search, StringComparison.OrdinalIgnoreCase) || r.Duration.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                            || r.MaxNumberOfGuests.ToString().Contains(search, StringComparison.OrdinalIgnoreCase));
        }
        private bool IsTwoWordsSearch(Route r, string searchFirst, string searchSecond)
        {   // City, Country   || Country, City
            return ((r.Location.City.Contains(searchFirst, StringComparison.OrdinalIgnoreCase) && r.Location.Country.Contains(searchSecond, StringComparison.OrdinalIgnoreCase))
                || (r.Location.Country.Contains(searchFirst, StringComparison.OrdinalIgnoreCase) && r.Location.City.Contains(searchSecond, StringComparison.OrdinalIgnoreCase)));
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
            //UpdateRoutesList();
            throw new NotImplementedException();
        }

        private void BuyTicket(object sender, RoutedEventArgs e)
        {   //
            if (SelectedRoute != null)
            {/*
                int NumberOfGuests = 1;
                Ticket ticket = new Ticket(SelectedRoute.Id, Guest.Username, NumberOfGuests);
                
                TicketDAO ticketDAO = new TicketDAO();
                ticketDAO.Add(ticket);
                */
                CreateTicketWindow createTicketWindow = new CreateTicketWindow(Guest.Username, SelectedRoute.Id);
                createTicketWindow.ShowDialog();

            }

           
        }
    }
}
