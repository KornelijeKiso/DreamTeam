using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ProjectTourism.WPF.ViewModel.Guest1ViewModel;
using ProjectTourism.DTO;
using ProjectTourism.View.Guest1View;

namespace ProjectTourism.WPF.View.Guest1View
{
    /// <summary>
    /// Interaction logic for Guest1Window.xaml
    /// </summary>
    public partial class Guest1Window : UserControl
    {
        public Guest1DTO Guest1 { get; set; }
        public ObservableCollection<AccommodationDTO> AccommodationDTOs { get; set; }
        public AccommodationDTO SelectedAccommodation { get; set; }
        public ObservableCollection<AccommodationDTO> FilteredAccommodations { get; set; }
        public ObservableCollection<ReservationDTO> ReservationDTOs { get; set; }
        public ReservationDTO ReservationDTO { get; set; }
        public ReservationDTO SelectedReservation { get; set; }
        public int GuestCount { get; set; }
        public string NameSearch { get; set; }
        public string LocationSearch { get; set; }
        public string GuestCountSearch { get; set; }
        public DateOnly startingDate { get; set; }
        public DateOnly endingDate { get; set; }


        public Guest1Window(string username)
        {
            InitializeComponent();
            DataContext = this;

            Guest1 = new Guest1DTO(username);
            
            AccommodationService accommodationService = new AccommodationService();
            FilteredAccommodations = new ObservableCollection<AccommodationDTO>(accommodationService.GetAll().Select(r => new AccommodationDTO(r)).ToList().OrderByDescending(a => a.Owner.IsSuperHost).ToList());
            AccommodationDTOs = new ObservableCollection<AccommodationDTO>(accommodationService.GetAll().Select(r => new AccommodationDTO(r)).ToList().OrderByDescending(a => a.Owner.IsSuperHost).ToList());
            SelectedReservation = new ReservationDTO(new Reservation());

            SetUpDatePicker();

        }
        public void CancelReservationClick(object sender, RoutedEventArgs e)
        {
            ReserveItem.Visibility = Visibility.Collapsed;
        }

        public void ReserveAccommodationClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            ReservationDTO reservationDTO = new ReservationDTO(new Reservation());
            reservationDTO.AccommodationId = SelectedAccommodation.Id;
            reservationDTO.Guest1Username = Guest1.Username;

            //Content = new Guest1ReservationWindow(reservationDTO, SelectedAccommodation, Guest1.Username);
            ReserveItem.Visibility = Visibility.Visible;
            //Update();
        }
        private void SetUpDatePicker()
        {
            StartDatePicker.DisplayDate = DateTime.Now;
            startingDate = DateOnly.FromDateTime(DateTime.Now);
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(-1)));
            EndDatePicker.DisplayDate = DateTime.Now;
            endingDate = DateOnly.FromDateTime(DateTime.Now);
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(new DateTime(1, 1, 1), DateTime.Now.AddDays(-1)));
            //ReservationService reservationService = new ReservationService();
            //ReservationDTOs = new ObservableCollection<ReservationDTO>(reservationService.GetAll().Select(r => new ReservationDTO(r)).Reverse().ToList());
            //
            //foreach (ReservationDTO reservationDTO in ReservationDTOs)
            //{
            //    if (SelectedReservation.Accommodation.Id == reservationDTO.AccommodationId)
            //    {
            //        StartDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationDTO.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationDTO.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
            //        EndDatePicker.BlackoutDates.Add(new CalendarDateRange(reservationDTO.StartDate.ToDateTime(TimeOnly.Parse("00:00")), reservationDTO.EndDate.ToDateTime(TimeOnly.Parse("00:00"))));
            //    }
            //}
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        
        /*private void StartDateChanged(object sender, SelectionChangedEventArgs e)
        {
            startingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }
        private void EndDateChanged(object sender, SelectionChangedEventArgs e)
        {
            endingDate = DateOnly.FromDateTime((DateTime)(((DatePicker)sender).SelectedDate));
        }
        private bool ReservationAvailable(DateOnly reservationStart, DateOnly reservationEnd, AccommodationDTO accommodationDTO)
        {
            if (!reservationStart.Equals(""))
            {
                ReservationService reservationService = new ReservationService();
                ReservationDTO reservationDTO = new ReservationDTO(new Reservation());
                reservationDTO.StartDate = reservationStart;
                reservationDTO.EndDate = reservationEnd;
                reservationDTO.AccommodationId = accommodationDTO.Id;
                reservationDTO.Guest1Username = Guest1.Username;

                var reservedDaysCount = reservationEnd.DayNumber - reservationStart.DayNumber;

                if (reservationService.IsPossible(reservationDTO.GetReservation()) && reservedDaysCount >= accommodationDTO.MinDaysForReservation)
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

        //private bool TypeMatch(AccommodationVM accommodationVM)
        ////{
        //////    if (ComboType.SelectedIndex == 0)
        ////////    {
        ////////        return true;
        ////////    }
        ////////    if (ComboType.SelectedIndex == 1 && accommodationVM.Type == ACCOMMODATIONTYPE.APARTMENT)
        ////////    {
        ////////        return true;
        ////////    }
        ////////    else if (ComboType.SelectedIndex == 2 && accommodationVM.Type == ACCOMMODATIONTYPE.HOUSE)
        ////////    {
        ////////        return true;
        ////////    }
        ////////    else if (ComboType.SelectedIndex == 3 && accommodationVM.Type == ACCOMMODATIONTYPE.HUT)
        ////////    {
        ////////        return true;
        ////////    }
        ////////    else
        ////////    {
        //////        return false;
        ////    }
        //}

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
                                && NameMatch(NameSearch, accommodationVM);
            //&& TypeMatch(accommodationVM);
        }
        */

        /*
        public void MyReservationsClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string username = Guest1VM.Username;

            Guest1ReservedAccommodations guest1ReservedAccommodations = new Guest1ReservedAccommodations(username);
            //guest1ReservedAccommodations.ShowDialog();
            Update();
        }
        public void ShowGradableClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string username = Guest1VM.Username;

            GradableAccommodationsWindow gradableAccommodationsWindow = new GradableAccommodationsWindow(username);
            //gradableAccommodationsWindow.ShowDialog();
            Update();
        }

        public void MyProfileClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string username = Guest1VM.Username;

            MyProfileWindow myProfileWindow = new MyProfileWindow(username);
            //myProfileWindow.ShowDialog();
            Update();

        }
        
        public void Update()
        {

        }*/
    }
}
