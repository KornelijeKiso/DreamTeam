using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectTourism.Model
{
    public class Ticket: Serializable, INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged();
                }
            }
        }
        /*
        private SolidColorBrush _ButtonColor;
        public SolidColorBrush ButtonColor
        {
            get => _ButtonColor;
            set
            {
                if (value != _ButtonColor)
                {
                    _ButtonColor = value;
                    OnPropertyChanged();
                }
            }
        }
        */
        private int _RouteId;
        public int RouteId
        {
            get => _RouteId;
            set
            {
                if (value != _RouteId)
                {
                    _RouteId = value;
                    OnPropertyChanged();
                }
            }
        }

        private Route _Route;
        public Route Route
        {
            get => _Route;
            set
            {
                if (value != _Route)
                {
                    _Route = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _TourAppointmentId;
        public int TourAppointmentId
        {
            get => _TourAppointmentId;
            set
            {
                if (value != _TourAppointmentId)
                {
                    _TourAppointmentId = value;
                    OnPropertyChanged();
                }
            }
        }

        private TourAppointment _TourAppointment;
        public TourAppointment TourAppointment
        {
            get => _TourAppointment;
            set
            {
                if (value != _TourAppointment)
                {
                    _TourAppointment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _RouteStop;
        public string RouteStop
        {
            get => _RouteStop;
            set
            {
                if (value != _RouteStop)
                {
                    _RouteStop = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Guest2Username;
        public string Guest2Username
        {
            get => _Guest2Username;
            set
            {
                if (value != _Guest2Username)
                {
                    _Guest2Username = value;
                    OnPropertyChanged();
                }
            }
        }

        private Guest2 _Guest2;
        public Guest2 Guest2

        {
            get => _Guest2;
            set
            {
                if (value != _Guest2)
                {
                    _Guest2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _numberOfGuests;
        public int NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                if (value != _numberOfGuests)
                {
                    _numberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        public Ticket()
        {  }

        public Ticket(int id, int tourAppId, string RouteStop, string guest2Username, int numberOfGuests)
        {
            Id = id;
            TourAppointmentId = tourAppId;
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            TourAppointment = tourAppointmentDAO.GetOne(tourAppId);
            this.RouteStop = RouteStop;
            Guest2Username = guest2Username;
            Guest2 = FindGuest2(guest2Username);
            NumberOfGuests = numberOfGuests;
        }

        public Ticket(int tourAppId, string RouteStop, string guest2Username, int numberOfGuests)
        {
            TourAppointmentId = tourAppId;
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            TourAppointment = tourAppointmentDAO.GetOne(tourAppId);
            this.RouteStop = RouteStop;
            Guest2Username = guest2Username;
            Guest2 = FindGuest2(guest2Username);
            NumberOfGuests = numberOfGuests;
        }


        public Guest2 FindGuest2(string username)
        {
            Guest2DAO guest2DAO = new Guest2DAO();
            return guest2DAO.GetOne(username);
        }
        public TourAppointment FindTourAppointment(int id)
        {
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            return tourAppointmentDAO.GetOne(id);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourAppointmentId.ToString(),
                Guest2Username,
                NumberOfGuests.ToString(),
                RouteStop
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourAppointmentId = int.Parse(values[1]);
            TourAppointment = FindTourAppointment(TourAppointmentId);
            Guest2Username = values[2];
            NumberOfGuests = int.Parse(values[3]);
            RouteStop = values[4];
            Guest2 = FindGuest2(Guest2Username);            
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
