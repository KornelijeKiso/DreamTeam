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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTourism.DTO;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.View.Guest2View.UserControls
{
    /// <summary>
    /// Interaction logic for CreateTicketUserControl.xaml
    /// </summary>
    public partial class CreateTicketUserControl : UserControl
    {
        public CreateTicketVM createTicketVM { get; set; }
        public CreateTicketUserControl()
        {
            InitializeComponent();
            //DataContext = new CreateTicketVM()
        }

        public CreateTicketUserControl(Guest2DTO guest2)
        {
            InitializeComponent();
            createTicketVM = new CreateTicketVM(guest2);
            DataContext = createTicketVM;
        }

        private void CreateTicket(object sender, RoutedEventArgs e)
        {
            if (createTicketVM.PickedAnAppointment)
            {
                createTicketVM.Ticket.CreateTicket(new Ticket(createTicketVM.selectedAppointment.Id, createTicketVM.SelectedTour.StopsList[StopsComboBox.SelectedIndex], createTicketVM.Guest2.Username, int.Parse(sliderText.Text)));
                createTicketVM.selectedAppointment.UpdateTourAppointmentDTO(createTicketVM.selectedAppointment);
                //Close();
            }
            else
            {
                MessageBox.Show("You can't buy tickets or use vouchers for this tour! \nIt is already full!");
                //Close();
            }
        }


        private void DatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = createTicketVM.dates[DatesComboBox.SelectedIndex];
            createTicketVM.selectedAppointment = createTicketVM.SelectedTour.TourAppointments.First(a => a.TourDateTime == date);
            slider.Maximum = createTicketVM.selectedAppointment.AvailableSeats;
            createTicketVM.PickedAnAppointment = true;
        }
        private void UseVoucherClick(object sender, RoutedEventArgs e)
        {
            if (createTicketVM.dates.Count > 0)
            {
                createTicketVM.Ticket.CreateTicket(new Ticket(createTicketVM.selectedAppointment.Id, StopsComboBox.Text, createTicketVM.Guest2.Username, int.Parse(sliderText.Text)));
                createTicketVM.Ticket = createTicketVM.Ticket.GetLast();
                UnusedVouchersWindow unusedVouchersWindow = new UnusedVouchersWindow(createTicketVM.Guest2, createTicketVM.Ticket);
                unusedVouchersWindow.ShowDialog();
                if (unusedVouchersWindow.IsUsed)
                {
                    createTicketVM.selectedAppointment.UpdateTourAppointmentDTO(createTicketVM.selectedAppointment);
                }
                else
                {   // delete created Ticket if UseVoucher is canceled
                    createTicketVM.Ticket.RemoveLast();
                }
                //Close();
            }
            else
            {
                MessageBox.Show("You can't buy tickets or use vouchers for this tour! \nIt is already full!");
                //Close();
            }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            // TO DO -> return to HomeVM
            //Close();
        }
    }
}
