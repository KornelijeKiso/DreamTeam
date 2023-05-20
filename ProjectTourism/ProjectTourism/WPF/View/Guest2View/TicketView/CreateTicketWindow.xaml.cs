using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.Model;
using System.Globalization;
using ProjectTourism.DTO;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for CreateTicketWindow.xaml
    /// </summary>
    public partial class CreateTicketWindow : Window, INotifyPropertyChanged
    {
        public Guest2DTO Guest2 { get; set; }
        public TourDTO SelectedTour { get; set; }
        public TourAppointmentDTO selectedAppointment { get; set; }
        public TicketDTO Ticket { get; set; }
        public List<string> StopsList { get; set; }
        public List<DateTime> dates { get; set; }
        
        public CreateTicketWindow(Guest2DTO guest2, TourDTO tour)
        {
            InitializeComponent();
            DataContext = this;

            Guest2 = guest2;
            SelectedTour = tour;
            if (DateTime.TryParse(DatesComboBox.SelectedIndex.ToString("dd.MM.yyyy HH:mm"), 
                new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
            selectedAppointment = new TourAppointmentDTO(SelectedTour.GetTour(), dateTimeParsed);
            Ticket = new TicketDTO(new Ticket());

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
                selectedAppointment.UpdateTourAppointmentDTO(selectedAppointment);
                Close();
            }
            else
            {
                MessageBox.Show("You can't buy tickets or use vouchers for this tour! \nIt is already full!");
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

        private List<DateTime> FindAllDates(TourDTO tour)
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
                TourAppointmentDTO tourAppointmentDTO = new TourAppointmentDTO(SelectedTour.GetTour(), date);
                if ((tourAppointmentDTO.AvailableSeats != 0) && (tourAppointmentDTO.State == TOURSTATE.READY))
                    NoFreeSeats.Add(date);
            }
            return NoFreeSeats;
        }
        
        private void DatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = dates[DatesComboBox.SelectedIndex];
            selectedAppointment = new TourAppointmentDTO(SelectedTour.GetTour(), date);
            slider.Maximum = selectedAppointment.AvailableSeats;
        }
        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {
            if (dates.Count > 0)
            {
                Ticket.CreateTicket(new Ticket(selectedAppointment.Id, StopsComboBox.Text, Guest2.Username, int.Parse(sliderText.Text)));
                Ticket = Ticket.GetLast();
                UnusedVouchersWindow unusedVouchersWindow = new UnusedVouchersWindow(Guest2, Ticket);
                unusedVouchersWindow.ShowDialog();
                if (unusedVouchersWindow.IsUsed)
                {
                    selectedAppointment.AvailableSeats -= int.Parse(sliderText.Text);
                    selectedAppointment.UpdateTourAppointmentDTO(selectedAppointment);
                }
                else
                {   // delete created Ticket if UseVoucher is canceled
                    Ticket.RemoveLast();    
                }
                Close();
            }
            else
            {
                MessageBox.Show("You can't buy tickets or use vouchers for this tour! \nIt is already full!");
                Close();
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
