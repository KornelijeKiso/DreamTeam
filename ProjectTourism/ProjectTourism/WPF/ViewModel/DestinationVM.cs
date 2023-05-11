using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectTourism.WPF.ViewModel
{
    public class DestinationVM
    {
        public LocationVM Location { get; set; }
        public List<ReservationVM> Reservations { get; set; }
        public List<AccommodationVM> Accommodations { get; set; }

        public DestinationVM(LocationVM location)
        {
            Location = location;
            Reservations = new List<ReservationVM>();
            Accommodations = new List<AccommodationVM>();
        }
        public int NumberOfReservations
        {
            get => Reservations.Count;
        }
        public int Occupancy
        {
            get => CalculateOccupancy();
        }
        public int CalculateOccupancy()
        {
            int countedDays = 0;
            int totalDays = 0;
            foreach(var accommodation in Accommodations)
            {
                DateOnly date = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
                for (DateOnly m = date; m<DateOnly.FromDateTime(DateTime.Now); m = m.AddDays(1))
                {
                    totalDays++;
                    if (accommodation.IsReserved(m)) countedDays++;
                }
            }
            return Convert.ToInt32(100 * (double)countedDays / (double)totalDays);
        }
    }
}
