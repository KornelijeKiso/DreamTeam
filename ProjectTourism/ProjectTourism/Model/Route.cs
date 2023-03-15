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
using ProjectTourism.ModelDAO;

public enum ROUTESTATE {STARTED, FINISHED, STOPPED};

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

        private ROUTESTATE _State;
        public ROUTESTATE State
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

        private string? _Start;
        public string? Start
        {
            get => _Start;
            set
            {
                if (_Start != value)
                {
                    _Start = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _Finish;
        public string? Finish
        {
            get => _Finish;
            set
            {
                if (_Finish != value)
                {
                    _Finish = value;
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

        public List<string> StopsList { get; set; }

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

        private int? _AvailableSeats;
        public int? AvailableSeats
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Route()
        {
            StopsList = new List<string>();
        }

        public Route(string name, Location location, string description, string language, int maxNumberOfGuests, string start, string stops, string finish, DateTime startDate, double duration, string images, string guideUsername)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberOfGuests = maxNumberOfGuests;
            Start = start;
            Stops = stops;
            Finish = finish;
            StartDate = startDate;
            Duration = duration;
            Images = images;
            GuideUsername = guideUsername;
            Guide = FindGuide(guideUsername);
            StopsList = new List<string>();
            AvailableSeats = maxNumberOfGuests;
        }
        public Route(int id, string name, Location location, string description, string language, int maxNumberOfGuests, string start, string stops, string finish, DateTime startDate, double duration, string images, string guideUsername)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberOfGuests = maxNumberOfGuests;
            Start = start;
            Stops = stops;
            Finish = finish;
            StartDate = startDate;
            Duration = duration;
            Images = images;
            GuideUsername = guideUsername;
            Guide = FindGuide(guideUsername);
            AvailableSeats = maxNumberOfGuests;
            StopsList = new List<string>();
        }

        public Guide? FindGuide(string username)
        {
            GuideDAO guideDAO = new GuideDAO();
            return guideDAO.GetOne(username);
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            Language = values[3];
            MaxNumberOfGuests = int.Parse(values[4]);
            Start = values[5];
            Stops = values[6];
            Finish = values[7];
            if (DateTime.TryParse(values[8], new CultureInfo("en-GB"), DateTimeStyles.None, out var startDate)) StartDate = startDate;
            Duration = double.Parse(values[9]);
            Images = values[10];
            GuideUsername = values[11];
            LocationId = int.Parse(values[12]);
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
                Id.ToString(),
                Name,
                Description,
                Language,
                MaxNumberOfGuests.ToString(),
                Start,
                Stops,
                Finish,
                StartDate.ToString("dd.MM.yyyy HH:mm"),
                Duration.ToString(),
                Images,
                GuideUsername,
                LocationId.ToString()
            };
            return csvValues;
        }

        private Regex _NameRegex = new Regex("[A-Z a-z]+");
        //private Regex _MaxNumberOfGuestsRegex = new Regex("0|^[0-9]+$");
        //private Regex _StartRegex = new Regex("^.*[a-zA-Z]+.*$");
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";
                    Match match1 = _NameRegex.Match(Name);
                    if (!match1.Success)
                        return "Enter name";
                }
              
                else
                {
                    return "Error";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Name" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
    }
}
