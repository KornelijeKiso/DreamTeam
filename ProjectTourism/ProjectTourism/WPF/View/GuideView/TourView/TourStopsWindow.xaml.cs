using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// <summary>
    /// Interaction logic for TourStopsWindow.xaml
    /// </summary>
    public partial class TourStopsWindow : UserControl, INotifyPropertyChanged
    {
        public TourAppointmentVM TourAppointment { get; set; }
        public TicketVM SelectedTicket { get; set; }
        public GuideVM Guide { get; set; }
        public ObservableCollection<TicketVM> Tickets { get; set; }
        public TourStopsWindow(TourAppointmentVM SelectedTourAppointment)
        {
            InitializeComponent();
            DataContext = this;

            Guide = new GuideVM(SelectedTourAppointment.Tour.Guide.GetGuide());
            TourAppointment = SelectedTourAppointment;
            Tickets = new ObservableCollection<TicketVM>(TourAppointment.Tickets);
            EmergencyButtonSet();
            if (SelectedTourAppointment.CurrentTourStop.Equals(SelectedTourAppointment.Tour.Finish))
            {
                EmergencyStopButton.IsEnabled = false;
                StopPassedButton.IsEnabled = false;
                TourAppointment.Tour.Guide.EndTour(SelectedTourAppointment);
            }
        }
        private void EmergencyButtonSet()
        {
            if (TourAppointment.State == TOURSTATE.STARTED)
            {
                StopPassedButton.Content = "Stop passed";
                EmergencyStopButton.IsEnabled = true;
            }
            else
                EmergencyStopButton.IsEnabled = false;
        }
        public int PassedButtonClicks()
        {
            int number = 0;
            foreach (var stop in TourAppointment.Tour.StopsList)
            {
                if (stop.Equals(TourAppointment.CurrentTourStop))
                    break;
                number++;
            }
            return number;
        }
        public void NextStop()
        {
            StopPassedButton.Content = "Stop passed";
            StopTextBox.Text = TourAppointment.Tour.Guide.NextStop(TourAppointment.GetTourAppointment()).Trim();
            TourStatusTextBox.Text = TOURSTATE.STARTED.ToString();
        }
        private bool CanGuidePassStop()
        {
            if ((TourAppointment.State == TOURSTATE.READY && !Guide.HasTourStarted) || 
                    (TourAppointment.State == TOURSTATE.STARTED && Guide.HasTourStarted))
                return true;
            return false;
        }
        private void CheckpointPassedButton_Click(object sender, RoutedEventArgs e)
        {
            Guide = new GuideVM(TourAppointment.Tour.Guide.GetGuide());
            if (CanGuidePassStop())
            {
                if (IsNextStopFinish())
                {
                    TourAppointment.Tour.Guide.FinishTourAndReturnStop(TourAppointment);
                    StopPassedButton.IsEnabled = false;
                }
                else 
                    NextStop();
            }
            else
                MessageBox.Show("Guide has already started a tour!");
            EmergencyButtonSet();
        }
        private bool IsNextStopFinish()
        {
            //if we are on the one before last or if the stops are null
            return PassedButtonClicks() == TourAppointment.Tour.StopsList.Count() - 2 || TourAppointment.Tour.Stops.Equals(""); 
        }
        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {
            TourAppointment.Tour.Guide.EmergencyStop(TourAppointment);
            ReviewsButton.Visibility = Visibility.Hidden;
            TourStateLabel.Visibility = Visibility.Hidden;
            grid.Visibility = Visibility.Hidden;
            StopPassedButton.Visibility = Visibility.Hidden;
            EmergencyStopButton.Visibility = Visibility.Hidden;
            ContentArea.Content = new LiveToursTrackingWindow(Guide.Username);
        }
        private void TicketStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket.ButtonColor != Brushes.Green)
            {
                SelectedTicket.ButtonColor = Brushes.IndianRed;
                TourAppointment.Tour.Guide.CheckTicket(SelectedTicket);
            }
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (TourAppointment.Tickets.Count != 0 && TourAppointment.TicketGrades.Count != 0)
            {
                HideTourStopsContent();
                ContentArea.Content = new ReviewsWindow(TourAppointment);
            }
            else
                MessageBox.Show("There are no reviews for this appointment!");
        }
        private void HideTourStopsContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { StopPassedButton, EmergencyStopButton, CurrentTourStopLabel,
                                                                   TourStatusTextBox, Grid1, TourStateLabel, ReviewsButton, StopTextBox };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }

        public void Update() { }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
