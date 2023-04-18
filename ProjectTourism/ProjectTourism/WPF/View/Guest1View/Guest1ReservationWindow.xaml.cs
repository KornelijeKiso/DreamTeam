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
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Services;
using ProjectTourism.Repositories;

namespace ProjectTourism.View.Guest1View
{
    /// <summary>
    /// Interaction logic for Guest1ReservationWindow.xaml
    /// </summary>
    
    public partial class Guest1ReservationWindow : Window
    {
        public Guest1 Guest1 { get; set; }
        public Guest1VM Guest1VM { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<AccommodationVM> AccommodationVMs { get; set; }
        public Accommodation Accommodation { get; set; }
        public AccommodationVM AccommodationVM { get; set; }
        public ObservableCollection<Accommodation> FilteredAccommodations { get; set; }
        public Guest1Controller Guest1Controller { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
        public ObservableCollection<ReservationVM> ReservationVMs { get; set; }
        public Reservation Reservation { get; set; }
        public ReservationVM ReservationVM { get; set; }
        //public ReservationController reservationService { get; set; }
        public int GuestCount { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }
        
        public Guest1ReservationWindow(ReservationVM reservationVM, AccommodationVM accommodationVM)
        {
            InitializeComponent();
            DataContext = this;
            ReservationVM = new ReservationVM(new Reservation());
            SetReservation(reservationVM, accommodationVM);
            //this.reservationService = new ReservationController();
            //ReservationVMs = new ObservableCollection<Reservation>(ReservationController.GetAll());
            ReservationService reservationService = new ReservationService(new ReservationRepository());
            ReservationVMs = new ObservableCollection<ReservationVM>(reservationService.GetAll().Select(r => new ReservationVM(r)).Reverse().ToList());
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

            foreach (ReservationVM reservationVM in ReservationVMs)
            {
                if (ReservationVM.Accommodation.Id == reservationVM.AccommodationId)
                {
                    StartDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationVM.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationVM.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationVM.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationVM.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                }
            }
        }

        private void SetReservation(ReservationVM reservationVM, AccommodationVM accommodationVM)
        {
            ReservationVM.StartDate = startingDate; //reservationVM.StartDate;
            ReservationVM.EndDate = endingDate; //reservationVM.EndDate;
            ReservationVM.Guest1 = reservationVM.Guest1;
            ReservationVM.Guest1Username = reservationVM.Guest1Username;
            ReservationVM.AccommodationId = accommodationVM.Id;
            ReservationVM.Accommodation = accommodationVM;
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
            ReservationVM.StartDate = startingDate;
            ReservationVM.EndDate = endingDate;

            var reservedDaysCount = Reservation.EndDate.DayNumber - Reservation.StartDate.DayNumber;

            if (reservedDaysCount >= (Reservation.Accommodation.MinDaysForReservation - 1) || reservedDaysCount < 0)
            {
                if (GuestCount <= Reservation.Accommodation.MaxNumberOfGuests)
                {
                    if (GuestCount > 0)
                    {
                        if (reservedDaysCount >= 0)
                        {
                            Guest1VM.ProcessReservation(ReservationVM);
                            if (Guest1VM.ProcessReservation(ReservationVM)) 
                            {
                                MessageBox.Show("Accommodation reserved successfully!");
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Selected Accommodation isn't available for the chosen date. \nTake a look at available dates?");
                                MessageBox.Show("First available date is: " + Reservation.StartDate + " - " + Reservation.EndDate);
                            }
                            //ReservationService reservationService = new ReservationService(new ReservationRepository());
                            //if (reservationService.IsPossible(Reservation))
                            //{
                            //    BookAccommodation();
                            //}
                            //else
                            //{
                            //    FindFirstAvailableAccommodation();
                            //}
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

        //private void FindFirstAvailableAccommodation()
        //{
        //    MessageBox.Show("Selected Accommodation isn't available for the chosen date. \nTake a look at available dates?");
        //    ReservationService reservationService = new ReservationService(new ReservationRepository());
        //    while (!reservationService.IsPossible(ReservationVM.GetReservation()))
        //    {
        //        Reservation.StartDate = Reservation.StartDate.AddDays(1);
        //        Reservation.EndDate = Reservation.EndDate.AddDays(1);
        //    }
        //    MessageBox.Show("First available date is: " + Reservation.StartDate + " - " + Reservation.EndDate);
        //}
        //
        //private void BookAccommodation()
        //{
        //    ReservationService reservationService = new ReservationService(new ReservationRepository());
        //    reservationService.Add(Reservation);
        //    MessageBox.Show("Accommodation reserved successfully!");
        //    Close();
        //}
    }
}
