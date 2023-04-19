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
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for UpdateTicketWindow.xaml
    /// </summary>
    public partial class UpdateTicketWindow : Window, INotifyPropertyChanged
    {
        public Ticket Ticket { get; set; }
        public TicketService TicketService { get; set; }
        public TourAppointmentService TourAppointmentService { get; set; }
        public TourAppointment selectedAppointment { get; set; }
        public Tour selectedTour { get; set; }
        public TourService TourService { get; set; }
        public List<string> StopsList { get; set; }
        
        public UpdateTicketWindow(string username, int tourAppId, int tourId)
        {
            InitializeComponent();
            DataContext = this;
            TicketService = new TicketService(new TicketRepository());
            TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            TourService = new TourService(new TourRepository());

            selectedTour = TourService.GetOne(tourId);
            //Ticket = TicketService.GetGuest2Ticket(username, tourAppId);
            selectedAppointment = TourAppointmentService.GetOne(tourAppId);

            //slider.Maximum = selectedAppointment.AvailableSeats + Ticket.NumberOfGuests;

            StopsList = selectedTour.StopsList;
            StopsList.RemoveAt(StopsList.Count() - 1);
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
            TicketService.Update(Ticket);
            //TourAppointmentService.UpdateAppointmentTicket(selectedAppointment.Id, Ticket);
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
