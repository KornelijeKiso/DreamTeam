using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Reservation: Serializable,INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateOnly _StartDate;
        public DateOnly StartDate
        {
            get => _StartDate;
            set
            {
                if (value != _StartDate)
                {
                    _StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateOnly _EndDate;
        public DateOnly EndDate
        {
            get => _EndDate;
            set
            {
                if (value != _EndDate)
                {
                    _EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateOnly _GradingDeadline;
        public DateOnly GradingDeadline
        {
            get => _GradingDeadline;
            set
            {
                if (value != _GradingDeadline)
                {
                    _GradingDeadline = value;
                    OnPropertyChanged();
                }
            }
        }
        private Accommodation _Accommodation;
        public Accommodation Accommodation
        {
            get => _Accommodation;
            set
            {
                if (value != _Accommodation)
                {
                    _Accommodation = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _AccommodationId;
        public int AccommodationId
        {
            get => _AccommodationId;
            set
            {
                if (value != _AccommodationId)
                {
                    _AccommodationId = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _AccommodationGraded;
        public bool AccommodationGraded
        {
            get => _AccommodationGraded;
            set
            {
                if (value != _AccommodationGraded)
                {
                    _AccommodationGraded = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Guest1Username;
        public string Guest1Username
        {
            get => _Guest1Username;
            set
            {
                if (value != _Guest1Username)
                {
                    _Guest1Username = value;
                    OnPropertyChanged();
                }
            }
        }
        private Guest1 _Guest1;
        public Guest1 Guest1

        {
            get => _Guest1;
            set
            {
                if (value != _Guest1)
                {
                    _Guest1 = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _Graded;
        public bool Graded
        {
            get => _Graded;
            set
            {
                if (value != _Graded)
                {
                    _Graded = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _CanBeGraded;
        public bool CanBeGraded
        {
            get => _CanBeGraded;
            set
            {
                if (value != _CanBeGraded)
                {
                    _CanBeGraded = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _VisibleReview;
        public bool VisibleReview
        {
            get => _VisibleReview;
            set
            {
                if (value != _VisibleReview)
                {
                    _VisibleReview = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _GradingDeadlineMessage;
        public string GradingDeadlineMessage
        {
            get => _GradingDeadlineMessage;
            set
            {
                if(value!=_GradingDeadlineMessage)
                {
                    _GradingDeadlineMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        private AccommodationGrade _AccommodationGrade;
        public AccommodationGrade AccommodationGrade
        {
            get=> _AccommodationGrade;
            set
            {
                if (value != _AccommodationGrade)
                {
                    _AccommodationGrade = value;
                    OnPropertyChanged();
                }
            }
        }

        private Guest1Grade _Guest1Grade;
        public Guest1Grade Guest1Grade
        {
            get => _Guest1Grade;
            set
            {
                if (value != _Guest1Grade)
                {
                    _Guest1Grade = value;
                    OnPropertyChanged();
                }
            }
        }

        public Reservation()
        {
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
        public string[] ToCSV()
        {
            
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString("dd.MM.yyyy"),
                EndDate.ToString("dd.MM.yyyy"),
                Guest1Username,       };
            return csvValues;
        }
        public string GenerateGradingDeadlineMessage()
        {
            if (IsAbleToGrade())
            {
                return GradingDeadline.ToString();
            }
            else
            {
                if(DateOnly.FromDateTime(DateTime.Now) > EndDate)
                {
                    return "Expired.";
                }
                else
                {
                    return "Visit not ended yet.";
                }
            }
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            if(DateOnly.TryParse(values[2],new CultureInfo("en-GB"),DateTimeStyles.None,out var startDate)) StartDate = startDate;
            if (DateOnly.TryParse(values[3], new CultureInfo("en-GB"), DateTimeStyles.None, out var endDate)) EndDate = endDate;
            Guest1Username = values[4];
            GradingDeadline = EndDate.AddDays(5);
            GradingDeadlineMessage = GenerateGradingDeadlineMessage();
        }
    }
}
