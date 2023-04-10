using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

public enum TOURSTATE { READY, STARTED, FINISHED, STOPPED };

namespace ProjectTourism.WPF.ViewModel
{
    public class TourAppointmentVM: INotifyPropertyChanged //IDataErrorInfo
    {
        private TourAppointment _tourAppointment;
        public TourAppointmentVM(TourAppointment tourAppointment)
        {
            _tourAppointment = tourAppointment;
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
        public List<Ticket> Tickets
        {
            get => _tourAppointment.Tickets;
            set
            {
                if (_tourAppointment.Tickets != value)
                {
                    _tourAppointment.Tickets = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
