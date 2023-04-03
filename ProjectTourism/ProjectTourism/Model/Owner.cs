using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Owner:Serializable,INotifyPropertyChanged
    {
        private string _Username;
        public string Username
        {
            get => _Username;
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _FirstName;
        public string FirstName 
        {
            get => _FirstName;
            set
            {
                if (value != _FirstName)
                {
                    _FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _LastName;
        public string LastName
        {
            get => _LastName;
            set
            {
                if (value != _LastName)
                {
                    _LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                if (value != _Email)
                {
                    _Email = value;
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
        public List<Accommodation> Accommodations;
        public List<Reservation> Reservations;
        public Owner()
        {
            AverageGrade = 0;
            Accommodations = new List<Accommodation>();
            Reservations = new List<Reservation>();
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
                Username,
                FirstName,
                LastName,
                Email,
                AverageGrade.ToString()     };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Username = values[0];
            FirstName = values[1];
            LastName = values[2];
            Email = values[3];
            AverageGrade = double.Parse(values[4]);
            Accommodations = new List<Accommodation>();
            Reservations = new List<Reservation>();
        }
    }
}
