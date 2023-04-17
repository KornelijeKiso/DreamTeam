using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectTourism.WPF.ViewModel
{
    public class TicketVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private Ticket _ticket;

        public TicketVM(Ticket ticket)
        {
            _ticket = ticket;
            Synchronize();
        }

        private void Synchronize()
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            _ticket.TourAppointment = tourAppointmentService.GetOne(_ticket.TourAppointmentId);

            Guest2Service guest2Service = new Guest2Service(new Guest2Repository());
            _ticket.Guest2 = guest2Service.GetOne(_ticket.Guest2Username);

            //TicketGradeService ticketGradeService = new TicketGradeService(new TicketGradeRepository());
            //_ticket.TicketGrade = ticketGradeService.GetOne(_ticket.TicketGradeId);
        }
        public Ticket GetTicket()
        {
            return _ticket;
        }

        public int Id
        {
            get => _ticket.Id;
            set
            {
                if (value != _ticket.Id)
                {
                    _ticket.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasVoucher
        {
            get => _ticket.HasVoucher;
            set
            {
                if (value != _ticket.HasVoucher)
                {
                    _ticket.HasVoucher = value;
                    OnPropertyChanged();
                }
            }
        }
        public SolidColorBrush ButtonColor
        {
            get => _ticket.ButtonColor;
            set
            {
                if (value != _ticket.ButtonColor)
                {
                    _ticket.ButtonColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TourAppointmentId
        {
            get => _ticket.TourAppointmentId;
            set
            {
                if (value != _ticket.TourAppointmentId)
                {
                    _ticket.TourAppointmentId = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourAppointmentVM TourAppointment
        {
            get => new TourAppointmentVM(_ticket.TourAppointment);
            set
            {
                if (value.GetTourAppointment() != _ticket.TourAppointment)
                {
                    _ticket.TourAppointment = value.GetTourAppointment();
                    OnPropertyChanged();
                }
            }
        }

        public string TourStop
        {
            get => _ticket.TourStop;
            set
            {
                if (value != _ticket.TourStop)
                {
                    _ticket.TourStop = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Guest2Username
        {
            get => _ticket.Guest2Username;
            set
            {
                if (value != _ticket.Guest2Username)
                {
                    _ticket.Guest2Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest2VM Guest2
        {
            get => new Guest2VM(_ticket.Guest2);
            set
            {
                if (value.GetGuest2() != _ticket.Guest2)
                {
                    _ticket.Guest2 = value.GetGuest2();
                    OnPropertyChanged();
                }
            }
        }
        public int NumberOfGuests
        {
            get => _ticket.NumberOfGuests;
            set
            {
                if (value != _ticket.NumberOfGuests)
                {
                    _ticket.NumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool HasGuideChecked
        {
            get => _ticket.HasGuideChecked;
            set
            {
                if (_ticket.HasGuideChecked != value)
                {
                    _ticket.HasGuideChecked = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool HasGuestConfirmed
        {
            get => _ticket.HasGuestConfirmed;
            set
            {
                if (_ticket.HasGuestConfirmed != value)
                {
                    _ticket.HasGuestConfirmed = value;
                    OnPropertyChanged();
                }
            }
        }
        //public int TicketGradeId
        //{
        //    get => _ticket.TicketGradeId;
        //    set
        //    {
        //        if (value != _ticket.TicketGradeId)
        //        {
        //            _ticket.TicketGradeId = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public TicketGradeVM TicketGrade
        {
            get => new TicketGradeVM(_ticket.TicketGrade);
            set
            {
                if (value.GetTicketGrade() != _ticket.TicketGrade)
                {
                    _ticket.TicketGrade = value.GetTicketGrade();
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // validation
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "NumberOfGuests")
                {
                    if (string.IsNullOrEmpty(NumberOfGuests.ToString()))
                        return "Number Of Tickets is required!";
                }
               

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "NumberOfGuests" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
