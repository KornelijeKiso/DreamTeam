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

namespace ProjectTourism.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for UpdateTicketWindow.xaml
    /// </summary>
    public partial class UpdateTicketWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Ticket Ticket { get; set; }
        public TicketController TicketController { get; set; }
        public TourAppointmentController TourAppointmentController { get; set; }
        public TourAppointment selectedAppointment { get; set; }
        public Tour selectedTour { get; set; }
        public TourController TourController { get; set; }
        public Guest2 Guest2 { get; set; }
        public Guest2Controller Guest2Controller { get; set; }
        public List<string> StopsList { get; set; }
        
        public UpdateTicketWindow(string username, int tourAppId, int tourId)
        {
            InitializeComponent();
            DataContext = this;
            TicketController = new TicketController();
            TourAppointmentController = new TourAppointmentController();
            Guest2Controller = new Guest2Controller();
            TourController = new TourController();

            selectedAppointment = TourAppointmentController.GetOne(tourAppId);
            selectedTour = TourController.GetOne(tourId);
            Guest2 = Guest2Controller.GetOne(username);
            Ticket = TicketController.GetGuest2Ticket(Guest2, selectedAppointment);

            slider.Maximum = selectedAppointment.AvailableSeats + Ticket.NumberOfGuests;

            // transfer to Tour -> StopsList
            StopsList = new List<string>();
            foreach (string stop in selectedTour.StopsList)
            {
                StopsList.Add(stop.Trim());
            }
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
        
        private void UpdateTicket(object sender, RoutedEventArgs e)
        {
            TicketController.Update(Ticket);
            TourAppointmentController.UpdateAppointmentUpdate(selectedAppointment.Id, Ticket);
            Close();
        }

        private void StopsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket.TourStop = StopsList[StopsComboBox.SelectedIndex];
        }

        // doesn't work, finds newSelected appointment but can't change selectedAppointment
        /*
        private void DatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = selectedAppointment.Tour.dates[DatesComboBox.SelectedIndex];
            TourAppointment newSelected = TourAppointmentController.GetByDate(date);
            //selectedAppointment = newSelected;
            //selectedAppointment.Id = newSelected.Id;
            selectedAppointment = TourAppointmentController.GetOne(newSelected.Id);

            //selectedAppointment = selectedAppointment;
            //Ticket = TicketController.GetGuest2Ticket(Guest2, selectedAppointment);
            TicketController.Update(Ticket);
            //TicketController.ChangeAppointment(Ticket);
        }*/
    }
}
