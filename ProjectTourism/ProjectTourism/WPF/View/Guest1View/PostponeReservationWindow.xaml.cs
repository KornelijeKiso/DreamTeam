using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for PostponeReservationWindow.xaml
    /// </summary>
    public partial class PostponeReservationWindow : UserControl
    {
        public Guest1DTO Guest1DTO { get; set; }
        public ObservableCollection<ReservationDTO> ReservationDTOs { get; set; }
        public ReservationDTO ReservationDTO { get; set; }
        public int GuestCount { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }
        public PostponeReservationWindow(ReservationDTO reservationDTO, string username)
        {
            InitializeComponent();
            //DataContext = this;
            ReservationDTO = new ReservationDTO(new Reservation());
            SetReservation(reservationDTO);
            Guest1DTO = new Guest1DTO(username);
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
            ReservationService reservationService = new ReservationService();
            ReservationDTOs = new ObservableCollection<ReservationDTO>(reservationService.GetAll().Select(r => new ReservationDTO(r)).Reverse().ToList());

            foreach (ReservationDTO reservationDTO in ReservationDTOs)
            {
                if (ReservationDTO.AccommodationId == reservationDTO.AccommodationId)
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

        private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            startingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }
        private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }

        public void ConfirmPostponeClick(object sender, RoutedEventArgs e)
        {
            ReservationDTO.StartDate = startingDate;
            ReservationDTO.EndDate = endingDate;

            var reservedDaysCount = ReservationDTO.EndDate.DayNumber - ReservationDTO.StartDate.DayNumber;

            if (reservedDaysCount >= (ReservationDTO.Accommodation.MinDaysForReservation - 1) || reservedDaysCount < 0)
            {
                if (reservedDaysCount >= 0)
                {
                    if (Guest1DTO.ProcessRequest(ReservationDTO))
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
    }
}
