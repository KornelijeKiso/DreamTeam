using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class DestinationDTO
    {
        public LocationDTO Location { get; set; }
        public List<ReservationDTO> Reservations { get; set; }
        public List<AccommodationDTO> Accommodations { get; set; }

        public DestinationDTO(LocationDTO location)
        {
            Location = location;
            Reservations = new List<ReservationDTO>();
            Accommodations = new List<AccommodationDTO>();
        }
        public int NumberOfReservations
        {
            get => Reservations.Count;
        }
        public int NumberOfAccommodations
        {
            get => Accommodations.Count;
        }
        public int Occupancy
        {
            get => CalculateOccupancy();
        }
        public int CalculateOccupancy()
        {
            int countedDays = 0;
            int totalDays = 0;
            foreach (var accommodation in Accommodations)
            {
                DateOnly date = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
                for (DateOnly m = date; m < DateOnly.FromDateTime(DateTime.Now); m = m.AddDays(1))
                {
                    totalDays++;
                    if (accommodation.IsReserved(m)) countedDays++;
                }
            }
            return Convert.ToInt32(100 * (double)countedDays / (double)totalDays);
        }
    }
}
