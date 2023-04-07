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
        public string? Name;
        public int? LocationId;
        public Location? Location;
        public string? Description;
        public string? Language;
        public int MaxNumberOfGuests;
        public string? Stops;
        public string? Start;
        public string? Finish;
        public DateTime StartDate;
        public List<DateTime> dates { get; set; }
        public double? Duration;
        public string? PictureURLs;
        public string[] Pictures;
        public string? GuideUsername;
        public List<string> StopsList { get; set; }
        public Guide? Guide;
        

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
