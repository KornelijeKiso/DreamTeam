using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class RenovationDTO : INotifyPropertyChanged
    {
        private Renovation _renovation;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RenovationDTO(Renovation renovation)
        {
            _renovation = renovation;
        }
        public RenovationDTO()
        {
            _renovation = new Renovation();
        }
        public Renovation GetRenovation()
        {
            return _renovation;
        }
        public int Id
        {
            get => _renovation.Id;
            set
            {
                if (value != _renovation.Id)
                {
                    _renovation.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int AccommodationId
        {
            get => _renovation.AccommodationId;
            set
            {
                if (value != _renovation.AccommodationId)
                {
                    _renovation.AccommodationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly StartDate
        {
            get => _renovation.StartDate;
            set
            {
                if (value != _renovation.StartDate)
                {
                    _renovation.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get => _renovation.EndDate;
            set
            {
                if (value != _renovation.EndDate)
                {
                    _renovation.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => _renovation.Description;
            set
            {
                if (value != _renovation.Description)
                {
                    _renovation.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Finished
        {
            get => EndDate < DateOnly.FromDateTime(DateTime.Now);
        }
        public bool NotFinished
        {
            get => !Finished;
        }
        public bool Cancellable
        {
            get => StartDate > DateOnly.FromDateTime(DateTime.Now.AddDays(5));
        }
        public void Reset()
        {
            StartDate = new DateOnly();
            EndDate = new DateOnly();
            Description = null;
            Id = 0;
            _renovation.StartDate = new DateOnly();
            _renovation.EndDate = new DateOnly();
            _renovation.Description = null;
            _renovation.Id = 0;
        }
    }
}
