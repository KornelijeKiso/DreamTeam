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
    public class ReservationVM:INotifyPropertyChanged
    {
        private Reservation _reservation;
        public ReservationVM(Reservation reservation)
        {
            _reservation = reservation;
        }
        public Reservation GetReservation()
        {
            return _reservation;
        }
        public int Id
        {
            get => _reservation.Id;
            set
            {
                if (value != _reservation.Id)
                {
                    _reservation.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly StartDate
        {
            get => _reservation.StartDate;
            set
            {
                if (value != _reservation.StartDate)
                {
                    _reservation.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get => _reservation.EndDate;
            set
            {
                if (value != _reservation.EndDate)
                {
                    _reservation.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly GradingDeadline
        {
            get => _reservation.GradingDeadline;
            set
            {
                if (value != _reservation.GradingDeadline)
                {
                    _reservation.GradingDeadline = value;
                    OnPropertyChanged();
                }
            }
        }
        public Accommodation Accommodation
        {
            get => _reservation.Accommodation;
            set
            {
                if (value != _reservation.Accommodation)
                {
                    _reservation.Accommodation = value;
                    OnPropertyChanged();
                }
            }
        }
        public int AccommodationId
        {
            get => _reservation.AccommodationId;
            set
            {
                if (value != _reservation.AccommodationId)
                {
                    _reservation.AccommodationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool AccommodationGraded
        {
            get => _reservation.AccommodationGraded;
            set
            {
                if (value != _reservation.AccommodationGraded)
                {
                    _reservation.AccommodationGraded = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Guest1Username
        {
            get => _reservation.Guest1Username;
            set
            {
                if (value != _reservation.Guest1Username)
                {
                    _reservation.Guest1Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest1 Guest1

        {
            get => _reservation.Guest1;
            set
            {
                if (value != _reservation.Guest1)
                {
                    _reservation.Guest1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Graded
        {
            get => _reservation.Graded;
            set
            {
                if (value != _reservation.Graded)
                {
                    _reservation.Graded = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool CanBeGraded
        {
            get => _reservation.CanBeGraded;
            set
            {
                if (value != _reservation.CanBeGraded)
                {
                    _reservation.CanBeGraded = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool VisibleReview
        {
            get => _reservation.VisibleReview;
            set
            {
                if (value != _reservation.VisibleReview)
                {
                    _reservation.VisibleReview = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GradingDeadlineMessage
        {
            get => GenerateGradingDeadlineMessage();
            set
            {
                if (value != _reservation.GradingDeadlineMessage)
                {
                    _reservation.GradingDeadlineMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccommodationGrade AccommodationGrade
        {
            get => _reservation.AccommodationGrade;
            set
            {
                if (value != _reservation.AccommodationGrade)
                {
                    _reservation.AccommodationGrade = value;
                    OnPropertyChanged();
                }
            }
        }
        public Guest1Grade Guest1Grade
        {
            get => _reservation.Guest1Grade;
            set
            {
                if (value != _reservation.Guest1Grade)
                {
                    _reservation.Guest1Grade = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GenerateGradingDeadlineMessage()
        {
            if (IsAbleToGrade())
            {
                return GradingDeadline.ToString();
            }
            else
            {
                if (DateOnly.FromDateTime(DateTime.Now) > EndDate)
                {
                    return "Expired.";
                }
                else
                {
                    return "Visit not ended yet.";
                }
            }
        }
        public bool IsAbleToGrade()
        {
            return DateOnly.FromDateTime(DateTime.Now) > EndDate && DateOnly.FromDateTime(DateTime.Now) < GradingDeadline;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
