using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Model;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;
using System.Windows.Input;
using System.Windows;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class CreateTicketVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public TourDTO SelectedTour { get; set; }
        public TicketDTO Ticket { get; set; }
        public List<DateTime> dates { get; set; }
        private DateTime? _date;
        public DateTime? date 
        { 
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _PickedAnAppointment;
        public bool PickedAnAppointment
        {
            get => _PickedAnAppointment;
            set
            {
                if (value != _PickedAnAppointment)
                {
                    _PickedAnAppointment = value;
                    OnPropertyChanged();
                }
            }
        }
        private TourAppointmentDTO _selectedAppointment;
        public TourAppointmentDTO selectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (value != _selectedAppointment)
                {
                    _selectedAppointment = value;
                    OnPropertyChanged();
                }
            }
        }

        private object _Content;
        public object Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
        }
        public CreateTicketVM() { }
        
        public CreateTicketVM(Guest2DTO guest2, TourDTO tour)
        {
            Guest2 = guest2;
            SelectedTour = tour;
            Ticket = new TicketDTO(new Ticket());

            dates = FindDates();
            date = null;
            PickedAnAppointment = false;
            DateValidationVisible = true;

            // Commands
            ContentCommand = new RelayCommand(ReturnToHome);
            DateSelectionChangedCommand = new RelayCommand(DateSelectionChanged);
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
            foreach (var tourApp in tour.TourAppointments)
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



        // CREATE TICKET COMMAND 
        private ICommand _CreateTicketCommand;
        public ICommand CreateTicketCommand
        {
            get
            {
                return _CreateTicketCommand ?? (_CreateTicketCommand = new CommandHandler(() => CreateTicketClick(), () => true));
            }
        }
        private void CreateTicketClick()
        {
            if (Ticket.IsValid && selectedAppointment != null)
            {
                Ticket.CreateTicket(new Ticket(selectedAppointment.Id, Ticket.TourStop, Guest2.Username, Ticket.NumberOfGuests));
                Ticket.TourAppointment = selectedAppointment;
                Ticket.TourAppointment.Tour = SelectedTour;
                Guest2.Tickets.Add(Ticket);
                selectedAppointment.UpdateTourAppointmentDTO(selectedAppointment);
                MessageBox.Show("Successfully reserved a ticket! ");
                Guest2.Synchronize(Guest2.Username);    // updating Guest2.Tickets
                Content = new HomeVM(Guest2);
            }
            else
            {
                MessageBox.Show("Please check if you entered the data correctly! ");
            }
        }
        // USE VOUCHER COMMAND 
        private ICommand _UseVoucherCommand;
        public ICommand UseVoucherCommand
        {
            get
            {
                return _UseVoucherCommand ?? (_UseVoucherCommand = new CommandHandler(() => UseVoucherClick(), () => true));
            }
        }
        private void UseVoucherClick()
        {
            if (Ticket.IsValid && selectedAppointment != null
                && (Guest2.Vouchers.Where(v => v.Status == STATUS.VALID).Count() != 0))
            {
                Ticket.CreateTicket(new Ticket(selectedAppointment.Id, Ticket.TourStop, Guest2.Username, Ticket.NumberOfGuests));
                Ticket = Ticket.GetLast();
                UnusedVouchersWindow unusedVouchersWindow = new UnusedVouchersWindow(Guest2, Ticket);
                unusedVouchersWindow.ShowDialog();
                if (unusedVouchersWindow.IsUsed)
                {
                    selectedAppointment.UpdateTourAppointmentDTO(selectedAppointment);
                    Ticket.HasVoucher = true;
                    MessageBox.Show("Successfully reserved a ticket!\nYou used your Voucher for this reservation! ");
                    Guest2.Synchronize(Guest2.Username);    // updating used Voucher
                    Content = new HomeVM(Guest2);
                }
                else
                {   // delete created Ticket if UseVoucher is canceled
                    Ticket.RemoveLast();
                }
            }
            else
            {
                if (Guest2.Vouchers.Where(v => v.Status == STATUS.VALID).Count() == 0)
                {
                    MessageBox.Show("You don't have valid Vouchers!\nPlease select Buy Ticket option! ");
                }
                else
                    MessageBox.Show("Please check if you entered the data correctly! ");
            }
        }
        public ICommand ContentCommand { get; set; }
        private void ReturnToHome(Object obj)
        {
            Content = new HomeVM(Guest2);
        }

        // DATE COMBO BOX SELECTION CHANGED
        public ICommand DateSelectionChangedCommand { get; set; }
        private void DateSelectionChanged(object obj)
        {
            if (date != null)
            {
                selectedAppointment = SelectedTour.TourAppointments.First(a => a.TourDateTime == date);
                PickedAnAppointment = true;
                DateValidationVisible = false;
            }
            else
                MessageBox.Show("Please select the date! ");
        }

        private bool _DateValidationVisible;
        public bool DateValidationVisible
        {
            get => _DateValidationVisible;
            set
            {
                if (value != _DateValidationVisible)
                {
                    _DateValidationVisible = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
