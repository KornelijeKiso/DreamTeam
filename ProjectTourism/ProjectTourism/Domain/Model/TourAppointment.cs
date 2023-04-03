using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProjectTourism.ModelDAO;

public enum TOURSTATE { READY, STARTED, FINISHED, STOPPED };

namespace ProjectTourism.Model
{
    public class TourAppointment: Serializable, INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
            }
        }

        private DateTime _TourDateTime;
        public DateTime TourDateTime
        {
            get => _TourDateTime;
            set
            {
                if (_TourDateTime != value)
                {
                    _TourDateTime = value;
                }
            }
        }

        private string _CurrentTourStop;
        public string CurrentTourStop
        {
            get => _CurrentTourStop;
            set
            {
                if (value != _CurrentTourStop)
                {
                    _CurrentTourStop = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _TourId;
        public int TourId
        {
            get => _TourId;
            set
            {
                if (_TourId != value)
                {
                    _TourId = value;
                    OnPropertyChanged();
                }
            }
        }

        private Tour _Tour;
        public Tour Tour
        {
            get => _Tour;
            set
            {
                if (_Tour != value)
                {
                    _Tour = value;
                    OnPropertyChanged();
                }
            }
        }

        
        private List<Ticket> _Tickets;
        public List<Ticket> Tickets
        {
            get => _Tickets;
            set
            {
                if (_Tickets != value)
                {
                    _Tickets = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _AvailableSeats;
        public int AvailableSeats
        {
            get => _AvailableSeats;
            set
            {
                if (_AvailableSeats != value)
                {
                    _AvailableSeats = value;
                    OnPropertyChanged();
                }
            }
        }
        private TOURSTATE _State;
        public TOURSTATE State
        {
            get => _State;
            set
            {
                if (_State != value)
                {
                    _State = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _IsNotFinished;
        public bool IsNotFinished
        {
            get => _IsNotFinished;
            set
            {
                if (_IsNotFinished != value)
                {
                    _IsNotFinished = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourAppointment()
        { 
            Tickets = new List<Ticket>();
        }

        public TourAppointment(DateTime tourDateTime, int tourId, Tour route)
        {
            TourDateTime = tourDateTime;
            TourId = tourId;
            Tour = route;

            CurrentTourStop = Tour.Start;
            AvailableSeats = Tour.MaxNumberOfGuests;
            IsNotFinished = true;
            State = TOURSTATE.READY;
            Tickets = new List<Ticket>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                TourDateTime.ToString("dd.MM.yyyy HH:mm"),
                CurrentTourStop.ToString(),
                AvailableSeats.ToString(),
                State.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            if (DateTime.TryParse(values[2], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                TourDateTime = dateTimeParsed;
            CurrentTourStop = values[3];
            AvailableSeats = Convert.ToInt32(values[4]);
            switch (values[5])
            {
                case "READY":
                    { State = TOURSTATE.READY; IsNotFinished = true; break; }
                case "STARTED":
                    { State = TOURSTATE.STARTED; IsNotFinished = true; break; }
                case "FINISHED":
                    { State = TOURSTATE.FINISHED; IsNotFinished = false; break; }
                case "STOPPED":
                    { State = TOURSTATE.STOPPED; IsNotFinished = false; break; }
                default:
                    { State = TOURSTATE.READY; IsNotFinished = true; break; }
            }

            TourDAO routeDAO = new TourDAO();
            Tour = routeDAO.GetOne(TourId);
        }
    }
}
