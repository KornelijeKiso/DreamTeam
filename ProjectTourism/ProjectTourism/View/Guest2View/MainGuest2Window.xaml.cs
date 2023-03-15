﻿using System;
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
        public ObservableCollection<Route> Routes { get; set; }
        //public GuideController GuideController { get; set; }

        public string search { get; set; }

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
            search = "";

            searchLocation = "";
            searchLanguage = "";
            searchDuration = "";
            searchMaxNumberOfGuests = "";

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

        
        private void SearchLocation(object sender, RoutedEventArgs e)
        {
            List<Route> routes = new List<Route>();
            if (searchLocation != "")
            {
                string[] searchQuery = searchLocation.ToLower().Split(',');
                string searchFirst = searchQuery[0].Trim();
                string searchSecond = searchQuery[1].Trim();
                foreach (Route r in Routes) 
                {
                    if ((r.Location.City.Contains(searchFirst, StringComparison.OrdinalIgnoreCase) && r.Location.Country.Contains(searchSecond, StringComparison.OrdinalIgnoreCase))
                || (r.Location.Country.Contains(searchFirst, StringComparison.OrdinalIgnoreCase) && r.Location.City.Contains(searchSecond, StringComparison.OrdinalIgnoreCase)))
                    {
                        routes.Add(r);
                    }
                }

                UpdateRoutesList(routes);
            }
            else UpdateRoutesList(RouteController.GetAll());
        }

        private void SearchLanguage(object sender, RoutedEventArgs e)
        {
            List<Route> routes = new List<Route>();
            if (searchLanguage != "")
            {
                foreach (Route r in Routes) 
                {
                    if (r.Language.Contains(searchLanguage, StringComparison.OrdinalIgnoreCase))
                    {
                        routes.Add(r);
                    }
                }

                UpdateRoutesList(routes);
            }
            else UpdateRoutesList(RouteController.GetAll());
        }

        private void SearchDuration(object sender, RoutedEventArgs e)
        {
            List<Route> routes = new List<Route>();
            if (searchDuration != "")
            {
                foreach (Route r in Routes) 
                {
                    if (r.Duration.ToString().Contains(searchDuration, StringComparison.OrdinalIgnoreCase))
                    {
                        routes.Add(r);
                    }
                }

                UpdateRoutesList(routes);
            }
            else UpdateRoutesList(RouteController.GetAll());
        }

        private void SearchMaxNumberOfGuests(object sender, RoutedEventArgs e)
        {
            List<Route> routes = new List<Route>();
            if (searchMaxNumberOfGuests != "")
            {
                foreach (Route r in Routes) 
                {
                    if (r.MaxNumberOfGuests.ToString().Contains(searchMaxNumberOfGuests, StringComparison.OrdinalIgnoreCase))
                    {
                        routes.Add(r);
                    }
                }

                UpdateRoutesList(routes);
            }
            else UpdateRoutesList(RouteController.GetAll());
        }

        private void ResetSearch(object sender, RoutedEventArgs e)
        {
            UpdateRoutesList(RouteController.GetAll());
            searchLocation = "";
            searchLanguage = "";
            searchDuration = "";
            searchMaxNumberOfGuests = "";
            
            tbLocation.Clear();
            tbLanguage.Clear();
            tbDuration.Clear();
            tbMaxNumberOfGuests.Clear();
        }
    }
}
