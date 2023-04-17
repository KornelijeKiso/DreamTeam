using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Utilities;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.View.Guest2View.TicketView;
using System.Windows;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TicketsVM : ViewModelBase
    {
        public CurrentUserService CurrentUserService { get; set; }
        public UserVM CurrentUser { get; set; }
        public Guest2VM Guest2 { get; set; }
        public TicketVM SelectedTicket { get; set; }
        public ObservableCollection<TicketVM> UpcomingTickets { get; set; }
        public ObservableCollection<TicketVM> AttendedTickets { get; set; }
        public ObservableCollection<TicketVM> SkippedTickets { get; set; }
        //public ObservableCollection<TicketVM> CancecledByGuideTickets { get; set; }
        public TicketGradeVM TicketGradeVM { get; set; }

        public TicketsVM()
        {
            SetGuest2();
            UpcomingTickets = SetUpcomingTickets();
            AttendedTickets = SetAttendedTickets();
            SkippedTickets = SetSkippedTickets();
            //CancecledByGuideTickets = SetCancecledByGuideTickets();
        }
        private void SetGuest2()
        {
            CurrentUserService = new CurrentUserService(new CurrentUserRepository());
            CurrentUser = new UserVM(CurrentUserService.GetUser());
            Guest2 = new Guest2VM(CurrentUser.Username);
        }

        private ObservableCollection<TicketVM> SetUpcomingTickets()
        {
            ObservableCollection<TicketVM> upcoming = new ObservableCollection<TicketVM>();
            foreach (var ticket in Guest2.Tickets)
            {
                if (IsUpcoming(ticket))
                {
                    upcoming.Add(ticket);
                }
            }
            return upcoming;
        }
        private bool IsUpcoming(TicketVM ticketVM)
        {
            return ticketVM.TourAppointment.State == TOURSTATE.STARTED
                || ticketVM.TourAppointment.State == TOURSTATE.READY;
        }

        private ObservableCollection<TicketVM> SetAttendedTickets()
        {
            ObservableCollection<TicketVM> attended = new ObservableCollection<TicketVM>();
            foreach (var ticket in Guest2.Tickets)
            {
                if (IsAttended(ticket))
                {
                    attended.Add(ticket);
                }
            }
            return attended;
        }
        private bool IsAttended(TicketVM ticketVM)
        {
            return ticketVM.HasGuideChecked
                && ticketVM.HasGuestConfirmed
                && (ticketVM.TourAppointment.State == TOURSTATE.FINISHED
                || ticketVM.TourAppointment.State == TOURSTATE.STOPPED);
        }

        private ObservableCollection<TicketVM> SetSkippedTickets()
        {
            ObservableCollection<TicketVM> skipped = new ObservableCollection<TicketVM>();
            foreach (var ticket in Guest2.Tickets)
            {
                if (IsSkipped(ticket))
                {
                    skipped.Add(ticket);
                }
            }
            return skipped;
        }
        private bool IsSkipped(TicketVM ticketVM)
        {
            return ticketVM.HasGuideChecked
                && !ticketVM.HasGuestConfirmed
                && (ticketVM.TourAppointment.State == TOURSTATE.FINISHED
                || ticketVM.TourAppointment.State == TOURSTATE.STOPPED);
        }

        private ICommand _GradeTicketCommand;
        public ICommand GradeTicketCommand
        {
            get
            {
                return _GradeTicketCommand ?? (_GradeTicketCommand = new CommandHandler(() => GradeTicket_Click(), () => CanGrade));
            }
        }
        public bool CanGrade
        {
            get
            {
                if (SelectedTicket != null)
                {
                    TicketGradeVM = new TicketGradeVM(SelectedTicket.Id);
                    return (Guest2 != null
                         && TicketGradeVM == null);
                }
                return (SelectedTicket != null && Guest2 != null
                     && SelectedTicket.TicketGrade == null);
            }
        }
        public void GradeTicket_Click()
        {
            if (SelectedTicket == null)
            {
                MessageBox.Show("Select the Ticket!");
            }
            else
            {
                TicketGradeVM = new TicketGradeVM(SelectedTicket.Id);
                if (TicketGradeVM == null)
                {
                    GradeTicketWindow gradeTicketWindow = new GradeTicketWindow(SelectedTicket.Id);
                    gradeTicketWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Already has a grade!");
                }
            }
        }

    }
}
