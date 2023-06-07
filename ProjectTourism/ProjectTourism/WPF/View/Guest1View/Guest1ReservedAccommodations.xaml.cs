using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.Utilities;
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

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for Guest1ReservedAccommodations.xaml
    /// </summary>
    public partial class Guest1ReservedAccommodations : UserControl, INotifyPropertyChanged
    {
        public ReservationDTO SelectedReservation { get; set; }
        public Guest1DTO Guest1 { get; set; }
        public ObservableCollection<ReservationDTO> ReservationDTOs { get; set; }
        public ReservationDTO ReservationDTO { get; set; }
        public int GuestCount { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }
        public Guest1ReservedAccommodations(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1 = new Guest1DTO(username);

            ReservationDTO = new ReservationDTO(new Reservation());
        }

        private void CancelReservationClick(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation.StartDate > DateOnly.FromDateTime(DateTime.Now).AddDays(SelectedReservation.Accommodation.CancellationDeadline))
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this reservation?", "Delete appointment", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Guest1.CancelReservation(SelectedReservation);
                    }
                }
            else
                MessageBox.Show("The tour can not be canceled because the cancelation date is " + SelectedReservation.Accommodation.CancellationDeadline + " days before start.");
        }

        public void CancelPostponeClick(object sender, RoutedEventArgs e)
        {
            PostponeItem.Visibility = Visibility.Collapsed;
        }
        private void PostponeReservationClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            ReservationDTO reservationDTO = new ReservationDTO(new Reservation());
            reservationDTO.Id = SelectedReservation.Id;
            reservationDTO.AccommodationId = SelectedReservation.AccommodationId;
            reservationDTO.Accommodation = SelectedReservation.Accommodation;
            reservationDTO.Guest1Username = Guest1.Username;
            SetUpDatePicker();
            PostponeItem.Visibility = Visibility.Visible;
            //Content = new PostponeReservationWindow(reservationDTO, Guest1.Username);
            //PostponeReservationWindow postponeReservationWindow = new PostponeReservationWindow(reservationDTO, Guest1.Username);
            //postponeReservationWindow.ShowDialog();
        }
        //public void SwitchToPostponeReservation(object parameter)
        //{
        //    ReservationVM reservationVM = new ReservationVM(new Reservation());
        //    reservationVM.Id = SelectedReservation.Id;
        //    reservationVM.AccommodationId = SelectedReservation.AccommodationId;
        //    reservationVM.Accommodation = SelectedReservation.Accommodation;
        //    reservationVM.Guest1Username = Guest1.Username;
        //    Content = new PostponeReservationWindow(reservationVM, Guest1.Username);
        //}
        //public ICommand SwitchToPostponereservationCommand
        //{
        //    get => new RelayCommand(SwitchToPostponeReservation);
        //}
        private void SetUpDatePicker()
        {
            StartDatePicker.DisplayDate = DateTime.Now;
            startingDate = DateOnly.FromDateTime(DateTime.Now);
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(-1)));
            EndDatePicker.DisplayDate = DateTime.Now;
            endingDate = DateOnly.FromDateTime(DateTime.Now);
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(-1)));
            ReservationService reservationService = new ReservationService();
            ReservationDTOs = new ObservableCollection<ReservationDTO>(reservationService.GetAll().Select(r => new ReservationDTO(r)).Reverse().ToList());

            foreach (ReservationDTO reservationDTO in ReservationDTOs)
            {
                if (SelectedReservation.AccommodationId == reservationDTO.AccommodationId)
                {
                    StartDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationDTO.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationDTO.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationDTO.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationDTO.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                }
            }
        }

        private void SetReservation(ReservationDTO reservationDTO)
        {
            ReservationDTO.Id = reservationDTO.Id;
            ReservationDTO.StartDate = startingDate;
            ReservationDTO.EndDate = endingDate;
            ReservationDTO.Guest1 = reservationDTO.Guest1;
            ReservationDTO.Guest1Username = reservationDTO.Guest1Username;
            ReservationDTO.AccommodationId = reservationDTO.AccommodationId;
            ReservationDTO.Accommodation = reservationDTO.Accommodation;
        }

        //private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    startingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        //}
        //private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    endingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        //}

        public void ConfirmPostponeClick(object sender, RoutedEventArgs e)
        {
            startingDate = DateOnly.FromDateTime((DateTime)StartDatePicker.SelectedDate);
            endingDate = DateOnly.FromDateTime((DateTime)EndDatePicker.SelectedDate);
            ReservationDTO.StartDate = startingDate;
            ReservationDTO.EndDate = endingDate;

            var reservedDaysCount = ReservationDTO.EndDate.DayNumber - ReservationDTO.StartDate.DayNumber;

            if (reservedDaysCount >= (ReservationDTO.Accommodation.MinDaysForReservation - 1) || reservedDaysCount < 0)
            {
                if (reservedDaysCount >= 0)
                {
                    if (Guest1.ProcessRequest(ReservationDTO))
                    {
                        MessageBox.Show("Postpone request sent successfully!");
                        //Close();
                    }
                    else
                    {
                        MessageBox.Show("Selected Accommodation isn't available for the chosen date. \nTake a look at available dates?");
                        MessageBox.Show("First available date is: " + ReservationDTO.StartDate + " - " + ReservationDTO.EndDate);
                    }
                }
                else
                { MessageBox.Show("Invalid date format"); }
            }
            else
            { MessageBox.Show("At least " + ReservationDTO.Accommodation.MinDaysForReservation + " days must be reserved"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
