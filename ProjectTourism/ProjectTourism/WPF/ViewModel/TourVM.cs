﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ProjectTourism.Localization;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;

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
        public string[] Pictures
        {
            get => _tour.Pictures;
            set
            {
                if (value != _tour.Pictures)
                {
                    _tour.Pictures = value;
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
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("RequiredNameLabel");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                        
                    Match match = _NameRegex.Match(Name);
                    if (!match.Success)
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("EnterNameLabel");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                }
                else if (columnName == "MaxNumberOfGuests")
                {
                    if (string.IsNullOrEmpty(MaxNumberOfGuests.ToString()))
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("NumberMustBePositive");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                    Match match = _PositiveNumberRegex.Match(MaxNumberOfGuests.ToString());
                    if (!match.Success)
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("EnterPositiveNumber");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                }
                else if (columnName == "Start")
                {
                    if (string.IsNullOrEmpty(Start))
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("StartPointRequired");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                    Match match = _StartRegex.Match(Start);
                    if (!match.Success)
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("EnterStart");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                }
                else if (columnName == "Finish")
                {
                    if (string.IsNullOrEmpty(Finish))
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("FinishRequired");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                    Match match = _StartRegex.Match(Finish);
                    if (!match.Success)
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("EnterFinish");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                }
                else if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration.ToString()))
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("NumberMustBePositive");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                    Match match = _PositiveNumberRegex.Match(Duration.ToString());
                    if (!match.Success)
                    {
                        TextBlock label = new TextBlock();
                        LocExtension locExtension = new LocExtension("EnterPositiveNumber");
                        BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                        return label.Text;
                    }
                }
                else
                {
                    TextBlock label = new TextBlock();
                    LocExtension locExtension = new LocExtension("Error");
                    BindingOperations.SetBinding(label, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
                    return label.Text;
                }
                return null;
            }
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
