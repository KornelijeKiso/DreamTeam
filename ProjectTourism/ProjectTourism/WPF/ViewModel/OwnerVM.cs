using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class OwnerVM : INotifyPropertyChanged
    {
        private Owner _owner;

        public OwnerVM(Owner owner)
        {
            _owner = owner;
            Reservations = _owner.Reservations.Select(r => new ReservationVM(r)).ToList();
            Accommodations = _owner.Accommodations.Select(r => new AccommodationVM(r)).ToList();
        }
        public Owner GetOwner()
        {
            return _owner;
        }
        public string Username
        {
            get => _owner.Username;
            set
            {
                if (value != _owner.Username)
                {
                    _owner.Username = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSuperHost
        {
            get => _owner.IsSuperHost;
            set
            {
                if(value!= _owner.IsSuperHost)
                {
                    _owner.IsSuperHost = value;
                    OnPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get => _owner.FirstName;
            set
            {
                if (value != _owner.FirstName)
                {
                    _owner.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => _owner.LastName;
            set
            {
                if (value != _owner.LastName)
                {
                    _owner.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _owner.Email;
            set
            {
                if (value != _owner.Email)
                {
                    _owner.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public double AverageGrade
        {
            get => _owner.AverageGrade;
            set
            {
                if (value != _owner.AverageGrade)
                {
                    _owner.AverageGrade = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<AccommodationVM> Accommodations;
        public List<ReservationVM> Reservations;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
