using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.Guest2View.TicketView
{
    /// <summary>
    /// Interaction logic for Guest2AttendanceWindow.xaml
    /// </summary>
    public partial class Guest2AttendanceWindow : Window
    {
        public Guest2VM Guest2 { get; set; }
        public TicketVM Ticket { get; set; }
        public Guest2AttendanceWindow(TicketVM ticket, Guest2VM guest2)
        {
            InitializeComponent();
            DataContext = this;
            Guest2 = guest2;
            Ticket = ticket;

        }

       

        private void JoinTourAppointmentClick(object sender, RoutedEventArgs e)
        {
            if (Ticket.TourAppointment.State == TOURSTATE.STARTED && Ticket.HasGuideChecked 
             && Ticket.TourAppointment.CurrentTourStop.Equals(Ticket.TourStop))
            {
                Ticket.HasGuestConfirmed = true;
                /// call update in TicketVM
                MessageBox.Show("Successfully joined Tour ! ");
                Close();
            }
            else
            {

            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
