using ProjectTourism.ModelDAO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ProjectTourism.Model
{
    public enum ACCOMMODATIONTYPE { APARTMENT, HOUSE, HUT }
    public class Accommodation:Serializable,INotifyPropertyChanged, IDataErrorInfo
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
        private string _PictureURLs;
        public string PictureURLs
        {
            get => _PictureURLs;
            set
            {
                if (value != _PictureURLs)
                {
                    _PictureURLs = value;
                    OnPropertyChanged();
                }
            }
        }
        private string[] _Pictures;
        public string[] Pictures
        {
            get => _Pictures;
            set
            {
                if (value != _Pictures)
                {
                    _Pictures = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }
        private Location _Location;
        public Location Location
        {
            get => _Location;
            set
            {
                if (value != _Location)
                {
                    _Location = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _LocationId;
        public int LocationId
        {
            get => _LocationId;
            set
            {
                if (value != _LocationId)
                {
                    _LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _CityAndCountry;
        public string CityAndCountry
        {
            get => _CityAndCountry;
            set
            {
                if(value!=_CityAndCountry)
                {
                    _CityAndCountry = value;
                    OnPropertyChanged();
                }
            }
        }
        private ACCOMMODATIONTYPE _Type;
        public ACCOMMODATIONTYPE Type
        {
            get => _Type;
            set
            {
                if (value != _Type)
                {
                    _Type = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _MaxNumberOfGuests;
        public int MaxNumberOfGuests
        {
            get => _MaxNumberOfGuests;
            set
            {
                if (value != _MaxNumberOfGuests)
                {
                    _MaxNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _MinDaysForReservation;
        public int MinDaysForReservation
        {
            get => _MinDaysForReservation;
            set
            {
                if (value != _MinDaysForReservation)
                {
                    _MinDaysForReservation = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _CancellationDeadline;
        public int CancellationDeadline
        {
            get => _CancellationDeadline;
            set
            {
                if (value != _CancellationDeadline)
                {
                    _CancellationDeadline = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _OwnerUsername;
        public string OwnerUsername
        {
            get => _OwnerUsername;
            set
            {
                if (value != _OwnerUsername)
                {
                    _OwnerUsername = value;
                    OnPropertyChanged();
                }
            }
        }
        private Owner _Owner;
        public Owner Owner
        {
            get => _Owner;
            set
            {
                if (value != _Owner)
                {
                    _Owner = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<Reservation> Reservations { get; set; }
        public Accommodation()
        {
            CancellationDeadline = 1;
            MaxNumberOfGuests= 1;
            MinDaysForReservation= 1;
            PictureURLs = "";
            Reservations = new List<Reservation>();
        }
        public Accommodation(Accommodation accommodation)
        {
            Id = accommodation.Id;
            Name = accommodation.Name;
            Location = accommodation.Location;
            LocationId= accommodation.LocationId;
            Type = accommodation.Type;
            PictureURLs= accommodation.PictureURLs;
            Owner= accommodation.Owner;
            OwnerUsername= accommodation.OwnerUsername;
            MaxNumberOfGuests = accommodation.MaxNumberOfGuests;
            MinDaysForReservation = accommodation.MinDaysForReservation;
            CancellationDeadline= accommodation.CancellationDeadline;  
            CityAndCountry = accommodation.CityAndCountry;
            Pictures = accommodation.GetPictureURLsFromCSV();
            Reservations = new List<Reservation>();

        }
        public void SetLocation(Location location)
        {
            Location = location;
            LocationId = location.Id;
            CityAndCountry = location.City + ", " + location.Country;
        }
        public void Reset()
        {
            Location = null;
            CancellationDeadline= 1;
            MaxNumberOfGuests= 1;
            MinDaysForReservation= 1;
            Type = ACCOMMODATIONTYPE.APARTMENT;
            Name = "";
            PictureURLs = "";
        }

        public string[] GetPictureURLsFromCSV()
        {
            string[] pictures = PictureURLs.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int GenerateCSVType()
        {
            switch (Type)
            {
                case ACCOMMODATIONTYPE.APARTMENT: { return 0; }
                case ACCOMMODATIONTYPE.HOUSE: { return 1; }
                case ACCOMMODATIONTYPE.HUT: { return 2; }
                default: { return 2; }
            }
        }
        private ACCOMMODATIONTYPE ReadTypeFromCSV(int type)
        {
            switch (type)
            {
                case 0:return ACCOMMODATIONTYPE.APARTMENT;
                case 1: return ACCOMMODATIONTYPE.HOUSE;
                case 2: return ACCOMMODATIONTYPE.HUT;
                default: return ACCOMMODATIONTYPE.HUT;
            }
        }
        public string[] ToCSV()
        {         
            string[] csvValues =
            {
                Id.ToString(),
                LocationId.ToString(),
                GenerateCSVType().ToString(),
                MaxNumberOfGuests.ToString(),
                MinDaysForReservation.ToString(),
                CancellationDeadline.ToString(),
                OwnerUsername,
                Name,
                PictureURLs     };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            LocationId = int.Parse(values[1]);
            Type = ReadTypeFromCSV(int.Parse(values[2]));
            MaxNumberOfGuests = int.Parse(values[3]);
            MinDaysForReservation = int.Parse(values[4]);
            CancellationDeadline = int.Parse(values[5]);
            OwnerUsername = values[6];
            Name = values[7];
            PictureURLs = values[8];
            Pictures = GetPictureURLsFromCSV();
            Reservations = new List<Reservation>();
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
