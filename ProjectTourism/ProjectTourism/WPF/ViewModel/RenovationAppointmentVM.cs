﻿using ProjectTourism.Model;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTourism.WPF.ViewModel
{
    public class RenovationAppointmentVM:INotifyPropertyChanged,IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private DateOnly _StartDate;
        private DateOnly _EndDate;
        private int _Duration;
        private int _AccommodationId;
        public RenovationAppointmentVM()
        {
            _StartDate = DateOnly.FromDateTime(DateTime.Now);
            _EndDate = _StartDate.AddDays(10);
            Duration = 3;
        }
        public DateOnly StartDate
        {
            get { return _StartDate; } set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get { return _EndDate; } set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public int AccommodationId
        {
            get { return _AccommodationId;}
            set
            {
                if(value!=_AccommodationId)
                {
                    _AccommodationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Duration
        {
            get => _Duration;
            set
            {
                if (value != _Duration)
                {
                    if (value <= 0) _Duration = 1; else _Duration =value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime StartDateWrapper
        {
            get { return new DateTime(StartDate.Year, StartDate.Month, StartDate.Day); }
            set { StartDate = new DateOnly(value.Year, value.Month, value.Day); }
        }
        public DateTime EndDateWrapper
        {
            get { return new DateTime(EndDate.Year, EndDate.Month, EndDate.Day); }
            set { EndDate = new DateOnly(value.Year, value.Month, value.Day); }
        }
        public ObservableCollection<RenovationVM> OfferedAppointments()
        {
            return new ObservableCollection<RenovationVM>(new RenovationService().OfferAppointments(StartDate, EndDate, Duration, AccommodationId).Select(a=>new RenovationVM(a)).ToList());
        }
        private Regex _DurationRegex = new Regex("[1-9]|[1-9][0-9]{1,2}|1[0-9]{3}|2000");
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Duration")
                {
                    if (string.IsNullOrEmpty(Duration.ToString()))
                        return "Duration is required!";
                    if (!_DurationRegex.Match(Duration.ToString()).Success)
                        return "Duration must be a positive number!";
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
