using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTourism.Model;
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
using ProjectTourism.DTO;

namespace ProjectTourism.View.Guest1View
{
    /// <summary>
    /// Interaction logic for Guest1ReservationWindow.xaml
    /// </summary>

    public partial class Guest1ReservationWindow : UserControl
    {
        public Guest1DTO Guest1 { get; set; }
        public ObservableCollection<ReservationDTO> ReservationDTOs { get; set; }
        public ReservationDTO ReservationDTO { get; set; }
        public int GuestCount { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }

        public Guest1ReservationWindow(ReservationDTO reservationDTO, AccommodationDTO accommodationDTO, string username)
        {
            InitializeComponent();
            //DataContext = this;
            ReservationDTO = new ReservationDTO(new Reservation());
            SetReservation(reservationDTO, accommodationDTO);
            Guest1 = new Guest1DTO(username);
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
                if (ReservationDTO.Accommodation.Id == reservationDTO.AccommodationId)
                {
                    StartDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationDTO.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationDTO.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationDTO.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationDTO.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
                }
            }
        }

        private void SetReservation(ReservationDTO reservationDTO, AccommodationDTO accommodationDTO)
        {
            ReservationDTO.StartDate = startingDate;
            ReservationDTO.EndDate = endingDate;
            ReservationDTO.Guest1 = reservationDTO.Guest1;
            ReservationDTO.Guest1Username = reservationDTO.Guest1Username;
            ReservationDTO.AccommodationId = accommodationDTO.Id;
            ReservationDTO.Accommodation = accommodationDTO;
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
            ReservationDTO.StartDate = startingDate;
            ReservationDTO.EndDate = endingDate;

            var reservedDaysCount = ReservationDTO.EndDate.DayNumber - ReservationDTO.StartDate.DayNumber;

            if (reservedDaysCount >= (ReservationDTO.Accommodation.MinDaysForReservation - 1) || reservedDaysCount < 0)
            {
                if (GuestCount <= ReservationDTO.Accommodation.MaxNumberOfGuests)
                {
                    if (GuestCount > 0)
                    {
                        if (reservedDaysCount >= 0)
                        {
                            if (Guest1.ProcessReservation(ReservationDTO))
                            {
                                MessageBox.Show("Accommodation reserved successfully!");
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
                    { MessageBox.Show("At least 1 guest is required"); }
                }
                else
                { MessageBox.Show("Maximum number of guests is " + ReservationDTO.Accommodation.MaxNumberOfGuests + "."); }
            }
            else
            { MessageBox.Show("At least " + ReservationDTO.Accommodation.MinDaysForReservation + " days must be reserved"); }
        }

    }
}
