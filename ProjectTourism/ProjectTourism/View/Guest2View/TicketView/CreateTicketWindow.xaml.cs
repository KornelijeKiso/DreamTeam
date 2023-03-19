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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.View.Guest2View
{
    /// <summary>
    /// Interaction logic for CreateTicketWindow.xaml
    /// </summary>
    public partial class CreateTicketWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Ticket Ticket { get; set; }
        public TicketController TicketController { get; set; }
        public TourAppointmentController TourAppointmentController { get; set; }
        public TourAppointment selectedAppointment { get; set; }
        public Tour SelectedTour { get; set; }
        public TourController TourController { get; set; }
        public Guest2 Guest2 { get; set; }
        public Guest2Controller Guest2Controller { get; set; }
        public List<string> StopsList { get; set; }
        public List<DateTime> dates { get; set; }
        
        public CreateTicketWindow(string username, int tourId)
        {
            InitializeComponent();
            DataContext = this;
            TicketController = new TicketController();
            TourController = new TourController();
            TourAppointmentController = new TourAppointmentController();
            Guest2Controller = new Guest2Controller();
            SelectedTour = TourController.GetOne(tourId);
            Guest2 = Guest2Controller.GetOne(username);
            Ticket = new Ticket();

            // transfer to Tour -> StopsList
            StopsList = new List<string>();                     
            foreach (string stop in SelectedTour.StopsList)
            {
                StopsList.Add(stop.Trim());
            }
            StopsList.RemoveAt(StopsList.Count() - 1);  // Guest can't chose Finish stop to join the Tour

            // so we can't see dates with no available tickets or one where Guest2 already has ticket
            dates = FindDates(SelectedTour.dates);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void CreateTicket(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket(selectedAppointment.Id, Ticket.TourStop, Guest2.Username, Ticket.NumberOfGuests);
            TicketController.Add(ticket);
            TourAppointmentController.UpdateAppointmentCreate(selectedAppointment.Id, Ticket);
            Close();
        }

        private void StopsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket.TourStop = StopsList[StopsComboBox.SelectedIndex];
        }

        private List<DateTime> RemoveDatesWithNoAvailableSeats(List<DateTime> AllDates)
        {
            List<DateTime> NoFreeSeats = new List<DateTime>();
            TourAppointment tourAppointment;
            foreach (var date in AllDates)
            {
                tourAppointment = TourAppointmentController.GetByDate(SelectedTour.Id, date);
                if (tourAppointment.AvailableSeats != 0)
                    NoFreeSeats.Add(date);
            }
            return NoFreeSeats;
        }

        private List<DateTime> RemoveDatesHasTicketsOrInvalidAppointmetn(List<DateTime> AllDates)
        {
            List<DateTime> TicketNotBought = new List<DateTime>();
            foreach (var date in AllDates)
                TicketNotBought.Add(date);

            // checking if the state of appointment is valid and if the ticket is from that tour
            List<Ticket> allGuestsTickets = TicketController.GetByGuest(Guest2);
            List<Ticket> validTicket = new List<Ticket>();
            foreach (var ticket in allGuestsTickets)
            {
                if (ticket.TourAppointment.State == TOURSTATE.READY && ticket.TourAppointment.TourId == SelectedTour.Id)
                    validTicket.Add(ticket);
            }

            // if some ticket already has same date, remove that date
            foreach (var ticket in validTicket) 
            {
                foreach (var date in AllDates)
                {
                    if (ticket.TourAppointment.TourDateTime.Equals(date) )
                        TicketNotBought.Remove(date);
                }
            }
            return TicketNotBought;
        }
        private List<DateTime> FindDates(List<DateTime> dates)
        {
            List<DateTime> noSeats = RemoveDatesWithNoAvailableSeats(dates);
            List<DateTime> available = RemoveDatesHasTicketsOrInvalidAppointmetn(noSeats);
            return available;
        }

        private void DatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = dates[DatesComboBox.SelectedIndex];
            selectedAppointment = TourAppointmentController.GetByDate(SelectedTour.Id, date);
            slider.Maximum = selectedAppointment.AvailableSeats;
        }
    }
}
