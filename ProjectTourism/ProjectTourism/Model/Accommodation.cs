using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectTourism.Model
{
    public enum ACCOMMODATIONTYPE { APARTMENT, HOUSE, HUT }
    public class Accommodation:Serializable,INotifyPropertyChanged
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

        public Accommodation(int id, string name,  int locationId, ACCOMMODATIONTYPE type, int maxNumberOfGuests, int minDaysForReservation, int cancellationDeadline, string ownerUsername)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Location = FindLocation(locationId);
            Type = type;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinDaysForReservation = minDaysForReservation;
            CancellationDeadline = cancellationDeadline;
            OwnerUsername = ownerUsername;
            Owner = FindOwner(ownerUsername);
        }
        public Accommodation(string name, int locationId, ACCOMMODATIONTYPE type, int maxNumberOfGuests, int minDaysForReservation, int cancellationDeadline, string ownerUsername)
        {
            Name = name;
            Location = FindLocation(locationId);
            LocationId = locationId;
            Type = type;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinDaysForReservation = minDaysForReservation;
            CancellationDeadline = cancellationDeadline;
            OwnerUsername = ownerUsername;
            Owner = FindOwner(ownerUsername);
        }
        public Accommodation()
        {
            CancellationDeadline = 1;
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
            CancellationDeadline= accommodation.CancellationDeadline;        }
        public Location FindLocation(int id)
        {
            LocationDAO locationDAO = new LocationDAO();
            return locationDAO.GetOne(id);
        }
        public void Reset()
        {
            Location = null;
            CancellationDeadline= 1;
            MaxNumberOfGuests= 0;
            MinDaysForReservation= 0;
            Type = ACCOMMODATIONTYPE.APARTMENT;
            Name = "";
            PictureURLs = "";
        }
        public Owner FindOwner(string username)
        {
            OwnerDAO ownerDAO = new OwnerDAO();
            return ownerDAO.GetOne(username);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string[] ToCSV()
        {
            int type;
            switch (Type)
            {
                case ACCOMMODATIONTYPE.APARTMENT: { type = 0; break; }
                case ACCOMMODATIONTYPE.HOUSE: { type = 1; break; }
                case ACCOMMODATIONTYPE.HUT: { type = 2; break; }
                default: { type = 2; break; }
            }
            string[] csvValues =
            {
                Id.ToString(),
                LocationId.ToString(),
                type.ToString(),
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
            Location = FindLocation(LocationId);
            int type = int.Parse(values[2]);
            switch (type)
            {
                case 0: { Type = ACCOMMODATIONTYPE.APARTMENT; break; }
                case 1: { Type = ACCOMMODATIONTYPE.HOUSE; break; }
                case 2: { Type = ACCOMMODATIONTYPE.HUT; break; }
            }
            MaxNumberOfGuests = int.Parse(values[3]);
            MinDaysForReservation = int.Parse(values[4]);
            CancellationDeadline = int.Parse(values[5]);
            OwnerUsername = values[6];
            Name = values[7];
            PictureURLs = values[8];
        }
    }
}
