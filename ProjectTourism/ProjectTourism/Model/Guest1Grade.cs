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
    public class Guest1Grade:Serializable,INotifyPropertyChanged
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
        public static readonly string[] CategoryNames = { "Cleanness","Communication", "Following the rules"};
        private string _Comment;
        public string Comment
        {
            get => _Comment;
            set
            {
                if(value!=_Comment)
                {
                    _Comment = value;
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
            get=> _Grades;
            set
            {
                if (value != _Grades)
                {
                    _Grades = value;
                    OnPropertyChanged();
                }
            }
        }
        

        public Guest1Grade()
        {
            Grades = new Dictionary<string, int>();
            foreach(var category in CategoryNames)
            {
                Grades.Add(category, 0);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Reservation FindReservation(int reservationId)
        {
            ReservationDAO reservationDAO= new ReservationDAO();
            Reservation reservation = reservationDAO.GetOne(reservationId);
            return reservation;
        }
        public string[] ToCSV()
        {
            List<string> csv = new List<string>();
            csv.Add(Id.ToString());
            csv.Add(ReservationId.ToString());
            csv.Add(Comment);
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
            Reservation = FindReservation(ReservationId);
            for(int i = 3; i < values.Length; i++)
            {
                Grades[CategoryNames[i - 3]] = int.Parse(values[i]);
            }
        }
    }
}
