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
    /// Interaction logic for Guest1ReservationWindow.xaml
    /// </summary>
    
    public partial class Guest1ReservationWindow : Window
    {
        public Guest1 Guest1 { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation Accommodation { get; set; }
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }
        public Guest1Controller Guest1Controller { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
        public Reservation Reservation { get; set; }
        public ReservationController ReservationController { get; set; }
        public int GuestCount { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }
        
        public Guest1ReservationWindow(Reservation reservation, Accommodation accommodation)
        {
            InitializeComponent();
            DataContext = this;
            Reservation = new Reservation();
            SetReservation(reservation, accommodation);
            ReservationController = new ReservationController();
            Reservations = new ObservableCollection<Reservation>(ReservationController.GetAll());
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

            foreach (Reservation reservation in Reservations)
            {
                if (Reservation.Accommodation.Id == reservation.AccommodationId)
                {
                    StartDatePicker.BlackoutDates.Add(new CalendarDateRange(reservation.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservation.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(reservation.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservation.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                }
            }
        }

        private void SetReservation(Reservation reservation, Accommodation accommodation)
        {
            Reservation.StartDate = reservation.StartDate;
            Reservation.EndDate = reservation.EndDate;
            Reservation.Guest1 = reservation.Guest1;
            Reservation.Guest1Username = reservation.Guest1Username;
            Reservation.AccommodationId = accommodation.Id;
            Reservation.Accommodation = accommodation;
        }

        private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            startingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }
        private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }

        public void ConfirmReservationClick(object sender, RoutedEventArgs e)
        {
            Reservation.StartDate = startingDate;
            Reservation.EndDate = endingDate;
            
            var reservedDaysCount = Reservation.EndDate.DayNumber - Reservation.StartDate.DayNumber;

            if (reservedDaysCount >= (Reservation.Accommodation.MinDaysForReservation - 1) || reservedDaysCount < 0)
            {
                if (GuestCount <= Reservation.Accommodation.MaxNumberOfGuests)
                {
                    if (GuestCount > 0)
                    {
                        if (reservedDaysCount >= 0)
                        {
                            if (ReservationController.IsPossible(Reservation))
                            {
                                BookAccommodation();
                            }
                            else
                            {
                                FindFirstAvailableAccommodation();
                            }
                        }
                        else
                        {MessageBox.Show("Invalid date format");}
                    }
                    else
                    {MessageBox.Show("At least 1 guest is required");}
                }
                else
                {MessageBox.Show("Maximum number of guests is " + Reservation.Accommodation.MaxNumberOfGuests + ".");}
            }
            else
            {MessageBox.Show("At least " + Reservation.Accommodation.MinDaysForReservation + " days must be reserved");}
        }

        private void FindFirstAvailableAccommodation()
        {
            MessageBox.Show("Selected Accommodation isn't available for the chosen date. \nTake a look at available dates?");
            while (!ReservationController.IsPossible(Reservation))
            {
                Reservation.StartDate = Reservation.StartDate.AddDays(1);
                Reservation.EndDate = Reservation.EndDate.AddDays(1);
            }
            MessageBox.Show("First available date is: " + Reservation.StartDate + " - " + Reservation.EndDate);
        }

        private void BookAccommodation()
        {
            ReservationController.Add(Reservation);
            MessageBox.Show("Accommodation reserved successfully!");
            Close();
        }
    }
}
