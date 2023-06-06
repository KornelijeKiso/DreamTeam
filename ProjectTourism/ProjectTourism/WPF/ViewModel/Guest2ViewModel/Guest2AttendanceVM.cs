using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;
using ProjectTourism.Services;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class Guest2AttendanceVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public TicketDTO Ticket { get; set; }
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public bool IsStarted
        {
            get => (Ticket.TourAppointment.State == TOURSTATE.STARTED);
        }
        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }

        public Guest2AttendanceVM() { }
        public Guest2AttendanceVM(Guest2DTO guest2, TicketDTO ticket)
        {
            Guest2 = guest2;
            Ticket = ticket;
            StartTimer(IsStarted);

            if (Ticket.TourAppointment.State == TOURSTATE.FINISHED)
                MessageBox.Show("Tour is already finished ! ");

            // TicketCommand 
            ContentCommand = new RelayCommand(ReturnToTickets);
        }

        private void StartTimer(bool IsStarted)
        {
            if (IsStarted)
            {
                // Live tracking guide's changes in TourAppointment
                // checks for changes every 20 seconds
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 20);
                dispatcherTimer.Start();
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Ticket = new TicketDTO(Ticket.UpdateTicketTourAppointmentData(Ticket.GetTicket()));
            // triggering PropertyChanged for CurrentTourStopLabel
            OnPropertyChanged(nameof(Ticket));

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        
        //JoinCommand
        private ICommand _JoinCommand;
        public ICommand JoinCommand
        {
            get
            {
                return _JoinCommand ?? (_JoinCommand = new CommandHandler(() => JoinStartedTourApp(), () => true));
            }
        }
        public void JoinStartedTourApp()
        {
            if (CanJoin())
            {
                Ticket.ConfirmAttendance(Ticket.GetTicket());
                MessageBox.Show("Successfully joined Tour ! ");
                Content = new TicketsVM(Guest2);
            }
            else if (Ticket.HasGuestConfirmed)
            {
                MessageBox.Show("You have already joined the Tour ! ");
            }
            else
            {
                MessageBox.Show("Can't join the Tour yet ! ");
            }
        }
        
        private bool CanJoin()
        {
            return (Ticket.TourAppointment.State == TOURSTATE.STARTED && Ticket.HasGuideChecked
                && !Ticket.HasGuestConfirmed
                && (Ticket.TourAppointment.Tour.StopsList.IndexOf(Ticket.TourAppointment.CurrentTourStop)
                  >= Ticket.TourAppointment.Tour.StopsList.IndexOf(Ticket.TourStop)));
        }
        public ICommand ContentCommand { get; set; }
        public void ReturnToTickets(object obj)
        {
            Content = new TicketsVM(Guest2);
        }
    }
}
