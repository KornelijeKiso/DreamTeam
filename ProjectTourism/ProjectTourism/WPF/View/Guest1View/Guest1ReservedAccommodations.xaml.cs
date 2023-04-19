using ProjectTourism.Model;
using ProjectTourism.Observer;
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
    public partial class Guest1ReservedAccommodations : Window, INotifyPropertyChanged
    {
        public ReservationVM SelectedReservation { get; set; }
        public Guest1VM Guest1VM { get; set; }
        public Guest1ReservedAccommodations(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1VM = new Guest1VM(username);
        }

        private void CancelReservationClick(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation.StartDate > DateOnly.FromDateTime(DateTime.Now).AddDays(SelectedReservation.Accommodation.CancellationDeadline))
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this reservation?", "Delete appointment", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Guest1VM.CancelReservation(SelectedReservation);
                    }
                }
            else
                MessageBox.Show("The tour can not be canceled because the cancelation time is at least" + SelectedReservation.Accommodation.CancellationDeadline + "days before start.");
        }

        private void PostponeReservationClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            ReservationVM reservationVM = new ReservationVM(new Reservation());
            reservationVM.AccommodationId = SelectedReservation.AccommodationId;
            reservationVM.Accommodation = SelectedReservation.Accommodation;
            reservationVM.Guest1Username = Guest1VM.Username;

            PostponeReservationWindow postponeReservationWindow = new PostponeReservationWindow(reservationVM, Guest1VM.Username);
            postponeReservationWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
