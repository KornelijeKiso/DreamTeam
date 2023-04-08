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
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TourStopsWindow.xaml
    /// </summary>
    public partial class TourStopsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public TourAppointmentService TourAppointmentService { get; set; }
        public TourAppointmentVM TourAppointment { get; set; }
        public TicketService TicketService { get; set; }
        public GuideService GuideService { get; set; }
        public ObservableCollection<TicketVM> Tickets { get; set; }
        public TicketVM SelectedTicket { get; set; }

        public TourStopsWindow(int id)
        {
            InitializeComponent();
            DataContext = this;
            SetServices();

            TourAppointment = TourAppointmentService.GetOne(id);

            List<TicketVM> tickets = TicketService.GetByAppointment(id);
            Tickets = new ObservableCollection<TicketVM>(tickets);

         //   ControlTicketStatusColor();
            EmergencyButtonSet();
        }

        private void SetServices()
        {
            TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            TicketService = new TicketService(new TicketRepository());
            GuideService = new GuideService(new GuideRepository());
        }

        private void EmergencyButtonSet()
        {
            if (TourStarted())
            {
                StopPassedButton.Content = "Stop passed";
                EmergencyStopButton.IsEnabled = true;
            }
            else
                EmergencyStopButton.IsEnabled = false;
        }

        private bool TourStarted()
        {
            return TourAppointment.State == TOURSTATE.STARTED;
        }
        public int PassedButtonClicks(TourAppointmentVM tour)
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

        public bool IsLastStop(TourAppointmentVM tour)
        {
            return tour.Tour.StopsList.Last().Equals(tour.CurrentTourStop);
        }

        public void FinishTour(TourAppointmentVM tour)
        {
            StopPassedButton.Content = "Finish tour";
            UpdateFinishTour(tour);
            StopPassedButton.IsEnabled = false;
            EmergencyStopButton.IsEnabled = false;
        }

        private void UpdateFinishTour(TourAppointmentVM tourApp)
        {
            tourApp.IsNotFinished = false;
            tourApp.State = TOURSTATE.FINISHED;
            GuideService.UpdateHasTourStarted(tourApp.Tour.Guide.Username, false);
            TourAppointmentService.ChangeState(tourApp);
        }

        public void NextStop(TourAppointmentVM tour)
        {
            StopPassedButton.Content = "Stop passed";
            UpdateNextStop(tour);
           // ControlTicketStatusColor();
        }
        private void UpdateNextStop(TourAppointmentVM tourAppVM)
        {
            StopTextBox.Text = TourAppointmentService.GetNextStop(new TourVM(tourAppVM.Tour), PassedButtonClicks(tourAppVM));
            tourAppVM.CurrentTourStop = tourAppVM.Tour.StopsList[PassedButtonClicks(tourAppVM) + 1];
            TourAppointmentService.ChangeCurrentStop(tourAppVM);
            tourAppVM.State = TOURSTATE.STARTED;
            TourAppointmentService.ChangeState(tourAppVM);
            GuideService.UpdateHasTourStarted(tourAppVM.Tour.Guide.Username, true);
        }

        private void StopPassedButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanGoNextStop())
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
            EmergencyButtonSet();
        }

        private bool CanGoNextStop()
        {
            return !TourAppointment.Tour.Guide.HasTourStarted || TourAppointment.State == TOURSTATE.STARTED;
        }

        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {
            TourAppointment.State = TOURSTATE.STOPPED;
            TourAppointmentService.ChangeState(TourAppointment);
            TourAppointment.IsNotFinished = false;
            GuideService.UpdateHasTourStarted(TourAppointment.Tour.Guide.Username, false);
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
                TicketService.GuideCheck(SelectedTicket);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void Update()
        {
            Tickets = new ObservableCollection<TicketVM>((IEnumerable<TicketVM>)TourAppointment.Tickets);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
