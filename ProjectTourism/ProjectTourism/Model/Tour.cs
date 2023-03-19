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


namespace ProjectTourism.Model
{
    public class Tour : Serializable, INotifyPropertyChanged, IDataErrorInfo
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
        public List<DateTime> dates { get; set; }

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

        private string? _PictureURLs;
        public string? PictureURLs
        {
            get => _PictureURLs;
            set
            {
                _PictureURLs = value;
                OnPropertyChanged();
            }
        }

        private string[] _Pictures;
        public string[] Pictures
        {
            get => _Pictures;
            set
            {
                if (value != _Pictures)
                {
                    _Pictures = value;
                    OnPropertyChanged();
                }
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Tour()
        {
            StopsList = new List<string>();

        }


        public Tour(string name, Location location, string description, string language, int maxNumberOfGuests, string start, string stops, string finish, DateTime startDate, double duration, string images, string guideUsername)
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
            PictureURLs = images;
            GuideUsername = guideUsername;
            Guide = FindGuide(guideUsername);
            StopsList = new List<string>();

            dates = new List<DateTime>();           
        }
        public Tour(int id, string name, Location location, string description, string language, int maxNumberOfGuests, string start, string stops, string finish, DateTime startDate, double duration, string images, string guideUsername)
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
            PictureURLs = images;
            GuideUsername = guideUsername;
            Guide = FindGuide(guideUsername);
            Pictures = GetPictureURLsFromCSV();
            StopsList = new List<string>();

            dates = new List<DateTime>();
        }

        public Guide? FindGuide(string username)
        {
            GuideDAO guideDAO = new GuideDAO();
            return guideDAO.GetOne(username);
        }

        public string[] GetPictureURLsFromCSV()
        {
            string[] pictures = PictureURLs.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
        }

        public List<DateTime> FromString(string dateString)
        {
            List<DateTime> dates = new List<DateTime>();
            string[] oneDate = dateString.Split(',');
            foreach (var date in oneDate)
            {
                //if (DateTime.TryParse(date.Trim(), CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out var dateTimeParsed))
                    if (DateTime.TryParse(date.Trim(), new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                    dates.Add(dateTimeParsed);
            }
            return dates;
        }
        public string ToString(List<DateTime> dates)
        {
            string dateString = "";
            int i;
            for (i = 0; i < dates.Count(); i++)
            {
                //dateString += dates[i].ToString("MM/dd/yyyy hh\\:mm") + ",";          //dateString += dates[i].ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern) + ",";
                dateString += dates[i].ToString("dd.MM.yyyy HH:mm") + ",";
            }
            return dateString;
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
            //if (DateTime.TryParse(values[8], new CultureInfo("en-GB"), DateTimeStyles.None, out var startDate)) StartDate = startDate;
            //if (DateTime.TryParse(values[8], CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out var startDate)) StartDate = startDate;
            dates = FromString(values[8]);
            Duration = double.Parse(values[9]);
            PictureURLs = values[10];
            GuideUsername = values[11];
            LocationId = int.Parse(values[12]);
            Guide = FindGuide(GuideUsername);
            Location = FindLocation(LocationId);
            Pictures = GetPictureURLsFromCSV();
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
                ToString(dates),
                Duration.ToString(),
                PictureURLs,
                GuideUsername,
                LocationId.ToString()
            };
            return csvValues;
        }

        private Regex _NameRegex = new Regex("[A-Z a-z]+");
        private Regex _PositiveNumberRegex = new Regex("0|^[0-9]+$");
        private Regex _StartRegex = new Regex("^.*[a-zA-Z]+.*$");
        private Regex _DateTimeRegex = new Regex("^(0?[1-9]|1[012])\\/(0?[1-9]|[12][0-9]|3[01])\\/\\d{4}\\s(0?[1-9]|1[012]):([0-5][0-9]):([0-5][0-9])\\s(AM|PM)$\r\n");

        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required!";
                    Match match = _NameRegex.Match(Name);
                    if (!match.Success)
                        return "Enter name";
                }
                else if (columnName == "MaxNumberOfGuests")
                {
                    if (string.IsNullOrEmpty(MaxNumberOfGuests.ToString()))
                        return "Number must be positive!";
                    Match match = _PositiveNumberRegex.Match(MaxNumberOfGuests.ToString());
                    if (!match.Success)
                        return "Enter positive number";
                }
                else if (columnName == "Start")
                {
                    if (string.IsNullOrEmpty(Start))
                        return "Start point required!";
                    Match match = _StartRegex.Match(Start);
                    if (!match.Success)
                        return "Enter start";
                }
                else if (columnName == "Finish")
                {
                    if (string.IsNullOrEmpty(Finish))
                        return "Finish point is required!";
                    Match match = _StartRegex.Match(Finish);
                    if (!match.Success)
                        return "Enter finish";
                }
                else if (columnName == "StartDate")
                {
                    if (string.IsNullOrEmpty(StartDate.ToString("MM/dd/yyyy HH:mm:ss")))
                        return "Start date is required!";
                    Match match = _DateTimeRegex.Match(StartDate.ToString("MM/dd/yyyy HH:mm:ss"));
                    //if (!match.Success)
                    //    return "Enter start date";
                }
                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration.ToString()))
                        return "Number must be positive!";
                    Match match = _PositiveNumberRegex.Match(Duration.ToString());
                    if (!match.Success)
                        return "Enter positive number";
                }
                else
                {
                    return "Error";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Name", "MaxNumberOfGuests", "Start", "Finish", "StartDate", "Duration" };

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
