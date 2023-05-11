using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectTourism.Services
{
    public class RenovationService
    {
        private IRenovationRepository RenovationRepo;
        public RenovationService()
        {
            RenovationRepo = Injector.Injector.CreateInstance<IRenovationRepository>();
        }
        public List<Renovation> GetAllByAccommodation(int accommodationId)
        {
            return RenovationRepo.GetAllByAccommodation(accommodationId);
        }
        public List<Renovation> GetAll()
        {
            return RenovationRepo.GetAll();
        }
        public void Cancel(Renovation renovation)
        {
            RenovationRepo.Delete(renovation);
        }
        public int ScheduleAndReturnId(Renovation renovation)
        {
            return RenovationRepo.AddAndReturnId(renovation);
        }
        public Renovation GetOne(int id)
        {
            return RenovationRepo.GetOne(id);
        }
        public List<Renovation> OfferAppointments(DateOnly startDate, DateOnly endDate, int duration, int id)
        {
            List<Renovation> renovations = new List<Renovation>();
            for(DateOnly start = startDate; start<=endDate.AddDays(-1*duration); start=start.AddDays(1))
            {
                Renovation renovation = new Renovation();
                renovation.AccommodationId = id;
                renovation.StartDate = start;
                renovation.EndDate = start.AddDays(duration);
                if (IsPossible(renovation) && start >= DateOnly.FromDateTime(DateTime.Now))
                {
                    renovations.Add(renovation);
                }
            }
            return renovations;
        }
        public bool IsPossible(Renovation renovation)
        {
            List<Renovation> RenovationsForSameAccommdoation = RenovationRepo.GetAllByAccommodation(renovation.AccommodationId);
            List<Reservation> ReservationsForSameAccommodation = new ReservationService().GetAllByAccommodation(renovation.AccommodationId);
            return RenovationsForSameAccommdoation.Find(ren => Conflict(renovation, ren)) == null
                && ReservationsForSameAccommodation.Find(res => ReservationConflict(res, renovation)) == null;
        }
        private bool Conflict(Renovation renovation, Renovation existingRenovation)
        {
            return !(renovation.StartDate > existingRenovation.EndDate || renovation.EndDate < existingRenovation.StartDate);
        }
        private bool ReservationConflict(Reservation reservation, Renovation renovation)
        {
            return !(reservation.StartDate >= renovation.EndDate || reservation.EndDate <= renovation.StartDate);
        }
    }
}
