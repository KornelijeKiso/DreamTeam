using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
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
        public Guest1VM Guest1VM { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }
        public Guest1Controller Guest1Controller { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation Reservation { get; set; }
        public ReservationController ReservationController { get; set; }
        public string NameSearch { get; set; }
        public string LocationSearch { get; set; }
        public string GuestCountSearch { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }


public Guest1MainWindow(string username)
        {
            InitializeComponent();
            DataContext = this;



            Guest1Controller = new Guest1Controller();
            Guest1VM = new Guest1VM(username);
            Guest1 = Guest1Controller.GetOne(username);
            AccommodationController = new AccommodationController();
            Accommodations = new ObservableCollection<Accommodation>(AccommodationController.GetAll());
            ReservationController = new ReservationController();
            Reservations = new ObservableCollection<Reservation>(ReservationController.GetAll());
            FilteredAccommodations = new ObservableCollection<Accommodation>(Accommodations);

            SetUpDatePicker();

        }

        private void SetUpDatePicker()
        {
            StartDatePicker.DisplayDate = DateTime.Now;
            startingDate = DateOnly.FromDateTime(DateTime.Now);
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(-1)));
            EndDatePicker.DisplayDate = DateTime.Now;
            endingDate = DateOnly.FromDateTime(DateTime.Now);
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(-1)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            startingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }
        private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }
        private bool ReservationAvailable(DateOnly reservationStart, DateOnly reservationEnd, Accommodation accommodation)
        {
            if (!reservationStart.Equals(""))
            {
                ReservationController reservationController = new ReservationController();
                Reservation reservation = new Reservation();
                reservation.StartDate = reservationStart;
                reservation.EndDate = reservationEnd;
                reservation.AccommodationId = accommodation.Id;
                reservation.Guest1Username = Guest1.Username;

                var reservedDaysCount = reservationEnd.DayNumber - reservationStart.DayNumber;

                if (reservationController.IsPossible(reservation) && reservedDaysCount >= accommodation.MinDaysForReservation)
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

        private bool GuestNumberMatch(string GuestNumberQuery, Accommodation accommodation)
        {
            if (GuestNumberQuery != null)
            {
                if (!GuestNumberQuery.Equals(""))
                {
                    int search = int.Parse(GuestNumberQuery);
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
                    string Search, country, city;
                    string[] Query;
                    PrepareSearch(LocationQuery, accommodation, out Search, out Query, out country, out city);
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

        private static void PrepareSearch(string LocationQuery, Accommodation accommodation, out string Search, out string[] Query, out string country, out string city)
        {
            Search = LocationQuery.ToLower().Trim();
            Query = Search.ToLower().Split(',');
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
            country = accommodation.Location.Country;
            city = accommodation.Location.City;
            country = country.ToLower();
            city = city.ToLower();
        }

        private bool TypeMatch (Accommodation accommodation)
        {
            if (ComboType.SelectedIndex == 0)
            {
                return true;
            }
            if (ComboType.SelectedIndex == 1 && accommodation.Type == ACCOMMODATIONTYPE.APARTMENT)
            {
                    return true;
            }
            else if (ComboType.SelectedIndex == 2 && accommodation.Type == ACCOMMODATIONTYPE.HOUSE)
            {
                return true;
            }
            else if (ComboType.SelectedIndex == 3 && accommodation.Type == ACCOMMODATIONTYPE.HUT)
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
                if (Fits(accommodation))
                {
                    FilteredAccommodations.Add(accommodation);
                }

            }
        }

        private bool Fits(Accommodation accommodation)
        {
            return ReservationAvailable(startingDate, endingDate, accommodation)
                                && GuestNumberMatch(GuestCountSearch, accommodation)
                                && NameMatch(NameSearch, accommodation)
                                && LocationMatch(LocationSearch, accommodation)
                                && TypeMatch(accommodation);
        }

        public void ReserveAccommodationClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Reservation reservation;
            Accommodation accommodation;
            PrepareReservation(out reservation, out accommodation);

            Guest1ReservationWindow guest1ReservationWindow = new Guest1ReservationWindow(reservation, accommodation);
            guest1ReservationWindow.ShowDialog();
            Update();
        }

        private void PrepareReservation(out Reservation reservation, out Accommodation accommodation)
        {
            reservation = new Reservation();
            ReservationController reservationController = new ReservationController();
            reservation.StartDate = startingDate;
            reservation.EndDate = endingDate;
            reservation.AccommodationId = SelectedAccommodation.Id;
            reservation.Guest1Username = Guest1.Username;
            accommodation = AccommodationController.GetOne(reservation.AccommodationId);
        }

        public void Update()
        {

        }
    }
}
