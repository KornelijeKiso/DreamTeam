using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class RouteDate: Serializable, INotifyPropertyChanged
    {
        public DateTime RouteDateTime { get; set; }
        
        private int _RouteId;
        public int Id
        {
            get => _RouteId;
            set
            {
                if (_RouteId != value)
                {
                    _RouteId = value;
                }
            }
        }
        public RouteDate(){  }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(), RouteDateTime.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            RouteDateTime = Convert.ToDateTime(values[1]);
        }
    }
}
