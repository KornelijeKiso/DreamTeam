using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for Guest2AttendanceWindow.xaml
    /// </summary>
    public partial class Guest2AttendanceWindow : Window, INotifyPropertyChanged
    {
        public Guest2VM Guest2 { get; set; }
        public TicketVM Ticket { get; set; }
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        
        public Guest2AttendanceWindow(TicketVM ticket, Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = this;
            Guest2 = guest2;
            Ticket = ticket;

            // Live tracking guide's changes in TourAppointment
            // checks for changes every 20 seconds
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 20);
            dispatcherTimer.Start();

            // Can't join tour if it isn't STARTED
            if (Ticket.TourAppointment.State == TOURSTATE.READY)
            {
                JoinButton.IsEnabled = false;
                JoinButton.Focusable = false;
                JoinButton.Visibility = Visibility.Collapsed;   // invisible
            }
            if (Ticket.TourAppointment.State == TOURSTATE.FINISHED)
                TourAppointmentFinished();
        }
        
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Ticket = new TicketVM(Ticket.UpdateTicketTourAppointmentData(Ticket.GetTicket()));
            // triggering PropertyChanged for CurrentTourStopLabel
            OnPropertyChanged(nameof(Ticket)); 

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void JoinTourAppointmentClick(object sender, RoutedEventArgs e)
        {
            if (CanJoin())
            {
                Ticket.ConfirmAttendance(Ticket.GetTicket());
                MessageBox.Show("Successfully joined Tour ! ");
                Close();
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

        private void TourAppointmentFinished()
        {   // TO DO -> move finished tour, update ObservableCollections in TicketsVM
            JoinButton.Visibility = Visibility.Collapsed;   // invisible
            MessageBox.Show("Tour is already finished ! ");
            
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
