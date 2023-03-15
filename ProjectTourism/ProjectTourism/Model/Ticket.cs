using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        private bool _HasGuideChecked;
        public bool HasGuideChecked
        {
            get => _HasGuideChecked;
            set
            {
                if (value != _HasGuideChecked)
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
                if (value != _HasGuestConfirmed)
                {
                    _HasGuestConfirmed = value;
                    OnPropertyChanged();
                }
            }
        }
        public Ticket()
        {  }

        public Ticket(int id, int routeId, string RouteStop, string guest2Username, int numberOfGuests)
        {
            Id = id;
            RouteId = routeId;
            Route = FindRoute(routeId);
            this.RouteStop = RouteStop;
            Guest2Username = guest2Username;
            Guest2 = FindGuest2(guest2Username);
            NumberOfGuests = numberOfGuests;
            HasGuideChecked = false;
            HasGuestConfirmed = false;
        }

        public Ticket(int routeId, string RouteStop, string guest2Username, int numberOfGuests)
        {            
            RouteId = routeId;
            Route = FindRoute(routeId);
            this.RouteStop = RouteStop;
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
        public Route FindRoute(int id)
        {
            RouteDAO routeDAO = new RouteDAO();
            return routeDAO.GetOne(id);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                RouteId.ToString(),
                RouteStop,
                Guest2Username,
                NumberOfGuests.ToString(),
                HasGuideChecked.ToString(),
                HasGuestConfirmed.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            RouteId = int.Parse(values[1]);
            Route = FindRoute(RouteId);
            RouteStop = values[2];
            Guest2Username = values[3];
            Guest2 = FindGuest2(Guest2Username);
            NumberOfGuests = int.Parse(values[4]);
            HasGuideChecked = bool.Parse(values[5]);
            HasGuestConfirmed = bool.Parse(values[6]);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
