using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class AccommodationGradeVM:INotifyPropertyChanged
    {
        private AccommodationGrade _accommodationGrade;
        public AccommodationGradeVM(AccommodationGrade accommodationGrade)
        {
            _accommodationGrade = accommodationGrade;
        }
        public AccommodationGrade GetAccommodationGrade()
        {
            return _accommodationGrade;
        }
        public int Id
        {
            get => _accommodationGrade.Id;
            set
            {
                if (value != _accommodationGrade.Id)
                {
                    _accommodationGrade.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationId
        {
            get => _accommodationGrade.ReservationId;
            set
            {
                if (value != _accommodationGrade.ReservationId)
                {
                    _accommodationGrade.ReservationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public static readonly string[] CategoryNames = AccommodationGrade.CategoryNames;
        public string Comment
        {
            get => _accommodationGrade.Comment;
            set
            {
                if (value != _accommodationGrade.Comment)
                {
                    _accommodationGrade.Comment = value;
                    OnPropertyChanged();
                }
            }
        }
        public double AverageGrade
        {
            get => _accommodationGrade.AverageGrade;
            set
            {
                if (value != _accommodationGrade.AverageGrade)
                {
                    _accommodationGrade.AverageGrade = value;
                    OnPropertyChanged();
                }
            }
        }
        public Reservation Reservation
        {
            get => _accommodationGrade.Reservation;
            set
            {
                if (value != _accommodationGrade.Reservation)
                {
                    _accommodationGrade.Reservation = value;
                    OnPropertyChanged();
                }
            }
        }
        public Dictionary<string, int> Grades
        {
            get => _accommodationGrade.Grades;
            set
            {
                if (value != _accommodationGrade.Grades)
                {
                    _accommodationGrade.Grades = value;
                    CalculateAverageGrade();
                    OnPropertyChanged();
                }
            }
        }
        public string[] Pictures
        {
            get => _accommodationGrade.Pictures;
            set
            {
                if (value != _accommodationGrade.Pictures)
                {
                    _accommodationGrade.Pictures = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PictureURLs
        {
            get => _accommodationGrade.PictureURLs;
            set
            {
                if (value != _accommodationGrade.PictureURLs)
                {
                    _accommodationGrade.PictureURLs = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void CalculateAverageGrade()
        {
            double sum = 0;
            foreach (var category in CategoryNames)
            {
                sum += Grades[category];
            }
            AverageGrade = sum / CategoryNames.Length;
        }
    }
}
