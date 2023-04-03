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

        private string _TourStop;
        public string TourStop
        {
            get => _TourStop;
            set
            {
                if (value != _TourStop)
                {
                    _TourStop = value;
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

        private bool _HasGuideChecked;
        public bool HasGuideChecked
        {
            get => _HasGuideChecked;
            set
            {
                if (_HasGuideChecked != value)
                {
                    _HasGuideChecked = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _HasGuestConfirmed;
        public bool HasGuestConfirmed
        {
            get => _HasGuestConfirmed;
            set
            {
                if (_HasGuestConfirmed != value)
                {
                    _HasGuestConfirmed = value;
                    OnPropertyChanged();
                }
            }
        }
        public Ticket()
        {  }

        public Ticket(int id, int tourAppId, string tourStop, string guest2Username, int numberOfGuests)
        {
            Id = id;
            TourAppointmentId = tourAppId;
            TourAppointment = FindTourAppointment(tourAppId);
            TourStop = tourStop;
            Guest2Username = guest2Username;
            Guest2 = FindGuest2(guest2Username);
            NumberOfGuests = numberOfGuests;
            HasGuideChecked = false;
            HasGuestConfirmed = false;
        }

        public Ticket(int tourAppId, string tourStop, string guest2Username, int numberOfGuests)
        {
            TourAppointmentId = tourAppId;
            TourAppointment = FindTourAppointment(tourAppId);
            TourStop = tourStop;
            Guest2Username = guest2Username;
            Guest2 = FindGuest2(guest2Username);
            NumberOfGuests = numberOfGuests;
            HasGuideChecked = false;
            HasGuestConfirmed = false;
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
        private void AddTicketToAppointment(Ticket ticket)  // possible problem
        {
            ticket.TourAppointment.Tickets.Add(ticket);
            if (ticket.TourAppointment.CurrentTourStop.Equals(ticket.TourStop))
                ticket.HasGuideChecked = true;
            else ticket.HasGuideChecked = false; 
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourAppointmentId.ToString(),
                Guest2Username,
                NumberOfGuests.ToString(),
                TourStop,
                HasGuideChecked.ToString(),
                HasGuestConfirmed.ToString()
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
            TourStop = values[4];
            HasGuideChecked = bool.Parse(values[5]);
            HasGuestConfirmed= bool.Parse(values[6]);

            if (HasGuestConfirmed)
                ButtonColor = Brushes.Green;
            else if (HasGuideChecked)
                ButtonColor = Brushes.IndianRed;
            Guest2 = FindGuest2(Guest2Username);
            //AddTicketToAppointment(this);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
