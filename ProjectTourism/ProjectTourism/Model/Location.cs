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
    public class Location: Serializable, INotifyPropertyChanged, IDataErrorInfo
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
        private string _City;
        public string City
        {
            get => _City;
            set
            {
                if (value != _City)
                {
                    _City = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Country;
        public string Country
        {
            get => _Country;
            set
            {
                if (value != _Country)
                {
                    _Country = value;
                    OnPropertyChanged();
                }
            }
        }
        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }
        public Location() { }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                City,
                Country          };
            return csvValues;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Reset()
        {
            City = "";
            Country = "";
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
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
                else if(columnName == "Country")
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
