using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class RenovationAppointmentDTO : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private DateOnly _StartDate;
        private DateOnly _EndDate;
        private int _Duration;
        private int _AccommodationId;
        public RenovationAppointmentDTO()
        {
            _StartDate = DateOnly.FromDateTime(DateTime.Now);
            _EndDate = _StartDate.AddDays(10);
            Duration = 3;
        }
        public DateOnly StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public int AccommodationId
        {
            get { return _AccommodationId; }
            set
            {
                if (value != _AccommodationId)
                {
                    _AccommodationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Duration
        {
            get => _Duration;
            set
            {
                if (value != _Duration)
                {
                    if (value <= 0) _Duration = 1; else _Duration = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime StartDateWrapper
        {
            get { return new DateTime(StartDate.Year, StartDate.Month, StartDate.Day); }
            set { StartDate = new DateOnly(value.Year, value.Month, value.Day); }
        }
        public DateTime EndDateWrapper
        {
            get { return new DateTime(EndDate.Year, EndDate.Month, EndDate.Day); }
            set { EndDate = new DateOnly(value.Year, value.Month, value.Day); }
        }
        public ObservableCollection<RenovationDTO> OfferedAppointments()
        {
            return new ObservableCollection<RenovationDTO>(new RenovationService().OfferAppointments(StartDate, EndDate, Duration, AccommodationId).Select(a => new RenovationDTO(a)).ToList());
        }
    }
}
