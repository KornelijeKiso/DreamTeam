using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.OwnerView;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for ReservationsMenuItem.xaml
    /// </summary>
    public partial class ReservationsMenuItem : UserControl, INotifyPropertyChanged
    {
        public OwnerVM Owner { get; set; }
        public ReservationVM SelectedReservation { get; set; }
        public ReservationsMenuItem(string username)
        {
            InitializeComponent();
            DataContext = this;
            SetOwner(username);
        }
        private void SetOwner(string username)
        {
            Owner = new OwnerVM(username);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            SetOwner(Owner.Username);        
        }
        public void GradeGuestClick(object sender, RoutedEventArgs e)
        {
            Owner.timer.Stop();
            Button button = (Button)sender;
            GradeGuestWindow gradeGuestWindow = new GradeGuestWindow(SelectedReservation, Owner);
            gradeGuestWindow.ShowDialog();
            if (SelectedReservation.Graded) ShowPopupMessage("You have successfully graded your guest.\n You are now able to see guests review if guest left it.");
            Owner.SetTimer();
        }
        public void SeeReviewClick(object sender, RoutedEventArgs e)
        {
            Owner.timer.Stop();
            Guest1ReviewWindow guestsReviewWindow = new Guest1ReviewWindow(SelectedReservation);
            guestsReviewWindow.ShowDialog();
            Owner.SetTimer();
        }

        private void PostponeRequestClick(object sender, RoutedEventArgs e)
        {
            Owner.timer.Stop();
            PostponeRequestWindow postponeRequestWindow = new PostponeRequestWindow(SelectedReservation);
            postponeRequestWindow.ShowDialog();
            if(SelectedReservation!=null)
            {
                if (SelectedReservation.PostponeRequest.Accepted) ShowPopupMessage("You have successfully accepted postpone request.\n Reservation is postponed for the appointment requested by guest.");
                if (SelectedReservation.PostponeRequest.Rejected) ShowPopupMessage("You have successfully rejected postpone request.\n Your guest will be informed about this action.");
            }
            Owner.SetTimer();
        }
        private async void ShowPopupMessage(string message)
        {
            popupText.Text = message;
            popupContainer.Visibility = Visibility.Visible;
            await Task.Delay(4000);
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(9);
                popupContainer.Opacity += -0.05;
            }
            popupContainer.Visibility = Visibility.Collapsed;
            popupContainer.Opacity = 1.0;
        }
    }
}
