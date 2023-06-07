using ProjectTourism.Model;
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
    /// Interaction logic for GradableAccommodationsWindow.xaml
    /// </summary>
    public partial class GradableAccommodationsWindow : UserControl, INotifyPropertyChanged
    {
        public ReservationVM SelectedReservation { get; set; }
        public Guest1VM Guest1VM { get; set; }
        public GradableAccommodationsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guest1VM = new Guest1VM(username);
        }
        private void GradeAccommodationClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            ReservationVM reservationVM = new ReservationVM(new Reservation());
            reservationVM.Id = SelectedReservation.Id;
            reservationVM.AccommodationId = SelectedReservation.AccommodationId;
            reservationVM.Accommodation = SelectedReservation.Accommodation;
            reservationVM.Guest1Username = Guest1VM.Username;

            GradeItem.Visibility = Visibility.Visible;

            //GradeAccommodatonWindow gradeAccommodatonWindow = new GradeAccommodatonWindow(reservationVM, Guest1VM.Username);
            //gradeAccommodatonWindow.ShowDialog();
        }

        public void RenovationRecommendationClick(object sender, RoutedEventArgs e)
        {
            RRItem.Visibility = Visibility.Visible;
        }
        public void CancelRecommendationClick(object sender, RoutedEventArgs e)
        {
            RRItem.Visibility = Visibility.Collapsed;
        }

        public void CancelGradeClick(object sender, RoutedEventArgs e)
        {
            GradeItem.Visibility = Visibility.Collapsed;
        }
        public void GradeClick(object sender, RoutedEventArgs e)
        {
            GradeItem.Visibility = Visibility.Visible;
        }

        public void SubmitRecommendationClick(object sender, RoutedEventArgs e)
        {
            GradeItem.Visibility = Visibility.Visible;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
