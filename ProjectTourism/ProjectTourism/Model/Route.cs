using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ProjectTourism.ModelDAO;

namespace ProjectTourism.Model
{
    public class Route : Serializable, INotifyPropertyChanged
    {
        public int Id;
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _RouteLocationId;
        public int RouteLocationId
        {
            get => _RouteLocationId;
            set
            {
                if (_RouteLocationId != value)
                {
                    _RouteLocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        private RouteLocation _RouteLocation;
        public RouteLocation RouteLocation
        {
            get => _RouteLocation;
            set
            {
                if (_RouteLocation != value)
                {
                    _RouteLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Description;
        public string Description
        {
            get => _Description;
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Language;
        public string Language
        {
            get => _Language;
            set
            {
                if (_Language != value)
                {
                    _Language = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _MaxNumberOfGuests;
        public int MaxNumberOfGuests
        {
            get => _MaxNumberOfGuests;
            set
            {
                if (_MaxNumberOfGuests != value)
                {
                    _MaxNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Stops;
        public string Stops
        {
            get => _Stops;
            set
            {
                if (_Stops != value)
                {
                    _Stops = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _StartTime;
        public DateTime StartTime
        {
            get => _StartTime;
            set
            {
                _StartTime = value;
                OnPropertyChanged();
            }
        }

        private double _Duration;
        public double Duration
        {
            get => _Duration;
            set
            {
                _Duration = value;
                OnPropertyChanged();
            }
        }

        private List<Image> _Images;
        public List<Image> Images
        {
            get => _Images;
            set
            {
                _Images = value;
                OnPropertyChanged();
            }
        }

        private string _GuideUsername;
        public string GuideUsername
        {
            get => _GuideUsername;
            set
            {
                _GuideUsername = value;
                OnPropertyChanged();
            }
        }

        private Guide _Guide;
        public Guide Guide
        {
            get => _Guide;
            set
            {
                _Guide = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Route()
        {
            Images = new List<Image>();
        }

        public Route(string name, RouteLocation routeLocation, string description, string language, int maxNumberOfGuests, string stops, DateTime startTime, double duration, List<Image> images, string guideUsername)
        {
            Name = name;
            RouteLocation = routeLocation;
            Description = description;
            Language = language;
            MaxNumberOfGuests = maxNumberOfGuests;
            Stops = stops;
            StartTime = startTime;
            Duration = duration;
            Images = images;
            GuideUsername = guideUsername;
            Guide = FindGuide(guideUsername);
        }

        public Guide? FindGuide(string username)
        {
            GuideDAO guideDAO = new GuideDAO();
            return guideDAO.GetOne(username);
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            RouteLocation.Id = int.Parse(values[1]);
            Description = values[2];
            Language = values[3];
            MaxNumberOfGuests = int.Parse(values[4]);
            Stops = values[5];
            StartTime = DateTime.Parse(values[6]);
            Duration = int.Parse(values[7]);
            GuideUsername = values[8];
            RouteLocationId= int.Parse(values[9]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Name,
                RouteLocation.Id.ToString(),
                Description,
                Language,
                MaxNumberOfGuests.ToString(),
                Stops,
                StartTime.ToString(),
                Duration.ToString(),
                GuideUsername,
                RouteLocationId.ToString()
            };
            return csvValues;
        }
    }
}
