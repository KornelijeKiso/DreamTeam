using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
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
        public Guest1ReservedAccommodations(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1 = new Guest1DTO(username);
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

        private void PostponeReservationClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            ReservationDTO reservationDTO = new ReservationDTO(new Reservation());
            reservationDTO.Id = SelectedReservation.Id;
            reservationDTO.AccommodationId = SelectedReservation.AccommodationId;
            reservationDTO.Accommodation = SelectedReservation.Accommodation;
            reservationDTO.Guest1Username = Guest1.Username;
            Content = new PostponeReservationWindow(reservationDTO, Guest1.Username);
            PostponeReservationWindow postponeReservationWindow = new PostponeReservationWindow(reservationDTO, Guest1.Username);
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
