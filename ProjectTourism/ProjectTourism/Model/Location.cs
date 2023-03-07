using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Location:Serializable,INotifyPropertyChanged
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
        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
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
    }
}
