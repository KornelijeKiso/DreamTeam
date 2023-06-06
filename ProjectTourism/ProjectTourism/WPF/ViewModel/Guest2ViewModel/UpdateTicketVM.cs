using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Model;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;
using ProjectTourism.Services;
using System.Windows.Input;
using System.Windows;


namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class UpdateTicketVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public TourDTO SelectedTour { get; set; }
        public TicketDTO Ticket { get; set; }
        public TicketService TicketService { get; set; }
        public TourAppointmentDTO selectedAppointment { get; set; }
        private object _TicketContent;
        public object TicketContent
        {
            get { return _TicketContent; }
            set { _TicketContent = value; OnPropertyChanged(); }
        }

        public UpdateTicketVM() { }
        public UpdateTicketVM(Guest2DTO guest2, TicketDTO ticket) 
        {
            Guest2 = guest2;
            Ticket = ticket;
            selectedAppointment = ticket.TourAppointment;
            SelectedTour = ticket.TourAppointment.Tour;
            TicketService = new TicketService();

            //TicketCommand
            TicketCommand = new RelayCommand(ReturnToTickets);
        }
        public ICommand TicketCommand { get; set; }
        private void ReturnToTickets(Object obj)
        {
            TicketContent = new TicketsVM(Guest2);
        }

        // UPDATE TICKET COMMAND 
        private ICommand _UpdateTicketCommand; 
        public ICommand UpdateTicketCommand
        {
            get
            {
                return _UpdateTicketCommand ?? (_UpdateTicketCommand = new CommandHandler(() => UpdateTicket(), () => true));
            }
        }
        private void UpdateTicket()
        {
            if (selectedAppointment != null)
            {
                TicketService.Update(Ticket.GetTicket());
                MessageBox.Show("Ticket updated! ");
                TicketContent = new TicketsVM(Guest2);
            }
            else
            {
                MessageBox.Show("Please check if you entered the data correctly! ");
            }
        }
    }
}
