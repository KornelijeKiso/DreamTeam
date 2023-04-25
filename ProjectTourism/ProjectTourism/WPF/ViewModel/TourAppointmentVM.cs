using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;

public enum TOURSTATE { READY, STARTED, FINISHED, STOPPED, CANCELED };

namespace ProjectTourism.WPF.ViewModel
{
    public class TourAppointmentVM: INotifyPropertyChanged
    {
        private TourAppointment _tourAppointment;
        public TourAppointmentVM(TourAppointment tourAppointment)
        {
            _tourAppointment = tourAppointment;
            Tickets = new ObservableCollection<TicketVM>(_tourAppointment.Tickets.Select(r => new TicketVM(r)).ToList());
            TicketGrades = new ObservableCollection<TicketGradeVM>(_tourAppointment.TicketGrades.Select(r => new TicketGradeVM(r)).ToList());
        }

        private void Synchronize()
        {
            TourService tourService = new TourService();
            _tourAppointment.Tour = tourService.GetOne(_tourAppointment.TourId);
        }

        public TourAppointmentVM(Tour tour, DateTime date)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            _tourAppointment = tourAppointmentService.GetByDate(tour.Id, date);
            Synchronize();
            Tickets = new ObservableCollection<TicketVM>(_tourAppointment.Tickets.Select(r => new TicketVM(r)).ToList());
        }

        public void UpdateTourAppointmentVM(TourAppointmentVM tourAppointmentVM)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService();
            tourAppointmentService.Update(tourAppointmentVM.GetTourAppointment());
        }
        public TourAppointment GetTourAppointment()
        {
            return _tourAppointment;
        }

        private bool AreThereAnyReviews()
        {
            foreach(var ticket in _tourAppointment.Tickets)
            {
                if(ticket.TicketGrade != null)
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
        public TourVM Tour
        {
            get => new TourVM(_tourAppointment.Tour);
            set
            {
                if (value.GetTour() != _tourAppointment.Tour)
                {
                    _tourAppointment.Tour = value.GetTour();
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TicketVM> Tickets;
        public ObservableCollection<TicketGradeVM> TicketGrades;
        public int AvailableSeats
        {
            get => _tourAppointment.AvailableSeats;
            set
            {
                if (_tourAppointment.AvailableSeats != value)
                {
                    _tourAppointment.AvailableSeats = value;
                    OnPropertyChanged();
                }
            }
        }
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

        public bool CanBeCanceled { get => _tourAppointment.TourDateTime > DateTime.Now.AddHours(48); }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
