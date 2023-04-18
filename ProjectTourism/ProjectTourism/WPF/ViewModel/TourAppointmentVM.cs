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

public enum TOURSTATE { READY, STARTED, FINISHED, STOPPED };

namespace ProjectTourism.WPF.ViewModel
{
    public class TourAppointmentVM: INotifyPropertyChanged
    {
        private TourAppointment _tourAppointment;
        public TourAppointmentVM(TourAppointment tourAppointment)
        {
            _tourAppointment = tourAppointment;
            //Synchronize();
            Tickets = new ObservableCollection<TicketVM>(_tourAppointment.Tickets.Select(r => new TicketVM(r)).ToList());
            TicketGrades = new ObservableCollection<TicketGradeVM>(_tourAppointment.TicketGrades.Select(r => new TicketGradeVM(r)).ToList());
        }

        private void Synchronize()
        {
            TourService tourService = new TourService(new TourRepository());
            _tourAppointment.Tour = tourService.GetOne(_tourAppointment.TourId);

            //TicketService ticketService = new TicketService(new TicketRepository());
            //_tourAppointment.Tickets = ticketService.GetByAppointment(_tourAppointment.Id);

            //if (_tourAppointment.Tickets.Count > 0)
            //{
            //    TicketGradeService ticketGradeService = new TicketGradeService(new TicketGradeRepository());
            //    _tourAppointment.TicketGrades = ticketGradeService.GetAllByTourAppointment(_tourAppointment);
            //}
        }

        public TourAppointmentVM(Tour tour, DateTime date)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            _tourAppointment = tourAppointmentService.GetByDate(tour.Id, date);
            Synchronize();
            Tickets = new ObservableCollection<TicketVM>(_tourAppointment.Tickets.Select(r => new TicketVM(r)).ToList());
        }

        public void UpdateTourAppointmentVM(TourAppointmentVM tourAppointmentVM)
        {
            TourAppointmentService tourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());
            tourAppointmentService.Update(tourAppointmentVM.GetTourAppointment());
        }
        public TourAppointment GetTourAppointment()
        {
            return _tourAppointment;
        }
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
        public int Visits
        {
            get => _tourAppointment.Visits;
            set
            {
                if (value != _tourAppointment.Visits)
                {
                    _tourAppointment.Visits = value;
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
        public bool IsNotFinished
        {
            get => _tourAppointment.IsNotFinished;
            set
            {
                if (_tourAppointment.IsNotFinished != value)
                {
                    _tourAppointment.IsNotFinished = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsFinished
        {
            get => _tourAppointment.IsFinished;
            set
            {
                if (_tourAppointment.IsFinished != value)
                {
                    _tourAppointment.IsFinished = value;
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
