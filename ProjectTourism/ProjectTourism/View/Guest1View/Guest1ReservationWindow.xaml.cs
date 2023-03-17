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
            //this.SelectedAccommodation = AccommodationController.GetOne();
            ReservationController = new ReservationController();
            Reservations = new ObservableCollection<Reservation>(ReservationController.GetAll());


            StartDatePicker.DisplayDate = DateTime.Now;
            startingDate = DateOnly.FromDateTime(DateTime.Now);
            EndDatePicker.DisplayDate = DateTime.Now;
            endingDate = DateOnly.FromDateTime(DateTime.Now);

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

            if (ReservationController.IsPossible(Reservation)
                && reservedDaysCount >= Reservation.Accommodation.MinDaysForReservation
                && GuestCount <= Reservation.Accommodation.MaxNumberOfGuests)
            {
                ReservationController.Add(Reservation);
                MessageBox.Show("Accommodation reserved successfully!");
                Close();
            }
            else{
                MessageBox.Show("Jebi se " + GuestCount + " jebi se");
                MessageBox.Show("Jebi se " + GuestCount + " jebi se");
            }
        }
    }
}
