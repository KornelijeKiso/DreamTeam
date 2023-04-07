using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.ViewModel
{
    public class TourVM : INotifyPropertyChanged
    {
        private Tour _tour;
        public Tour GetTour() { return _tour; }
        public TourVM(Tour tour)
        {
            _tour = tour;
        }
        public string? Name
        {
            get => _tour.Name;
            set
            {
                if (_tour.Name != value)
                {
                    _tour.Name = value;
                }
            }
        }
        public int? LocationId
        {
            get => _tour.LocationId;
            set
            {
                if (_tour.LocationId != value)
                {
                    _tour.LocationId = value;
                }
            }
        }
        public Location? Location
        {
            get => _tour.Location;
            set
            {
                if (_tour.Location != value)
                {
                    _tour.Location = value;
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
                }
            }
        }
        public DateTime StartDate
        {
            get => _tour.StartDate;
            set
            {
                _tour.StartDate = value;
            }
        }
        public double? Duration
        {
            get => _tour.Duration;
            set
            {
                _tour.Duration = value;
            }
        }
        public string? PictureURLs
        {
            get => _tour.PictureURLs;
            set
            {
                _tour.PictureURLs = value;
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
                }
            }
        }
        public string? GuideUsername
        {
            get => _tour.GuideUsername;
            set
            {
                _tour.GuideUsername = value;
            }
        }

        public Guide? Guide
        {
            get => _tour.Guide;
            set
            {
                _tour.Guide = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
