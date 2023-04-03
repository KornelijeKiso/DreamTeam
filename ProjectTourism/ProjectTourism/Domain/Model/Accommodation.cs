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
    public class Accommodation:Serializable
    {
        public int Id;
        public string PictureURLs;
        public string[] Pictures;
        public string Name;
        public Location Location;
        public int LocationId;
        public string CityAndCountry;
        public ACCOMMODATIONTYPE Type;
        public int MaxNumberOfGuests;
        public int MinDaysForReservation;
        public int CancellationDeadline;
        public string OwnerUsername;
        public Owner Owner;
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
            Pictures = accommodation.GetPictureURLsFromCSV(PictureURLs);
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

        public string[] GetPictureURLsFromCSV(string source)
        {
            string[] pictures = source.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
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
            Pictures = GetPictureURLsFromCSV(values[8]);
            Reservations = new List<Reservation>();
        }

        
    }
}
