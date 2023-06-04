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
    public class Guest1GradeDTO : INotifyPropertyChanged
    {
        private Guest1Grade _guest1Grade;
        public Guest1GradeDTO(Guest1Grade guest1Grade)
        {
            _guest1Grade = guest1Grade;
        }
        public Guest1GradeDTO()
        {
            _guest1Grade = new Guest1Grade();
        }
        public Guest1Grade GetGuest1Grade()
        {
            return _guest1Grade;
        }
        public int Id
        {
            get => _guest1Grade.Id;
            set
            {
                if (value != _guest1Grade.Id)
                {
                    _guest1Grade.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationId
        {
            get => _guest1Grade.ReservationId;
            set
            {
                if (value != _guest1Grade.ReservationId)
                {
                    _guest1Grade.ReservationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public static readonly string[] CategoryNames = Guest1Grade.CategoryNames;
        public string Comment
        {
            get => _guest1Grade.Comment;
            set
            {
                if (value != _guest1Grade.Comment)
                {
                    _guest1Grade.Comment = value;
                    OnPropertyChanged();
                }
            }
        }
        public ReservationDTO Reservation
        {
            get => new ReservationDTO(_guest1Grade.Reservation);
            set
            {
                if (value.GetReservation() != _guest1Grade.Reservation)
                {
                    _guest1Grade.Reservation = value.GetReservation();
                    OnPropertyChanged();
                }
            }
        }
        public Dictionary<string, int> Grades
        {
            get => _guest1Grade.Grades;
            set
            {
                if (value != _guest1Grade.Grades)
                {
                    _guest1Grade.Grades = value;
                    OnPropertyChanged();
                }
            }
        }

        public double AverageGrade
        {
            get => CalculateAverageGrade();
        }

        private double CalculateAverageGrade()
        {
            double sum = 0;
            foreach (var category in CategoryNames)
            {
                sum += Grades[category];
            }
            sum = sum / CategoryNames.Length;
            return Math.Round(sum, 2);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
