using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ProjectTourism.Localization;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.ViewModel
{
    public class TourVM : INotifyPropertyChanged, IDataErrorInfo
    {
        private Tour _tour;

        public Tour GetTour() { return _tour; }
        public TourVM(Tour tour)
        {
            _tour = tour;
            TourAppointments = new ObservableCollection<TourAppointmentVM>(_tour.TourAppointments.Select(r => new TourAppointmentVM(r)).ToList());
        }
        public ObservableCollection<TourAppointmentVM> TourAppointments { get; set; }
        public string[] GetPictureURLsFromCSV()
        {
            if(PictureURLs != null)
            {
                string[] pictures = PictureURLs.Split(',');
                foreach (var picture in pictures)
                {
                    picture.Trim();
                }
                return pictures;
            }
            return null;
        }

        public List<DateTime> dates
        {
            get => _tour.dates;
            set
            {
                if(value != _tour.dates)
                {
                    _tour.dates = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<string> StopsList
        {
            get => _tour.StopsList;
            set
            {
                if(value != _tour.StopsList)
                {
                    _tour.StopsList = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id
        {
            get => _tour.Id;
            set
            {
                if (value != _tour.Id)
                {
                    _tour.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Name
        {
            get => _tour.Name;
            set
            {
                if (_tour.Name != value)
                {
                    _tour.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId
        {
            get => _tour.LocationId;
            set
            {
                if (_tour.LocationId != value)
                {
                    _tour.LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public LocationVM? Location
        {
            get => new LocationVM(_tour.Location);
            set
            {
                if (_tour.Location != value.GetLocation())
                {
                    _tour.Location = value.GetLocation();
                    OnPropertyChanged();
                }
            }
        }
        public string? Description
        {
            get => _tour.Description;
            set
            {
                if (_tour.Description != value)
                {
                    _tour.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Language
        {
            get => _tour.Language;
            set
            {
                if (_tour.Language != value)
                {
                    _tour.Language = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MaxNumberOfGuests
        {
            get => _tour.MaxNumberOfGuests;
            set
            {
                if (_tour.MaxNumberOfGuests != value)
                {
                    _tour.MaxNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Stops
        {
            get => _tour.Stops;
            set
            {
                if (_tour.Stops != value)
                {
                    _tour.Stops = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Start
        {
            get => _tour.Start;
            set
            {
                if (_tour.Start != value)
                {
                    _tour.Start = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Finish
        {
            get => _tour.Finish;
            set
            {
                if (_tour.Finish != value)
                {
                    _tour.Finish = value;
                    OnPropertyChanged();
                }
            }
        }
        public double Duration
        {
            get => _tour.Duration;
            set
            {
                _tour.Duration = value;
                OnPropertyChanged();
            }
        }
        public string? PictureURLs
        {
            get => _tour.PictureURLs;
            set
            {
                _tour.PictureURLs = value;
                OnPropertyChanged();
            }
        }
        private string[] _Pictures;
        public string[] Pictures
        {
            get => _Pictures = GetPictureURLsFromCSV();
            set
            {
                if (value != _Pictures)
                {
                    _Pictures = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? GuideUsername
        {
            get => _tour.GuideUsername;
            set
            {
                if (value != _tour.GuideUsername)
                {
                    _tour.GuideUsername = value;
                    OnPropertyChanged();
                }
            }
        }

        public GuideVM Guide
        {
            get => new GuideVM(_tour.Guide);
            set
            {
                if (value.GetGuide() != _tour.Guide)
                {
                    _tour.Guide = value.GetGuide();
                    OnPropertyChanged();
                }
            }
        }
        

        public string Error => null;

        private Regex _NameRegex = new Regex("[A-Z a-z]+");
        private Regex _PositiveNumberRegex = new Regex("0|^[0-9]+$");
        private Regex _StartRegex = new Regex("^.*[a-zA-Z]+.*$");
        private Regex _DateTimeRegex = new Regex("^(0?[1-9]|1[012])\\/(0?[1-9]|[12][0-9]|3[01])\\/\\d{4}\\s(0?[1-9]|1[012]):([0-5][0-9]):([0-5][0-9])\\s(AM|PM)$\r\n");

        public string? this[string columnName]
        {
            get
            {
                string? error = null;

                if (columnName == "Name")
                {
                    error = ValidateRequiredField(Name, "RequiredNameLabel")
                        ?? ValidateRegexMatch(Name, _NameRegex, "EnterNameLabel");
                }
                else if (columnName == "MaxNumberOfGuests")
                {
                    error = ValidateRequiredField(MaxNumberOfGuests.ToString(), "NumberMustBePositive")
                        ?? ValidateRegexMatch(MaxNumberOfGuests.ToString(), _PositiveNumberRegex, "EnterPositiveNumber");
                }
                else if (columnName == "Start")
                {
                    error = ValidateRequiredField(Start, "StartPointRequired")
                        ?? ValidateRegexMatch(Start, _StartRegex, "EnterStart");
                }
                else if (columnName == "Finish")
                {
                    error = ValidateRequiredField(Finish, "FinishRequired")
                        ?? ValidateRegexMatch(Finish, _StartRegex, "EnterFinish");
                }
                else if (columnName == "Duration")
                {
                    error = ValidateRequiredField(Duration.ToString(), "NumberMustBePositive")
                        ?? ValidateRegexMatch(Duration.ToString(), _PositiveNumberRegex, "EnterPositiveNumber");
                }
                else
                {
                    error = GetLocalizedErrorMessage("Error");
                }

                return error;
            }
        }

        string? ValidateRequiredField(string value, string resourceKey)
        {
            if (string.IsNullOrEmpty(value))
            {
                return GetLocalizedErrorMessage(resourceKey);
            }

            return null;
        }

        string? ValidateRegexMatch(string value, Regex regex, string resourceKey)
        {
            Match match = regex.Match(value);
            if (!match.Success)
            {
                return GetLocalizedErrorMessage(resourceKey);
            }

            return null;
        }

        string GetLocalizedErrorMessage(string resourceKey)
        {
            TextBlock label = new TextBlock();
            LocExtension locExtension = new LocExtension(resourceKey);
            BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
            return label.Text;
        }


        private readonly string[] _validatedProperties = {"Name", "MaxNumberOfGuests", "Start", "Finish","Duration" };

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
        public bool ArePicturesNull()
        {
            if (Pictures == null)
                return true;
            if (Pictures.Count() == 1)
            {
                if (Pictures[0].Equals(""))
                    return true;
            }
            return false;
        }
        public bool ArePicturesEmpty { get => ArePicturesNull(); }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
