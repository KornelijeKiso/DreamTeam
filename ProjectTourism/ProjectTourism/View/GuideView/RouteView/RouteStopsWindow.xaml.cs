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

namespace ProjectTourism.View.GuideView.RouteView
{
    /// <summary>
    /// Interaction logic for RouteStopsWindow.xaml
    /// </summary>
    public partial class RouteStopsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public TourAppointmentController TourAppointmentController { get; set; }
        public TourAppointment TourAppointment { get; set; }
        public TicketController TicketController { get; set; }

        public ObservableCollection<Ticket> Tickets { get; set; }
        public Ticket SelectedTicket { get; set; }
        //public SolidColorBrush GuestStatusColor { get; set; }

        public RouteStopsWindow(int id)
        {
            InitializeComponent();
            DataContext = this;
            TourAppointmentController = new TourAppointmentController();
            TourAppointment = TourAppointmentController.GetOne(id);
            TicketController = new TicketController();
            List<Ticket> tickets = TicketController.GetByAppointment(TourAppointment);
            Tickets = new ObservableCollection<Ticket>(tickets);
            
            TicketStatusButtonColor();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int PassedButtonClicks(TourAppointment tour)
        {
            int number = 0;
            foreach(var stop in tour.Route.StopsList)
            {
                if (stop.Equals(tour.CurrentTourStop))
                    break;
                number++;
            }
            return number;
        }

        public bool IsLastStop(TourAppointment tour)
        {
            return tour.Route.StopsList.Last().Equals(tour.CurrentTourStop);
        }

        public void FinishRoute(TourAppointment tour)
        {
            StopPassedButton.Content = "Finish route";
            tour.IsNotFinished = false;
            tour.State = TOURSTATE.FINISHED;
            TourAppointmentController.ChangeState(tour);
            StopPassedButton.IsEnabled = false;
            EmergencyStopButton.IsEnabled = false;
        }

        public void NextStop(TourAppointment tour)
        {
            StopTextBox.Text = TourAppointmentController.GetNextStop(tour.Route, PassedButtonClicks(tour));
            StopPassedButton.Content = "Stop passed";
            tour.CurrentTourStop = tour.Route.StopsList[PassedButtonClicks(tour) + 1];
            TourAppointmentController.ChangeCurrentStop(tour);
            tour.State = TOURSTATE.STARTED;
            TourAppointmentController.ChangeState(tour);
            TicketStatusButtonColor();
        }
        private void StopPassedButton_Click(object sender, RoutedEventArgs e)
        {
            NextStop(TourAppointment);
            if (IsLastStop(TourAppointment))
            {
                FinishRoute(TourAppointment);
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
        {/*
            if(TourAppointment.ButtonColor!=Brushes.Green)
            {
                TourAppointment.ButtonColor = Brushes.IndianRed;
                TicketController.GuideCheck(TourAppointment);
            }*/
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void Update()
        {
            Tickets = new ObservableCollection<Ticket>(TourAppointment.Tickets);
            //tickets = new ObservableCollection<Ticket>((IEnumerable<Ticket>)TicketController.GetByRoute(TourAppointment));
        }

    }
}
