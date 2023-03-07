using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjectTourism.Model
{
    public enum ACCOMMODATIONTYPE { APARTMENT, HOUSE, HUT }
    public class Accommodation:Serializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public ACCOMMODATIONTYPE Type { get; set; }
        public int MaxNumberOfGuests { get; set; }
        public int MinDaysForReservation { get; set; }
        public int CancellationDeadline { get; set; }
        public string OwnerUsername { get; set; }
        public Owner Owner { get; set; }
        public List<Image> Images { get; set; }

        public Accommodation(int id, int locationId, ACCOMMODATIONTYPE type, int maxNumberOfGuests, int minDaysForReservation, int cancellationDeadline, string ownerUsername)
        {
            Id = id;
            LocationId = locationId;
            Location = FindLocation(locationId);
            Type = type;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinDaysForReservation = minDaysForReservation;
            CancellationDeadline = cancellationDeadline;
            Images = new List<Image>();
            OwnerUsername = ownerUsername;
            Owner = FindOwner(ownerUsername);
        }
        public Accommodation(int locationId, ACCOMMODATIONTYPE type, int maxNumberOfGuests, int minDaysForReservation, int cancellationDeadline, string ownerUsername)
        {
            Location = FindLocation(locationId);
            LocationId = locationId;
            Type = type;
            MaxNumberOfGuests = maxNumberOfGuests;
            MinDaysForReservation = minDaysForReservation;
            CancellationDeadline = cancellationDeadline;
            Images = new List<Image>();
            OwnerUsername = ownerUsername;
            Owner = FindOwner(ownerUsername);
        }
        public Accommodation()
        {
            CancellationDeadline = 1;
            Images = new List<Image>();
        }
        public Location FindLocation(int id)
        {
            LocationDAO locationDAO = new LocationDAO();
            return locationDAO.GetOne(id);
        }
        public Owner FindOwner(string username)
        {
            OwnerDAO ownerDAO = new OwnerDAO();
            return ownerDAO.GetOne(username);
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
                OwnerUsername               };
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
        }
    }
}
