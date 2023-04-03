using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTourism.Model
{
    public class AccommodationGrade : Serializable, INotifyPropertyChanged
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
        private int _ReservationId;
        public int ReservationId
        {
            get => _ReservationId;
            set
            {
                if (value != _ReservationId)
                {
                    _ReservationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public static readonly string[] CategoryNames = { "Cleanness", "Comfort", "Hospitality", "Price and quality ratio", "Location" };
        private string _Comment;
        public string Comment
        {
            get => _Comment;
            set
            {
                if (value != _Comment)
                {
                    _Comment = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _AverageGrade;
        public double AverageGrade
        {
            get => _AverageGrade;
            set
            {
                if (value != _AverageGrade)
                {
                    _AverageGrade = value;
                    OnPropertyChanged();
                }
            }
        }
        private Reservation _Reservation;
        public Reservation Reservation
        {
            get => _Reservation;
            set
            {
                if (value != _Reservation)
                {
                    _Reservation = value;
                    OnPropertyChanged();
                }
            }
        }
        private Dictionary<string, int> _Grades;
        public Dictionary<string, int> Grades
        {
            get => _Grades;
            set
            {
                if (value != _Grades)
                {
                    _Grades = value;
                    OnPropertyChanged();
                }
            }
        }
        private string[] _Pictures;
        public string[] Pictures
        {
            get => _Pictures;
            set
            {
                if (value != _Pictures)
                {
                    _Pictures = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _PictureURLs;
        public string PictureURLs
        {
            get => _PictureURLs;
            set
            {
                if (value != _PictureURLs)
                {
                    _PictureURLs = value;
                    OnPropertyChanged();
                }
            }
        }
        public AccommodationGrade()
        {
            Grades = new Dictionary<string, int>();
            foreach (var category in CategoryNames)
            {
                Grades.Add(category, 0);
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
            foreach(var category in CategoryNames)
            {
                sum += Grades[category];
            }
            AverageGrade = sum / CategoryNames.Length;
        }
        public string[] GetPictureURLsFromCSV()
        {
            string[] pictures = PictureURLs.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
        }
        public string[] ToCSV()
        {
            List<string> csv = new List<string>();
            csv.Add(Id.ToString());
            csv.Add(ReservationId.ToString());
            csv.Add(Comment);
            csv.Add(PictureURLs);
            foreach (var category in CategoryNames)
            {
                csv.Add(Grades[category].ToString());
            }
            string[] csvValues = csv.ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Comment = values[2];
            PictureURLs = values[3];
            Pictures = GetPictureURLsFromCSV();
            for (int i = 4; i < values.Length; i++)
            {
                Grades[CategoryNames[i - 4]] = int.Parse(values[i]);
            }
            CalculateAverageGrade();
        }
    }
}
