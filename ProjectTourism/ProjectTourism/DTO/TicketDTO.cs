﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using System.Windows.Media;

namespace ProjectTourism.DTO
{
    public class TicketDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private Ticket _ticket;

        public TicketDTO(Ticket ticket)
        {
            _ticket = ticket;
        }
        public void ConfirmAttendance(Ticket ticket)
        {
            TicketService ticketService = new TicketService();
            ticket.HasGuestConfirmed = true;
            ticketService.Update(ticket);
        }
        public Ticket UpdateTicketTourAppointmentData(Ticket ticket)
        {
            TourService tourService = new TourService();
            GuideService guideService = new GuideService();
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            LocationService locationService = new LocationService();

            TourAppointment updatedAppointment = tourAppointmentService.GetOne(ticket.TourAppointmentId);
            updatedAppointment.Tickets = new List<Ticket>();
            updatedAppointment.TicketGrades = new List<TicketGrade>();
            updatedAppointment.Tour = tourService.GetOne(updatedAppointment.TourId);
            updatedAppointment.Tour.Location = locationService.GetOne(updatedAppointment.Tour.LocationId);
            updatedAppointment.Tour.Guide = guideService.GetOne(updatedAppointment.Tour.GuideUsername);
            updatedAppointment.Tour.TourAppointments = tourAppointmentService.GetAllByTour(updatedAppointment.TourId);
            updatedAppointment.Tour.StopsList = tourService.LoadStops(updatedAppointment.Tour);

            ticket.TourAppointment = updatedAppointment;
            return ticket;
        }
        public void CreateTicket(Ticket ticket)
        {
            TicketService ticketService = new TicketService();
            ticketService.Add(ticket);
        }
        public TicketDTO GetLast()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAll();
            Ticket last = tickets.Last();
            return new TicketDTO(last);
        }

        public void RemoveLast()
        {
            TicketService ticketService = new TicketService();
            List<Ticket> tickets = ticketService.GetAll();
            Ticket last = tickets.Last();
            ticketService.Delete(last);
        }
        public Ticket GetTicket()
        {
            return _ticket;
        }
        private SolidColorBrush SetButtonColor()
        {
            if (_ticket.HasGuestConfirmed)
                return Brushes.Green;
            else if (HasGuideChecked)
                return Brushes.IndianRed;
            return Brushes.Transparent;
        }

        public SolidColorBrush ButtonColor
        {
            get => SetButtonColor();
            set
            {
                OnPropertyChanged();
            }
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

        public TourAppointmentDTO TourAppointment
        {
            get => new TourAppointmentDTO(_ticket.TourAppointment);
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
        public Guest2DTO Guest2
        {
            get => new Guest2DTO(_ticket.Guest2);
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
        public TicketGradeDTO TicketGrade
        {
            get => new TicketGradeDTO(_ticket.TicketGrade);
            set
            {
                if (value.GetTicketGrade() != _ticket.TicketGrade)
                {
                    _ticket.TicketGrade = value.GetTicketGrade();
                    OnPropertyChanged();
                }
            }
        }
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "NumberOfGuests")
                {
                    if (string.IsNullOrEmpty(NumberOfGuests.ToString()))
                        return "Number Of Tickets is required!";
                    if (NumberOfGuests < 1)
                        return "Number Of Tickets has to be at least 1!";
                }
                else if (columnName == "TourStop")
                {
                    if (string.IsNullOrEmpty(TourStop))
                        return "Your starting point is required!";
                }
                else if (columnName == "TourAppointment")
                {
                    if (string.IsNullOrEmpty(TourStop))
                        return "Date is required!";
                }

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "NumberOfGuests", "TourStop" , "TourAppointment" };

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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
