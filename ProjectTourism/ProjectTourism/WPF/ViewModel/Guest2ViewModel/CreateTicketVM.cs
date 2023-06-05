using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.Model;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;
using System.Windows.Input;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class CreateTicketVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public TourDTO SelectedTour { get; set; }
        public TourAppointmentDTO selectedAppointment { get; set; }
        public TicketDTO Ticket { get; set; }
        public List<DateTime> dates { get; set; }

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

        private object _HomeContent;
        public object HomeContent
        {
            get { return _HomeContent; }
            set { _HomeContent = value; OnPropertyChanged(); }
        }
        public CreateTicketVM() { }
        public CreateTicketVM(Guest2DTO guest2)
        {
            Guest2 = guest2;
            if (Guest2.SelectedTour != null)
                SelectedTour = Guest2.SelectedTour;
            PickedAnAppointment = false;
            Ticket = new TicketDTO(new Ticket());

            // TO DO
            dates = new List<DateTime>();
            //dates = FindDates();

            //HomeCommand
            HomeCommand = new RelayCommand(ReturnHome);
        }
        public ICommand HomeCommand { get; set; }
        private void ReturnHome(Object obj)
        {
            Guest2.SelectedTour = null;
            HomeContent = new HomeVM(Guest2);
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
    }
}
