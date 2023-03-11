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
        private string? _Name;
        public string? Name
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

        private int? _LocationId;
        public int? LocationId
        {
            get => _LocationId;
            set
            {
                if (_LocationId != value)
                {
                    _LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        private Location? _Location;
        public Location? Location
        {
            get => _Location;
            set
            {
                if (_Location != value)
                {
                    _Location = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _Description;
        public string? Description
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

        private string? _Language;
        public string? Language
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

        private int? _MaxNumberOfGuests;
        public int? MaxNumberOfGuests
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

        private string? _Stops;
        public string? Stops
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

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get => _StartDate;
            set
            {
                _StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private double? _Duration;
        public double? Duration
        {
            get => _Duration;
            set
            {
                _Duration = value;
                OnPropertyChanged();
            }
        }

        private string? _Images;
        public string? Images
        {
            get => _Images;
            set
            {
                _Images = value;
                OnPropertyChanged();
            }
        }

        private string? _GuideUsername;
        public string? GuideUsername
        {
            get => _GuideUsername;
            set
            {
                _GuideUsername = value;
                OnPropertyChanged();
            }
        }

        private Guide? _Guide;
        public Guide? Guide
        {
            get => _Guide;
            set
            {
                _Guide = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Route() { }

        public Route(string name, Location location, string description, string language, int maxNumberOfGuests, string stops, DateTime startDate, double duration, string images, string guideUsername)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberOfGuests = maxNumberOfGuests;
            Stops = stops;
            StartDate = startDate;
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
            Id = int.Parse(values[1]);
            Description = values[2];
            Language = values[3];
            MaxNumberOfGuests = int.Parse(values[4]);
            Stops = values[5];
            StartDate = DateTime.Parse(values[6]);
            Duration = double.Parse(values[7]);
            Images = values[8];
            GuideUsername = values[9];
            LocationId = int.Parse(values[10]);
            Location = FindLocation(LocationId);
        }

        private Location? FindLocation(int? locationId)
        {
            LocationDAO locationDAO = new LocationDAO();
            return locationDAO.GetOne((int)locationId);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Name,
                Id.ToString(),
                Description,
                Language,
                MaxNumberOfGuests.ToString(),
                Stops,
                StartDate.ToString("dd.MM.yyyy HH:mm"),
                Duration.ToString(),
                Images,
                GuideUsername,
                LocationId.ToString()
            };
            return csvValues;
        }
    }
}
