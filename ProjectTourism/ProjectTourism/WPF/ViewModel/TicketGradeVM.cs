using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class TicketGradeVM : INotifyPropertyChanged
    {
        private TicketGrade _ticketGrade;
        public TicketGradeVM(TicketGrade ticketGrade)
        {
            _ticketGrade = ticketGrade;
        }

        public TicketGrade GetTicketGrade()
        {
            return _ticketGrade;
        }

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

        public Ticket Ticket
        {
            get => _ticketGrade.Ticket;
            set
            {
                if (value != _ticketGrade.Ticket)
                {
                    _ticketGrade.Ticket = value;
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

        public string[] Pictures
        {
            get => _ticketGrade.Pictures;
            set
            {
                if (value != _ticketGrade.Pictures)
                {
                    _ticketGrade.Pictures = value;
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
