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
    /// Interaction logic for UsedTicketOverviewWindow.xaml
    /// </summary>
    public partial class UsedTicketOverviewWindow : Window, INotifyPropertyChanged, IObserver
    {
        public TourAppointmentController TourAppointmentController { get; set; }
        public Ticket? SelectedTicket { get; set; }
        public TicketController TicketController { get; set; }
        public ObservableCollection<Ticket> UsedTickets { get; set; }
        public string Username { get; set; }

        public UsedTicketOverviewWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            Username = username;
            TourAppointmentController = new TourAppointmentController();
            TicketController = new TicketController();
            UsedTickets = FilterTickets(TicketController.GetByGuest(username));     // only used tickets
            TicketController.Subscribe(this);
        }
        
        // only used tickets
        private ObservableCollection<Ticket> FilterTickets(List<Ticket> AllGuestTickets)
        {
            List<Ticket> filtered = new List<Ticket>();
            foreach (Ticket ticket in AllGuestTickets)
            {
                if ((ticket.HasGuestConfirmed) && (ticket.HasGuideChecked))
                    filtered.Add(ticket);
            }

            ObservableCollection<Ticket> tickets = new ObservableCollection<Ticket>(filtered);
            return tickets;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        // Opens a window for leaving a grade
        private void GradeTicketClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket != null)
            {
                //if () // already has a grade
                    //MessageBox.Show("Ticket already has a grade!");
                //else
                {
                    GradeTicketWindow gradeTicketWindow = new GradeTicketWindow(SelectedTicket.Id);
                    gradeTicketWindow.ShowDialog();
                }
            }
            else
                MessageBox.Show("Please select the ticket for the tour you would like to grade.");
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
