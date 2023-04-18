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
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Observer;
using System.Globalization;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for CreateTicketWindow.xaml
    /// </summary>
    public partial class CreateTicketWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Guest2VM Guest2 { get; set; }
        public TourVM SelectedTour { get; set; }
        public TourAppointmentVM selectedAppointment { get; set; }
        public TicketVM Ticket { get; set; }
        public List<string> StopsList { get; set; }
        public List<DateTime> dates { get; set; }
        
        public CreateTicketWindow(Guest2VM guest2, TourVM tour)
        {
            InitializeComponent();
            DataContext = this;

            Guest2 = guest2;
            SelectedTour = tour;
            if (DateTime.TryParse(DatesComboBox.SelectedIndex.ToString("dd.MM.yyyy HH:mm"), 
                new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
            selectedAppointment = new TourAppointmentVM(SelectedTour.GetTour(), dateTimeParsed);
            Ticket = new TicketVM(new Ticket());

            dates = FindDates();

            StopsList = SelectedTour.StopsList;
            StopsList.RemoveAt(StopsList.Count() - 1);  // Guest can't chose Finish stop to join the Tour
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
            if (dates.Count > 0)
            {
                Ticket.CreateTicket(new Ticket(selectedAppointment.Id, StopsComboBox.Text, Guest2.Username, int.Parse(sliderText.Text)));
                selectedAppointment.AvailableSeats -= int.Parse(sliderText.Text);
                selectedAppointment.UpdateTourAppointmentVM(selectedAppointment);
                Close();
            }
            else
            {
                MessageBox.Show("You can't buy tickets for this tour!");
                Close();
            }
        }

        private void StopsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket.TourStop = StopsList[StopsComboBox.SelectedIndex];
        }
        
        private List<DateTime> FindDates()
        {
            List<DateTime> allDates = FindAllDates(SelectedTour);
            List<DateTime> NoOldDates = RemoveOldDates(allDates);
            List<DateTime> hasTickets = RemoveDatesHasTickets(NoOldDates);
            List<DateTime> available = RemoveDatesWithNoAvailableSeatsOrInvalidState(hasTickets);

            return available;
        }

        private List<DateTime> FindAllDates(TourVM tour)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach(var tourApp in tour.TourAppointments)
            {
                dates.Add(tourApp.TourDateTime);
            }
            return dates;
        }
        private List<DateTime> RemoveDatesHasTickets(List<DateTime> AllDates)
        {
            List<DateTime> TicketNotBought = new List<DateTime>();
            foreach (var date in AllDates)
                TicketNotBought.Add(date);

            foreach (var ticket in Guest2.Tickets)
            {
                foreach (var date in AllDates)
                {
                    if (ticket.TourAppointment.TourDateTime.Equals(date))
                        TicketNotBought.Remove(date);
                }
            }
            return TicketNotBought;
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
        private List<DateTime> RemoveDatesWithNoAvailableSeatsOrInvalidState(List<DateTime> AllDates)
        {
            List<DateTime> NoFreeSeats = new List<DateTime>();

            foreach (DateTime date in AllDates)
            {
                TourAppointmentVM tourAppointmentVM = new TourAppointmentVM(SelectedTour.GetTour(), date);
                if ((tourAppointmentVM.AvailableSeats != 0) && (tourAppointmentVM.State == TOURSTATE.READY))
                    NoFreeSeats.Add(date);
            }
            return NoFreeSeats;
        }
        
        private void DatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = dates[DatesComboBox.SelectedIndex];
            selectedAppointment = new TourAppointmentVM(SelectedTour.GetTour(), date);
            slider.Maximum = selectedAppointment.AvailableSeats;
        }
        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {
            // TO DO
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
