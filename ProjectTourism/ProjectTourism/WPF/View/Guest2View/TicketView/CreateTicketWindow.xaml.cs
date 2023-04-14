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
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.Observer;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for CreateTicketWindow.xaml
    /// </summary>
    public partial class CreateTicketWindow : Window, INotifyPropertyChanged, IObserver
    {
        public TicketVM Ticket { get; set; }
        public TicketService TicketService { get; set; }
        public TourAppointmentService TourAppointmentService { get; set; }
        public TourAppointment selectedAppointment { get; set; }
        //public TourAppointmentVM selectedAppointment { get; set; }
        public Tour SelectedTour { get; set; }
        //public TourVM SelectedTour { get; set; }
        public TourService TourService { get; set; }
        public List<string> StopsList { get; set; }
        public List<DateTime> dates { get; set; }
        
        public CreateTicketWindow(string username, int tourId)
        {
            InitializeComponent();
            DataContext = this;
            TicketService = new TicketService(new TicketRepository());
            TourService = new TourService(new TourRepository());
            TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            SelectedTour = TourService.GetOne(tourId);
            
            //Ticket = new TicketVM();
            //Ticket.Guest2Username = username;

            StopsList = SelectedTour.StopsList;
            StopsList.RemoveAt(StopsList.Count() - 1);  // Guest can't chose Finish stop to join the Tour

            dates = FindDates(SelectedTour.dates);
            TicketService.Subscribe(this);
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

        // TO DO                TourAppointmentService.UpdateAppointmentCreate
        //                      TicketService.Add
        private void CreateTicket(object sender, RoutedEventArgs e)
        {
            //Ticket ticket = new Ticket(selectedAppointment.Id, Ticket.TourStop, Ticket.Guest2Username, Ticket.NumberOfGuests);
            //TicketService.Add(ticket);
            //TourAppointmentService.UpdateAppointmentCreate(selectedAppointment.Id, Ticket);
            //Close();
        }

        private void StopsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket.TourStop = StopsList[StopsComboBox.SelectedIndex];
        }

        private List<DateTime> RemoveOldDates(List<DateTime> AllDates)
        {
            List<DateTime> NoOldDates = new List<DateTime>();
            foreach (DateTime date in AllDates)
            {
                if (date >= DateTime.Today)
                {
                    NoOldDates.Add(date);
                }
            }
            return NoOldDates;
        }

        // TO DO                TourAppointmentService.GetByDate
        private List<DateTime> RemoveDatesWithNoAvailableSeatsOrInvalidState(List<DateTime> AllDates)
        {
            List<DateTime> NoFreeSeats = new List<DateTime>();
            TourAppointment tourAppointment;
            //foreach (var date in AllDates)
            //{
            //    tourAppointment = TourAppointmentService.GetByDate(SelectedTour.Id, date);
            //    if ((tourAppointment.AvailableSeats != 0) &&  (tourAppointment.State == TOURSTATE.READY))
            //        NoFreeSeats.Add(date);
            //}
            return NoFreeSeats;
        }

        // TO DO        TicketService.GetByGuest
        private List<DateTime> RemoveDatesHasTickets(List<DateTime> AllDates)
        {
            List<DateTime> TicketNotBought = new List<DateTime>();
            foreach (var date in AllDates)
                TicketNotBought.Add(date);

            ////getting all guest's tickets
            //List<Ticket> allGuestsTickets = TicketService.GetByGuest(Ticket.Guest2Username);
            //List<Ticket> validTicket = new List<Ticket>();
            //foreach (var ticket in allGuestsTickets)
            //{
            //    if (ticket.TourAppointment.TourId == SelectedTour.Id)
            //        validTicket.Add(ticket);
            //}

            //// if ticket for that appointment exist already, can't buy it again
            //foreach (var ticket in validTicket) 
            //{
            //    foreach (var date in AllDates)
            //    {
            //        if (ticket.TourAppointment.TourDateTime.Equals(date))
            //            TicketNotBought.Remove(date);
            //    }
            //}
            return TicketNotBought;
        }
        
        
        private List<DateTime> FindDates(List<DateTime> dates)
        {
            List<DateTime> NoOldDates = RemoveOldDates(dates);
            List<DateTime> noSeats = RemoveDatesWithNoAvailableSeatsOrInvalidState(NoOldDates);
            List<DateTime> available = RemoveDatesHasTickets(noSeats);
            return available;
        }

        // TO DO            TourAppointmentService.GetByDate
        private void DatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = dates[DatesComboBox.SelectedIndex];
            //selectedAppointment = TourAppointmentService.GetByDate(SelectedTour.Id, date);
            slider.Maximum = selectedAppointment.AvailableSeats;
        }
    }
}
