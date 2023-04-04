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
    public class LocationVM:INotifyPropertyChanged,IDataErrorInfo
    {
        private Location _location;
        public LocationVM(Location location)
        {
            _location = location;
        }
        public LocationVM(string city, string country)
        {
            _location = new Location(city,country);
        }
        public Location GetLocation()
        {
            return _location;
        }
        public int Id
        {
            get => _location.Id;
            set
            {
                if (value != _location.Id)
                {
                    _location.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string City
        {
            get => _location.City;
            set
            {
                if (value != _location.City)
                {
                    _location.City = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Country
        {
            get => _location.Country;
            set
            {
                if (value != _location.Country)
                {
                    _location.Country = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Reset()
        {
            _location.Reset();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required!";
                }
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Country is required!";
                }

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "City", "Country" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
    }
}
