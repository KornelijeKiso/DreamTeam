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

        public Reservation()
        {
        }
        public bool IsAbleToGrade()
        {
            return DateOnly.FromDateTime(DateTime.Now) > EndDate && DateOnly.FromDateTime(DateTime.Now) < GradingDeadline; 
        }
        public bool IsGraded()
        {
            Guest1GradeDAO guest1GradeDAO= new Guest1GradeDAO();
            Graded = guest1GradeDAO.GetOneByReservation(Id) != null;
            return Graded;
        }
        public Guest1 FindGuest1(string username)
        {
            Guest1DAO guest1DAO = new Guest1DAO();
            return guest1DAO.GetOne(username);
        }
        public Accommodation FindAccommodation(int id)
        {
            AccommodationDAO accommodationDAO = new AccommodationDAO();
            return accommodationDAO.GetOne(id);
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
                return "Expired";
            }
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            Accommodation = FindAccommodation(AccommodationId);
            if(DateOnly.TryParse(values[2],new CultureInfo("en-GB"),DateTimeStyles.None,out var startDate)) StartDate = startDate;
            if (DateOnly.TryParse(values[3], new CultureInfo("en-GB"), DateTimeStyles.None, out var endDate)) EndDate = endDate;
            Guest1Username = values[4];
            Guest1 = FindGuest1(Guest1Username);
            Accommodation = FindAccommodation(AccommodationId);
            GradingDeadline = EndDate.AddDays(5);
            GradingDeadlineMessage = GenerateGradingDeadlineMessage();
        }
    }
}
