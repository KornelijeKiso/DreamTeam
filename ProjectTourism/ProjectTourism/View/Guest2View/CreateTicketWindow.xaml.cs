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
        public TicketController TicketController {get; set;} 
        public Route SelectedRoute { get; set; }
        public RouteController RouteController { get; set; }
        public Guest2 Guest2 { get; set; }
        public Guest2Controller Guest2Controller { get; set; }
        public List<string> StopsList { get; set; }


        public CreateTicketWindow(string username, int routeId)
        {
            InitializeComponent();
            DataContext = this;
            TicketController = new TicketController();
            RouteController = new RouteController();
            SelectedRoute = RouteController.GetOne(routeId);
            Guest2Controller = new Guest2Controller();
            Guest2 = Guest2Controller.GetOne(username);
            Ticket = new Ticket();

            // transfer to Route -> StopsList
            StopsList = new List<string>();
            foreach (string stop in SelectedRoute.StopsList)
            {
                StopsList.Add(stop.Trim());
            }
            
            StopsList.RemoveAt(StopsList.Count - 1); // Guest can't chose Finish stop to join the Route


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
            Ticket ticket = new Ticket(SelectedRoute.Id, Guest2.Username, Ticket.NumberOfGuests);
            TicketController.Add(ticket);
            Close();
        }

        private void StopsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
