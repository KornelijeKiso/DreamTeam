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
        public Route SelectedRoute { get; set; }
        public RouteController RouteController { get; set; }
        public Guest2 Guest2 { get; set; }
        public Guest2Controller Guest2Controller { get; set; }
        public List<string> StopsList { get; set; }
        public int AvailableTickets { get; set; }


        public UpdateTicketWindow(string username, int routeId)
        {
            InitializeComponent();
            DataContext = this;
            TicketController = new TicketController();
            RouteController = new RouteController();
            SelectedRoute = RouteController.GetOne(routeId);
            Guest2Controller = new Guest2Controller();
            Guest2 = Guest2Controller.GetOne(username);
            Ticket = TicketController.GetGuest2Ticket(Guest2, SelectedRoute);

            // transfer to Route -> StopsList
            StopsList = new List<string>();
            foreach (string stop in SelectedRoute.StopsList)
            {
                StopsList.Add(stop.Trim());
            }

            StopsList.RemoveAt(StopsList.Count - 1); // Guest can't chose Finish stop to join the Route

            AvailableTickets = GetAvailableTickets();
            if (AvailableTickets <= 0)
                AvailableTickets = Ticket.NumberOfGuests;
            
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
            Close();
        }

        private void StopsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ticket.RouteStop = StopsList[StopsComboBox.SelectedIndex];
        }

        public int GetAvailableTickets()
        {
            List<Ticket> available = TicketController.GetByRoute(SelectedRoute);
            int? availableCount = SelectedRoute.MaxNumberOfGuests;

            if (availableCount != null)
            {
                availableCount = availableCount.Value;
                foreach (Ticket ticket in available)
                {
                    if (ticket.Id != Ticket.Id) // doesn't count for selected Ticket that should be updated
                        availableCount = availableCount - ticket.NumberOfGuests;
                }

                if (availableCount >= 1) return availableCount.Value;
                return -1;
            }
            return -1;
        }
    }
}
