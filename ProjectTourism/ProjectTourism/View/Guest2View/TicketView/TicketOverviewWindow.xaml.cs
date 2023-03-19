using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TicketOverviewWindow.xaml
    /// </summary>
    public partial class TicketOverviewWindow : Window, INotifyPropertyChanged, IObserver
    {
        public User User { get; set; }
        public UserController UserController { get; set; }
        public Guest2 Guest { get; set; }
        public Guest2Controller GuestController { get; set; }
        public TourAppointmentController TourAppointmentController { get; set; }
        public Ticket? SelectedTicket { get; set; }
        public TicketController TicketController { get; set; }
        public ObservableCollection<Ticket> Tickets { get; set; }

        public TicketOverviewWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            UserController = new UserController();
            User = UserController.GetOne(username);
            GuestController = new Guest2Controller();
            Guest = GuestController.GetOne(username);
            TourAppointmentController = new TourAppointmentController();

            TicketController = new TicketController();
            TicketController.Subscribe(this);
            Tickets = new ObservableCollection<Ticket>(TicketController.GetByGuest(Guest));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ReturnTicket(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket != null)
            {
                MessageBoxResult result = ConfirmTicketDelete();
                if (result == MessageBoxResult.Yes)
                {
                    TourAppointmentController.UpdateAppointmentReturn(SelectedTicket.TourAppointmentId, SelectedTicket);
                    TicketController.Delete(SelectedTicket);
                    UpdateTicketsList();
                }
                else
                {
                    MessageBox.Show("Unable to delete ticket!");
                }
            }
            else
                MessageBox.Show("Please select the ticket.");
        }

        private MessageBoxResult ConfirmTicketDelete()
        {
            string sMessageBoxText = $"Are you sure you want to return your Ticket?\n{SelectedTicket.TourAppointment.Tour.Name}";
            string sCaption = "Return Ticket";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }

        private void UpdateTicket(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket != null)
            {
                UpdateTicketWindow updateTicketWindow = new UpdateTicketWindow(Guest.Username, SelectedTicket.TourAppointmentId, SelectedTicket.TourAppointment.Tour.Id);
                updateTicketWindow.ShowDialog();
                TicketController = updateTicketWindow.TicketController;
                SelectedTicket = updateTicketWindow.Ticket;           
                UpdateTicketsList();
            }
            else
                MessageBox.Show("Please select the ticket.");
        }

        private void UpdateTicketsList()
        {
            Tickets.Clear();
            foreach (Ticket ticket in TicketController.GetByGuest(Guest))
            {
                Tickets.Add(ticket);
            }
        }
        public void Update()
        {
            UpdateTicketsList();
            throw new NotImplementedException();
        }
    }
}
