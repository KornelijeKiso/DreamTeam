using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TourStopsWindow.xaml
    /// </summary>
    public partial class TourStopsWindow : UserControl, INotifyPropertyChanged, IObserver
    {
        public TourAppointmentVM TourAppointment { get; set; }
        public TicketVM SelectedTicket { get; set; }
        public GuideVM Guide { get; set; }

        public TourStopsWindow(TourAppointmentVM SelectedTourAppointment)
        {
            InitializeComponent();
            DataContext = this;

            Guide = new GuideVM(SelectedTourAppointment.Tour.Guide.GetGuide());
            TourAppointment = SelectedTourAppointment;
            //   ControlTicketStatusColor();
            EmergencyButtonSet();
        }
        private void EmergencyButtonSet()
        {
            if (HasTourStarted())
            {
                StopPassedButton.Content = "Stop passed";
                EmergencyStopButton.IsEnabled = true;
            }
            else
                EmergencyStopButton.IsEnabled = false;
        }

        private bool HasTourStarted()
        {
            return TourAppointment.State == TOURSTATE.STARTED;
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
            int nextStopIndex = PassedButtonClicks() + 1;
            StopPassedButton.Content = "Stop passed";
            StopTextBox.Text = TourAppointment.Tour.Guide.NextStop(TourAppointment.GetTourAppointment());
            // ControlTicketStatusColor();
        }
        private bool CanGuidePassStop()
        {
            if (TourAppointment.State == TOURSTATE.READY)
            {
                foreach (var tourApp in Guide.TourAppointments.Where(t => t.Id != TourAppointment.Id).ToList())
                {
                    if (tourApp.State == TOURSTATE.STARTED)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void StopPassedButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanGuidePassStop())
            {
                if (IsNextStopFinish())
                    TourAppointment.Tour.Guide.FinishTourAndReturnStop(TourAppointment);
                else if (CanGoNextStop())
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
        private bool CanGoNextStop()
        {
            return (!TourAppointment.Tour.Guide.HasTourStarted || TourAppointment.State == TOURSTATE.STARTED) && !IsNextStopFinish();
        }

        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {
            TourAppointment.Tour.Guide.EmergencyStop(TourAppointment);
        }

        //private void ControlTicketStatusColor()
        //{
        //    foreach (Ticket ticket in Tickets)
        //    {
        //        TicketController.CheckGuestStatus(TourAppointment.Id, ticket.Id);
        //        if (ticket.HasGuideChecked && !ticket.HasGuestConfirmed)
        //            ticket.ButtonColor = Brushes.IndianRed;
        //        else if (ticket.HasGuideChecked && ticket.HasGuestConfirmed)
        //            ticket.ButtonColor = Brushes.Green;
        //        else ticket.ButtonColor = Brushes.Transparent;
        //    }
        //}
        private void TicketStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket.ButtonColor != Brushes.Green)
            {
                SelectedTicket.ButtonColor = Brushes.IndianRed;
                TourAppointment.Tour.Guide.CheckTicket(SelectedTicket);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void Update() { }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            HideTourStopsContent();
            ContentArea.Content = new ReviewsWindow();
        }

        private void HideTourStopsContent()
        {
            StopPassedButton.Visibility = Visibility.Hidden;
            EmergencyStopButton.Visibility = Visibility.Hidden;
            CurrentTourStopLabel.Visibility = Visibility.Hidden;
            StopTextBox.Visibility = Visibility.Hidden;
            TourStatusTextBox.Visibility = Visibility.Hidden;
            Grid1.Visibility = Visibility.Hidden;
            TourStateLabel.Visibility = Visibility.Hidden;
            ReviewsButton.Visibility = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
