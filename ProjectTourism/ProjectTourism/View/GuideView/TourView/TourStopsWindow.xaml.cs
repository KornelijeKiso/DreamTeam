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

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TourStopsWindow.xaml
    /// </summary>
    public partial class TourStopsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public TourAppointmentController TourAppointmentController { get; set; }
        public TourAppointment TourAppointment { get; set; }
        public TicketController TicketController { get; set; }
        public GuideController GuideController { get; set; }
        public ObservableCollection<Ticket> Tickets { get; set; }
        public Ticket SelectedTicket { get; set; }

        public TourStopsWindow(int id)
        {
            InitializeComponent();
            DataContext = this;
            TourAppointmentController = new TourAppointmentController();
            TourAppointment = TourAppointmentController.GetOne(id);
            TicketController = new TicketController();
            GuideController = new GuideController();
            List<Ticket> tickets = TicketController.GetByAppointment(TourAppointment);
            Tickets = new ObservableCollection<Ticket>(tickets);

            TicketStatusButtonColor();

            if (TourStarted())
                StopPassedButton.Content = "Stop passed";
        }

        private bool TourStarted()
        {
            return TourAppointment.State == TOURSTATE.STARTED;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int PassedButtonClicks(TourAppointment tour)
        {
            int number = 0;
            foreach(var stop in tour.Tour.StopsList)
            {
                if (stop.Equals(tour.CurrentTourStop))
                    break;
                number++;
            }
            return number;
        }

        public bool IsLastStop(TourAppointment tour)
        {
            return tour.Tour.StopsList.Last().Equals(tour.CurrentTourStop);
        }

        public void FinishTour(TourAppointment tour)
        {
            StopPassedButton.Content = "Finish tour";
            tour.IsNotFinished = false;
            tour.State = TOURSTATE.FINISHED;
            GuideController.Update(tour.Tour.Guide.Username, false);
            TourAppointmentController.ChangeState(tour);
            StopPassedButton.IsEnabled = false;
            EmergencyStopButton.IsEnabled = false;
        }

        public void NextStop(TourAppointment tour)
        {
            StopTextBox.Text = TourAppointmentController.GetNextStop(tour.Tour, PassedButtonClicks(tour));
            StopPassedButton.Content = "Stop passed";
            tour.CurrentTourStop = tour.Tour.StopsList[PassedButtonClicks(tour) + 1];
            TourAppointmentController.ChangeCurrentStop(tour);
            tour.State = TOURSTATE.STARTED;
            TourAppointmentController.ChangeState(tour);
            GuideController.Update(tour.Tour.Guide.Username, true);

            TicketStatusButtonColor();
        }
        private void StopPassedButton_Click(object sender, RoutedEventArgs e)
        {
            if(!TourAppointment.Tour.Guide.HasTourStarted || TourAppointment.State == TOURSTATE.STARTED)
            {
                NextStop(TourAppointment);
                if (IsLastStop(TourAppointment))
                    FinishTour(TourAppointment);
            }
            else
            {
                MessageBox.Show("Guide has already started a tour!");
                Close();
            }
        }
        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {
            TourAppointment.State = TOURSTATE.STOPPED;
            TourAppointmentController.ChangeState(TourAppointment);
            TourAppointment.IsNotFinished = false;
        }
        
        private void TicketStatusButtonColor()
        {
            foreach (Ticket ticket in Tickets)
            {
                TicketController.CheckGuestStatus(TourAppointment.Id, ticket.Id);
                if (ticket.HasGuideChecked && !ticket.HasGuestConfirmed)
                    ticket.GuestStatusColor = Brushes.IndianRed;
                else if (ticket.HasGuideChecked && ticket.HasGuestConfirmed)
                    ticket.GuestStatusColor = Brushes.Green;
                else ticket.GuestStatusColor = Brushes.Transparent;
            }
        }
        private void TicketStatusButton_Click(object sender, RoutedEventArgs e)
        {   

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void Update()
        {
            Tickets = new ObservableCollection<Ticket>(TourAppointment.Tickets);
        }
    }
}
