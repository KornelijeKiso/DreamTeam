using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class AccommodationVM:INotifyPropertyChanged, IDataErrorInfo
    {
        private Accommodation _accommodation;
        public AccommodationVM(Accommodation accommodation)
        {
            _accommodation= accommodation;
            Reservations = new ObservableCollection<ReservationVM>(_accommodation.Reservations.Select(r => new ReservationVM(r)).Reverse().ToList());
            Renovations = new ObservableCollection<RenovationVM>(_accommodation.Renovations.Select(r => new RenovationVM(r)).ToList().OrderByDescending(r=>r.EndDate));
        }
        public AccommodationVM(AccommodationVM accommodation)
        {
            _accommodation = new Accommodation(accommodation.GetAccommodation());
        }
        public Accommodation GetAccommodation()
        {
            return _accommodation;
        }
        public int Id
        {
            get => _accommodation.Id;
            set
            {
                if (value != _accommodation.Id)
                {
                    _accommodation.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TypeIcon
        {
            get
            {
                switch (_accommodation.Type)
                {
                    case ACCOMMODATIONTYPE.APARTMENT: return "https://img.icons8.com/external-xnimrodx-blue-xnimrodx/512/external-hotel-resort-xnimrodx-blue-xnimrodx.png";
                    case ACCOMMODATIONTYPE.HOUSE: return "https://img.icons8.com/external-xnimrodx-blue-xnimrodx/2x/external-house-town-xnimrodx-blue-xnimrodx-4.png";
                    default: return "https://img.icons8.com/external-xnimrodx-blue-xnimrodx/512/external-hut-resort-xnimrodx-blue-xnimrodx.png";
                }
            }
        }
        public string TypeString
        {
            get
            {
                switch (_accommodation.Type)
                {
                    case ACCOMMODATIONTYPE.APARTMENT: return "Apartment";
                    case ACCOMMODATIONTYPE.HOUSE: return "   House  ";
                    default: return "     Hut   ";
                }
            }
        }
        public string PictureURLs
        {
            get => _accommodation.PictureURLs;
            set
            {
                if (value != _accommodation.PictureURLs)
                {
                    _accommodation.PictureURLs = value;
                    OnPropertyChanged();
                }
            }
        }
        public string[] Pictures
        {
            get => GetPictureURLsFromCSV(PictureURLs);
        }
        public string Name
        {
            get => _accommodation.Name;
            set
            {
                if (value != _accommodation.Name)
                {
                    _accommodation.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public LocationVM Location
        {
            get => new LocationVM(_accommodation.Location);
            set
            {
                if (value.GetLocation() != _accommodation.Location)
                {
                    _accommodation.Location = value.GetLocation();
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId
        {
            get => _accommodation.LocationId;
            set
            {
                if (value != _accommodation.LocationId)
                {
                    _accommodation.LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ACCOMMODATIONTYPE Type
        {
            get => _accommodation.Type;
            set
            {
                if (value != _accommodation.Type)
                {
                    _accommodation.Type = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MaxNumberOfGuests
        {
            get => _accommodation.MaxNumberOfGuests;
            set
            {
                if (value != _accommodation.MaxNumberOfGuests)
                {
                    _accommodation.MaxNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MinDaysForReservation
        {
            get => _accommodation.MinDaysForReservation;
            set
            {
                if (value != _accommodation.MinDaysForReservation)
                {
                    _accommodation.MinDaysForReservation = value;
                    OnPropertyChanged();
                }
            }
        }
        public int CancellationDeadline
        {
            get => _accommodation.CancellationDeadline;
            set
            {
                if (value != _accommodation.CancellationDeadline)
                {
                    _accommodation.CancellationDeadline = value;
                    OnPropertyChanged();
                }
            }
        }
        public string OwnerUsername
        {
            get => _accommodation.OwnerUsername;
            set
            {
                if (value != _accommodation.OwnerUsername)
                {
                    _accommodation.OwnerUsername = value;
                    OnPropertyChanged();
                }
            }
        }
        public OwnerVM Owner
        {
            get => new OwnerVM(_accommodation.OwnerUsername);
            set
            {
                if (value.GetOwner() != _accommodation.Owner)
                {
                    _accommodation.Owner = value.GetOwner();
                    OnPropertyChanged();
                }
            }
        }
        public bool IsRecentlyRenovated
        {
            get
            {
                if (NeverRenovated) return false;
                else return LastRenovation.EndDate > DateOnly.FromDateTime(DateTime.Now.AddYears(-1));
            }
        }
        private bool _NoRenovations;
        public bool NoRenovations
        {
            get => _NoRenovations=Renovations.Count == 0;
            set
            {
                if(value!= _NoRenovations)
                {
                    _NoRenovations=value;
                    OnPropertyChanged();
                }
            }
        }
        public RenovationVM LastRenovation
        {
            get
            {
                if (Renovations == null) return null;
                else return Renovations.ToList().Find(r => r.Finished);
            }
        }
        public bool NeverRenovated
        {
            get => LastRenovation==null;
        }
        public bool IsNotRecentlyRenovated
        {
            get=>!IsRecentlyRenovated && !NeverRenovated;
        }
        public ObservableCollection<ReservationVM> Reservations { get; set; }
        public ObservableCollection<RenovationVM> Renovations { get; set; }

        public void CancelRenovation(RenovationVM renovation)
        {
            Renovations.Remove(renovation);
            RenovationService renovationService = new RenovationService();
            renovationService.Cancel(renovation.GetRenovation());
        }
        public void ScheduleNewRenovation(RenovationVM renovation)
        {
            RenovationService renovationService = new RenovationService();
            Renovation ren = new Renovation(renovation.GetRenovation());
            renovationService.Schedule(new Renovation(ren));
            Renovations.Insert(0, new RenovationVM(ren));
            NoRenovations = false;
        }
        public void SetLocation(Location location)
        {
            Location = new LocationVM(location);
            LocationId = location.Id;
        }
        public void Reset()
        {
            Name = "";  
            MaxNumberOfGuests= 1;
            MinDaysForReservation = 1;
            CancellationDeadline = 1;
            PictureURLs = "";
            _accommodation.Reset();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string[] GetPictureURLsFromCSV(string source)
        {
            string[] pictures = source.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
        }
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required!";
                }

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Name" };

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
