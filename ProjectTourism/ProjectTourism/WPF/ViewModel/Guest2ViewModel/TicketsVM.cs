using System.Collections.ObjectModel;
using System.Windows.Input;
using ProjectTourism.Utilities;
using ProjectTourism.WPF.Guest2View.TicketView;
using ProjectTourism.WPF.View.Guest2View.TicketView;
using ProjectTourism.DTO;
using System.Windows;
using ProjectTourism.WPF.View.Guest2View;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class TicketsVM : ViewModelBase
    {
        public Guest2DTO Guest2 { get; set; }
        public TicketDTO SelectedTicket { get; set; }
        public ObservableCollection<TicketDTO> UpcomingTickets { get; set; }
        public ObservableCollection<TicketDTO> AttendedTickets { get; set; }
        public ObservableCollection<TicketDTO> SkippedTickets { get; set; }
        public ObservableCollection<TicketDTO> CanceledTickets { get; set; }
        public TicketGradeDTO TicketGrade { get; set; }
        private object _TicketContent;
        public object TicketContent
        {
            get { return _TicketContent; }
            set { _TicketContent = value; OnPropertyChanged(); }
        }

        public TicketsVM() { }
        public TicketsVM(Guest2DTO guest2)
        {
            Guest2 = guest2;
            UpcomingTickets = SetUpcomingTickets();
            AttendedTickets = SetAttendedTickets();
            SkippedTickets = SetSkippedTickets();
            CanceledTickets = SetCancecledByGuideTickets();
            TicketGrade = new TicketGradeDTO(new Model.TicketGrade());

            // Update Ticket Command
            UpdateTicketCommand = new RelayCommand(UpdateTicket);
            Guest2AttendanceCommand = new RelayCommand(Guest2Attendance);
        }

        public ICommand UpdateTicketCommand { get; set; }
        private void UpdateTicket(object obj)
        {
            if (SelectedTicket != null)
                TicketContent = new UpdateTicketVM(Guest2, SelectedTicket);
            else
                MessageBox.Show("Please select the ticket you would like to update!");
        }

        public ICommand Guest2AttendanceCommand { get; set; }
        public void Guest2Attendance(object obj)
        {
            if (SelectedTicket != null)
                TicketContent = new Guest2AttendanceVM(Guest2, SelectedTicket);
            else
                MessageBox.Show("Please select the ticket!");
        }

        private ObservableCollection<TicketDTO> SetUpcomingTickets()
        {
            ObservableCollection<TicketDTO> upcoming = new ObservableCollection<TicketDTO>();
            foreach (var ticket in Guest2.Tickets)
            {
                if (IsUpcoming(ticket))
                {
                    upcoming.Add(ticket);
                }
            }
            return upcoming;
        }
        private bool IsUpcoming(TicketDTO ticketDTO)
        {
            return ticketDTO.TourAppointment.State == TOURSTATE.STARTED
                || ticketDTO.TourAppointment.State == TOURSTATE.READY;
        }

        private ObservableCollection<TicketDTO> SetAttendedTickets()
        {
            ObservableCollection<TicketDTO> attended = new ObservableCollection<TicketDTO>();
            foreach (var ticket in Guest2.Tickets)
            {
                if (IsAttended(ticket))
                {
                    attended.Add(ticket);
                }
            }
            return attended;
        }
        private bool IsAttended(TicketDTO ticketDTO)
        {
            return ticketDTO.HasGuideChecked
                && ticketDTO.HasGuestConfirmed
                && (ticketDTO.TourAppointment.State == TOURSTATE.FINISHED
                || ticketDTO.TourAppointment.State == TOURSTATE.STOPPED);
        }

        private ObservableCollection<TicketDTO> SetSkippedTickets()
        {
            ObservableCollection<TicketDTO> skipped = new ObservableCollection<TicketDTO>();
            foreach (var ticket in Guest2.Tickets)
            {
                if (IsSkipped(ticket))
                {
                    skipped.Add(ticket);
                }
            }
            return skipped;
        }
        private bool IsSkipped(TicketDTO ticketDTO)
        {
            return ticketDTO.HasGuideChecked
                && !ticketDTO.HasGuestConfirmed
                && (ticketDTO.TourAppointment.State == TOURSTATE.FINISHED
                || ticketDTO.TourAppointment.State == TOURSTATE.STOPPED);
        }

        private ObservableCollection<TicketDTO> SetCancecledByGuideTickets()
        {
            ObservableCollection<TicketDTO> canceled = new ObservableCollection<TicketDTO>();
            foreach (var ticket in Guest2.Tickets)
            {
                if (IsCanceled(ticket))
                {
                    canceled.Add(ticket);
                }
            }
            return canceled;
        }
        private bool IsCanceled(TicketDTO ticketDTO)
        {
            return ticketDTO.TourAppointment.State == TOURSTATE.CANCELED;
        }

        private ICommand _GradeTicketCommand;
        public ICommand GradeTicketCommand
        {
            get
            {
                return _GradeTicketCommand ?? (_GradeTicketCommand = new CommandHandler(() => GradeTicket(), () => true));
            }
        }
        public void GradeTicket()
        {
            if (SelectedTicket != null && !TicketGrade.IsAlreadyGraded(SelectedTicket.GetTicket()))
            {
                GradeTicketWindow gradeTicketWindow = new GradeTicketWindow(SelectedTicket, Guest2);
                gradeTicketWindow.ShowDialog(); 
                MessageBox.Show("Successfully graded a Ticket!");
            }
            else
            {
                if (SelectedTicket != null && TicketGrade.IsAlreadyGraded(SelectedTicket.GetTicket()))
                {
                    MessageBox.Show("You already left a review!");
                }
                else
                    MessageBox.Show("Please select the ticket you would like to grade!");
            }
            
        }

        private ICommand _GeneratePDFDocumentCommand;
        public ICommand GeneratePDFDocumentCommand
        {
            get
            {
                return _GeneratePDFDocumentCommand ?? (_GeneratePDFDocumentCommand = new CommandHandler(() => GeneratePDFDocumentCommand_Click(), () => CanGenerate));
            }
        }
        public bool CanGenerate
        {
            get
            {
                return (SelectedTicket != null);
            }
        }
        public void GeneratePDFDocumentCommand_Click()
        {
            GeneratePDFDocumentVM generatePDFDocumentVM = new GeneratePDFDocumentVM(SelectedTicket);
            MessageBox.Show("Your Report is generated in folder ../../PDF/Guest2PDFs\n\n" +
                "under the name: ticket_report_" + SelectedTicket.Guest2Username + "_" + SelectedTicket.Id.ToString() + ".pdf");
            PDFDisplayWindow pDFDisplayWindow = new PDFDisplayWindow(SelectedTicket);
            pDFDisplayWindow.ShowDialog();
        }
    }
}
