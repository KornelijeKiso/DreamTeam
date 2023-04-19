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
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class LiveToursTrackingWindow : UserControl, INotifyPropertyChanged
    {
        public GuideVM Guide { get; set; }
        public TourAppointmentVM SelectedTourAppointment { get; set; }
        public LiveToursTrackingWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Guide = new GuideVM(username);
        }
        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourAppointment != null)
            {
                TourStopsWindow tourStopsWindow = new TourStopsWindow(SelectedTourAppointment);
                HideTodaysToursContent();
                ContentArea.Content = new TourStopsWindow(SelectedTourAppointment);
                SelectedTourAppointment = tourStopsWindow.TourAppointment;
            }
            else
                MessageBox.Show("You must choose an appointment which you would like to start.");
        }
        private void ReviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourAppointment != null)
            {
                if (SelectedTourAppointment.State == TOURSTATE.FINISHED || SelectedTourAppointment.State == TOURSTATE.STOPPED)
                    ShowReviewWindow();
                else
                    MessageBox.Show("The selected appointment was not finished yet!");
            }
                
            else
                MessageBox.Show("You must choose an appointment which reviews you would like to see.");
        }

        private void ShowReviewWindow()
        {
            if (SelectedTourAppointment.TicketGrades.Count > 0)
            {
                HideTodaysToursContent();
                ContentArea.Content = new ReviewsWindow(SelectedTourAppointment);
            }
            else
                MessageBox.Show("There are no reviews for this appointment!");
        }

        private void HideTodaysToursContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { StartTourButton, TodaysToursLabel, DataGridTourAppointments, ReviewButton };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        public void Update() { }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
