using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
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

namespace ProjectTourism.View.Guest1View
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public Guest1 Guest1 { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }
        public Guest1Controller Guest1Controller { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public string NameSearch { get; set; }
        public string LocationSearch { get; set; }
        public string GuestCountSearch { get; set; }
        public string ReservationDaysSearch { get; set; }


        public Guest1MainWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1Controller = new Guest1Controller();
            Guest1 = Guest1Controller.GetOne(username);
            AccommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(AccommodationController.GetAll());
            FilteredAccommodations = new ObservableCollection<Accommodation>(Accommodations);

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool ReservationDaysMatch(string ReservationDaysSearchQuery, Accommodation accommodation)
        {
            if (ReservationDaysSearchQuery != null)
            {
                if (!ReservationDaysSearchQuery.Equals(""))
                {
                    int search = int.Parse(ReservationDaysSearchQuery);
                    int minReservationDaysCount = accommodation.MinDaysForReservation;

                    if (search >= minReservationDaysCount)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool GuestNumberMatch(string GuestNumberSearchQuery, Accommodation accommodation)
        {
            if (GuestNumberSearchQuery != null)
            {
                if (!GuestNumberSearchQuery.Equals(""))
                {
                    int search = int.Parse(GuestNumberSearchQuery);
                    int maxGuestCount = accommodation.MaxNumberOfGuests;

                    if (search <= maxGuestCount)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool NameMatch(string NameQuery, Accommodation accommodation)
        {
            if (NameQuery != null)
            {
                if(!NameQuery.Equals(""))
                {
                    string search = NameQuery.ToLower().Trim();

                    string name = accommodation.Name;
                    name = name.ToLower();

                    if (name.Contains(search))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;   
            }
        }

        private bool LocationMatch(string LocationQuery, Accommodation accommodation)
        {
            if (LocationQuery != null)
            {
                if (LocationQuery != "")
                {

                    string Search = LocationQuery.ToLower().Trim();
                    string[] Query = Search.ToLower().Split(',');
                    int i = 0;
                    foreach (string query in Query)
                    {
                        string currentString;
                        currentString = query.Trim();
                        if (currentString == "")
                        {

                        }
                        else
                        {
                            Query[i] = currentString;
                            i++;
                        }
                    }
                    string country = accommodation.Location.Country;
                    string city = accommodation.Location.City;
                    country = country.ToLower();
                    city = city.ToLower();
                    if (Query.Length == 1 && (country.Contains(Search) || city.Contains(Search)))
                    {
                        return true;
                        
                    }
                    else if (Query.Length == 2 && (city.Contains(Query[0]) && country.Contains(Query[1])))

                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool TypeMatch (Accommodation accommodation)
        {
            if (ComboType.SelectedIndex == 0 && accommodation.Type == ACCOMMODATIONTYPE.APARTMENT)
            {
                return true;
            }
            else if (ComboType.SelectedIndex == 1 && accommodation.Type == ACCOMMODATIONTYPE.HOUSE)
            {
                return true;
            }
            else if (ComboType.SelectedIndex == 2 && accommodation.Type == ACCOMMODATIONTYPE.HUT)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FilterAccommodationsClick(object sender, RoutedEventArgs e)
        {
            FilteredAccommodations.Clear();

            

            foreach (Accommodation accommodation in Accommodations)
            {
                if(ReservationDaysMatch(ReservationDaysSearch, accommodation) 
                    && GuestNumberMatch(GuestCountSearch, accommodation)
                    && NameMatch(NameSearch, accommodation)
                    && LocationMatch(LocationSearch, accommodation)
                    && TypeMatch(accommodation))
                {
                    FilteredAccommodations.Add(accommodation);
                }
            }

            /*if (GuestCountSearch != "")
            {
                int search = int.Parse(GuestCountSearch);

                foreach (Accommodation accommodation in Accommodations)
                {
                    int maxGuestCount = accommodation.MaxNumberOfGuests;

                    if (search <= maxGuestCount)
                    {
                        FilteredAccommodations.Add(accommodation);
                    }
                }
            }
            else
            {
                foreach (Accommodation accommodation in Accommodations)
                {
                    FilteredAccommodations.Add(accommodation);
                }
            }



            if (LocationSearch != "")
            {

                string Search = LocationSearch.ToLower().Trim();
                string[] Query = Search.ToLower().Split(',');
                int i = 0;
                foreach (string query in Query)
                {
                    string currentString;
                    currentString = query.Trim();
                    if (currentString == "")
                    {

                    }
                    else
                    {
                        Query[i] = currentString;
                        i++;
                    }
                }

                foreach (Accommodation accommodation in Accommodations)
                {
                    string country = accommodation.Location.Country;
                    string city = accommodation.Location.City;
                    country = country.ToLower();
                    city = city.ToLower();
                    if (Query.Length == 1)
                    {
                        if (accommodation.Location.Country.Equals(Search, StringComparison.OrdinalIgnoreCase)
                            || accommodation.Location.City.Equals(Search, StringComparison.OrdinalIgnoreCase)
                            || country.Contains(Search) || city.Contains(Search))
                        {
                            FilteredAccommodations.Add(accommodation);
                        }
                    }
                    else if (Query.Length == 2)
                    {
                        if (city.Contains(Query[0]) && country.Contains(Query[1]))
                        {
                            FilteredAccommodations.Add(accommodation);
                        }
                    }
                    else
                    {
                        FilteredAccommodations.Clear();
                    }
                }
            }
            else
            {
                foreach (Accommodation accommodation in Accommodations)
                {
                    FilteredAccommodations.Add(accommodation);
                }
            }


            if(NameSearch != "")
            {
                string search = NameSearch.ToLower().Trim();
                
                foreach (Accommodation accommodation in Accommodations)
                {
                    string name = accommodation.Name;
                    name = name.ToLower();
                    
                    if (accommodation.Name.Equals(search, StringComparison.OrdinalIgnoreCase) || name.Contains(search))
                    {
                        FilteredAccommodations.Add(accommodation);
                    }  
                }
            }
            else
            {
                foreach (Accommodation accommodation in Accommodations)
                {
                    FilteredAccommodations.Add(accommodation);
                }
            }

            foreach (Accommodation accommodation in Accommodations)
            {
                if (ComboType.SelectedIndex == 0 && accommodation.Type == ACCOMMODATIONTYPE.APARTMENT)
                {   
                    FilteredAccommodations.Add(accommodation);
                }
                else if (ComboType.SelectedIndex == 1 && accommodation.Type == ACCOMMODATIONTYPE.HOUSE)
                {               
                    FilteredAccommodations.Add(accommodation);
                }
                else if (accommodation.Type == ACCOMMODATIONTYPE.HUT)
                {                   
                    FilteredAccommodations.Add(accommodation);
                }
            }*/
        }
        public void Update()
        {

        }
    }
}
