using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class AccommodationGradeDTO : INotifyPropertyChanged
    {
        private AccommodationGrade _accommodationGrade;
        public AccommodationGradeDTO(AccommodationGrade accommodationGrade)
        {
            _accommodationGrade = accommodationGrade;
        }
        public AccommodationGrade GetAccommodationGrade()
        {
            return _accommodationGrade;
        }
        public RenovationRecommendationDTO RenovationRecommendation { get; set; }
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
            get => CalculateAverageGrade();
        }
        public ReservationDTO Reservation
        {
            get => new ReservationDTO(_accommodationGrade.Reservation);
            set
            {
                if (value.GetReservation() != _accommodationGrade.Reservation)
                {
                    _accommodationGrade.Reservation = value.GetReservation();
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
            get => GetPictureURLsFromCSV();
        }
        private string[] GetPictureURLsFromCSV()
        {
            if (PictureURLs != null)
            {
                string[] pictures = PictureURLs.Split(',');
                foreach (var picture in pictures)
                {
                    picture.Trim();
                }
                return pictures;
            }
            string[] pictures2 = { "" };
            return pictures2;
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
        private double CalculateAverageGrade()
        {
            double sum = 0;
            foreach (var category in CategoryNames)
            {
                sum += Grades[category];
            }
            return sum / CategoryNames.Length;
        }
    }
}
