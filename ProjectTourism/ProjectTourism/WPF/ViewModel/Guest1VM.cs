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
    public class Guest1VM:INotifyPropertyChanged
    {
        private Guest1 _guest1;
        public Guest1VM(Guest1 guest1)
        {
            _guest1 = guest1;
            Reservations = _guest1.Reservations.Select(r => new ReservationVM(r)).ToList();
        }
        public Guest1 GetGuest1()
        {
            return _guest1;
        }
        public string Username
        {
            get => _guest1.Username;
            set
            {
                if (value != _guest1.Username)
                {
                    _guest1.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get => _guest1.FirstName;
            set
            {
                if (value != _guest1.FirstName)
                {
                    _guest1.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => _guest1.LastName;
            set
            {
                if (value != _guest1.LastName)
                {
                    _guest1.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _guest1.Email;
            set
            {
                if (value != _guest1.Email)
                {
                    _guest1.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public double AverageGrade
        {
            get => _guest1.AverageGrade;
            set
            {
                if (value != _guest1.AverageGrade)
                {
                    _guest1.AverageGrade = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<ReservationVM> Reservations;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
