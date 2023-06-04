using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.Services;

public enum TOURSTATE { READY, STARTED, FINISHED, STOPPED, CANCELED, EXPIRED };

namespace ProjectTourism.DTO
{
    public class TourAppointmentDTO : INotifyPropertyChanged
    {
        private TourAppointment _tourAppointment;
        public TourAppointmentDTO(TourAppointment tourAppointment)
        {
            _tourAppointment = tourAppointment;
            Tickets = new ObservableCollection<TicketDTO>(_tourAppointment.Tickets.Select(r => new TicketDTO(r)).ToList());
            TicketGrades = new ObservableCollection<TicketGradeDTO>(_tourAppointment.TicketGrades.Select(r => new TicketGradeDTO(r)).ToList());
        }
        public TourAppointmentDTO(Tour tour, DateTime date)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            _tourAppointment = tourAppointmentService.GetByDate(tour.Id, date);
            Synchronize();
            Tickets = new ObservableCollection<TicketDTO>(_tourAppointment.Tickets.Select(r => new TicketDTO(r)).ToList());
        }
        private void Synchronize()
        {
            TourService tourService = new TourService();
            _tourAppointment.Tour = tourService.GetOne(_tourAppointment.TourId);
        }

        public void UpdateTourAppointmentDTO(TourAppointmentDTO tourAppointmentDTO)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            tourAppointmentService.Update(tourAppointmentDTO.GetTourAppointment());
        }
        public TourAppointment GetTourAppointment()
        {
            return _tourAppointment;
        }

        private bool AreThereAnyReviews()
        {
            foreach (var ticket in _tourAppointment.Tickets)
            {
                if (ticket.TicketGrade != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsNotFinished { get => _tourAppointment.State != TOURSTATE.FINISHED && _tourAppointment.State != TOURSTATE.STOPPED; }
        public bool IsFinished { get => _tourAppointment.State == TOURSTATE.FINISHED || _tourAppointment.State == TOURSTATE.STOPPED; }
        public bool IsReviewVisible { get => ((_tourAppointment.State == TOURSTATE.FINISHED || _tourAppointment.State == TOURSTATE.STOPPED) && AreThereAnyReviews()); }
        public int Visits { get => _tourAppointment.Tickets.Count; }
        public bool AppointmentNotVisited { get => Visits == 0; }
        public int Id
        {
            get => _tourAppointment.Id;
            set
            {
                if (_tourAppointment.Id != value)
                {
                    _tourAppointment.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime TourDateTime
        {
            get => _tourAppointment.TourDateTime;
            set
            {
                if (_tourAppointment.TourDateTime != value)
                {
                    _tourAppointment.TourDateTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CurrentTourStop
        {
            get => _tourAppointment.CurrentTourStop;
            set
            {
                if (value != _tourAppointment.CurrentTourStop)
                {
                    _tourAppointment.CurrentTourStop = value;
                    OnPropertyChanged();
                }
            }
        }
        public int TourId
        {
            get => _tourAppointment.TourId;
            set
            {
                if (_tourAppointment.TourId != value)
                {
                    _tourAppointment.TourId = value;
                    OnPropertyChanged();
                }
            }
        }
        public TourDTO Tour
        {
            get => new TourDTO(_tourAppointment.Tour);
            set
            {
                if (value.GetTour() != _tourAppointment.Tour)
                {
                    _tourAppointment.Tour = value.GetTour();
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TicketDTO> Tickets;
        public ObservableCollection<TicketGradeDTO> TicketGrades;
        public TOURSTATE State
        {
            get => _tourAppointment.State;
            set
            {
                if (_tourAppointment.State != value)
                {
                    _tourAppointment.State = value;
                    OnPropertyChanged();
                }
            }
        }


        public int AvailableSeats
        {
            get => GetAvailableSeats();
        }
        public int GetAvailableSeats()
        {
            int availableSeats = _tourAppointment.Tour.MaxNumberOfGuests;
            foreach (var ticket in Tickets)
            {
                availableSeats = availableSeats - ticket.NumberOfGuests;
            }
            return availableSeats;
        }
        public bool IsAvailable 
        {
            get => ((AvailableSeats > 0) && (_tourAppointment.State == TOURSTATE.READY) && (_tourAppointment.TourDateTime >= DateTime.Now)); 
        }
        public bool CanBeCanceled { get => _tourAppointment.TourDateTime > DateTime.Now.AddHours(48); }
        public bool AreThereAnyTickets { get => _tourAppointment.Tickets.Any(); }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
