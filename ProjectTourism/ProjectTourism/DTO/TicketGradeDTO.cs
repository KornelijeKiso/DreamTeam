using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.DTO
{
    public class TicketGradeDTO : INotifyPropertyChanged
    {
        private TicketGrade _ticketGrade;
        public TicketGradeDTO(TicketGrade ticketGrade)
        {
            _ticketGrade = ticketGrade;
        }

        public bool IsAlreadyGraded(Ticket ticket)
        {
            TicketService ticketService = new TicketService();
            _ticketGrade.Ticket = ticketService.GetOne(ticket.Id);

            TicketGradeService ticketGradeService = new TicketGradeService();
            List<TicketGrade> grades = ticketGradeService.GetAll();
            foreach (TicketGrade grade in grades)
            {
                if (grade.TicketId == ticket.Id)
                    return true;
            }
            return false;
        }

        public TicketGrade GetTicketGrade()
        {
            return _ticketGrade;
        }

        public string[] GetPictureURLsFromCSV()
        {
            string[] pictures = PictureURLs.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
        }

        public string[] Pictures { get => GetPictureURLsFromCSV(); }
        public int Id
        {
            get => _ticketGrade.Id;
            set
            {
                if (value != _ticketGrade.Id)
                {
                    _ticketGrade.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TicketId
        {
            get => _ticketGrade.TicketId;
            set
            {
                if (value != _ticketGrade.TicketId)
                {
                    _ticketGrade.TicketId = value;
                    OnPropertyChanged();
                }
            }
        }

        public TicketDTO Ticket
        {
            get => new TicketDTO(_ticketGrade.Ticket);
            set
            {
                if (value.GetTicket() != _ticketGrade.Ticket)
                {
                    _ticketGrade.Ticket = value.GetTicket();
                    OnPropertyChanged();
                }
            }
        }

        public static readonly string[] CategoryNames = TicketGrade.CategoryNames;

        public Dictionary<string, int> Grades
        {
            get => _ticketGrade.Grades;
            set
            {
                if (value != _ticketGrade.Grades)
                {
                    _ticketGrade.Grades = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Comment
        {
            get => _ticketGrade.Comment;
            set
            {
                if (value != _ticketGrade.Comment)
                {
                    _ticketGrade.Comment = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? PictureURLs
        {
            get => _ticketGrade.PictureURLs;
            set
            {
                _ticketGrade.PictureURLs = value;
                OnPropertyChanged();
            }
        }
        public bool IsNotReported
        {
            get => _ticketGrade.IsNotReported;
            set
            {
                if (value != _ticketGrade.IsNotReported)
                {
                    _ticketGrade.IsNotReported = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
