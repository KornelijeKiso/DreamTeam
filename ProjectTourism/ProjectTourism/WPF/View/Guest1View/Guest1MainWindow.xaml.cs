using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
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
using ProjectTourism.Services;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.View.Guest1View;

namespace ProjectTourism.View.Guest1View
{
    /// <summary>
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public Guest1VM Guest1VM { get; set; }
        public ObservableCollection<AccommodationVM> AccommodationVMs { get; set; }
        public AccommodationVM SelectedAccommodation { get; set; }
        public ObservableCollection<AccommodationVM> FilteredAccommodations { get; set; }
        public string NameSearch { get; set; }
        public string LocationSearch { get; set; }
        public string GuestCountSearch { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }


        public Guest1MainWindow(string username)
        {
            InitializeComponent();
            DataContext = this;

            Guest1VM = new Guest1VM(username);

            AccommodationService accommodationService = new AccommodationService(new AccommodationRepository());
            FilteredAccommodations = new ObservableCollection<AccommodationVM>(accommodationService.GetAll().Select(r => new AccommodationVM(r)).ToList().OrderByDescending(a => a.Owner.IsSuperHost).ToList());
            AccommodationVMs = new ObservableCollection<AccommodationVM>(accommodationService.GetAll().Select(r => new AccommodationVM(r)).ToList().OrderByDescending(a => a.Owner.IsSuperHost).ToList());

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
        private bool ReservationAvailable(DateOnly reservationStart, DateOnly reservationEnd, AccommodationVM accommodationVM)
        {
            if (!reservationStart.Equals(""))
            {
                ReservationService reservationService = new ReservationService(new ReservationRepository());
                ReservationVM reservationVM = new ReservationVM(new Reservation());
                reservationVM.StartDate = reservationStart;
                reservationVM.EndDate = reservationEnd;
                reservationVM.AccommodationId = accommodationVM.Id;
                reservationVM.Guest1Username = Guest1VM.Username;

                var reservedDaysCount = reservationEnd.DayNumber - reservationStart.DayNumber;

                if (reservationService.IsPossible(reservationVM.GetReservation()) && reservedDaysCount >= accommodationVM.MinDaysForReservation)
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

        private bool GuestNumberMatch(string GuestNumberQuery, AccommodationVM accommodationVM)
        {
            if (!string.IsNullOrEmpty(GuestNumberQuery))
            {
                int search = int.Parse(GuestNumberQuery);
                int maxGuestCount = accommodationVM.MaxNumberOfGuests;

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

        private bool NameMatch(string NameQuery, AccommodationVM accommodationVM)
        {
            if (!string.IsNullOrEmpty(NameQuery))
            {
                string search = NameQuery.ToLower().Trim();

                string name = accommodationVM.Name;
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

        private bool TypeMatch(AccommodationVM accommodationVM)
        {
            if (ComboType.SelectedIndex == 0)
            {
                return true;
            }
            if (ComboType.SelectedIndex == 1 && accommodationVM.Type == ACCOMMODATIONTYPE.APARTMENT)
            {
                return true;
            }
            else if (ComboType.SelectedIndex == 2 && accommodationVM.Type == ACCOMMODATIONTYPE.HOUSE)
            {
                return true;
            }
            else if (ComboType.SelectedIndex == 3 && accommodationVM.Type == ACCOMMODATIONTYPE.HUT)
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

            foreach (AccommodationVM accommodationVM in AccommodationVMs)
            {
                if (Fits(accommodationVM))
                {
                    FilteredAccommodations.Add(accommodationVM);
                }
            }
        }

        private bool Fits(AccommodationVM accommodationVM)
        {
            return ReservationAvailable(startingDate, endingDate, accommodationVM)
                                && GuestNumberMatch(GuestCountSearch, accommodationVM)
                                && NameMatch(NameSearch, accommodationVM)
                                && TypeMatch(accommodationVM);
        }

        public void ReserveAccommodationClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            ReservationVM reservationVM = new ReservationVM(new Reservation());
            reservationVM.AccommodationId = SelectedAccommodation.Id;
            reservationVM.Guest1Username = Guest1VM.Username;

            Guest1ReservationWindow guest1ReservationWindow = new Guest1ReservationWindow(reservationVM, SelectedAccommodation, Guest1VM.Username);
            guest1ReservationWindow.ShowDialog();
            Update();
        }

        public void MyReservationsClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string username = Guest1VM.Username;

            Guest1ReservedAccommodations guest1ReservedAccommodations = new Guest1ReservedAccommodations(username);
            guest1ReservedAccommodations.ShowDialog();
            Update();
        }
        public void ShowGradableClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string username = Guest1VM.Username;

            GradableAccommodationsWindow gradableAccommodationsWindow = new GradableAccommodationsWindow(username);
            gradableAccommodationsWindow.ShowDialog();
            Update();
        }

        public void Update()
        {

        }
    }
}
