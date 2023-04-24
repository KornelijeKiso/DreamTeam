using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class CanceledReservationService
    {
        private ICanceledReservationRepository CanceledReservationRepo;
        public CanceledReservationService()
        {
            CanceledReservationRepo = Injector.Injector.CreateInstance<ICanceledReservationRepository>(); ;
        }
        public void Add(Reservation reservation)
        {
            CanceledReservationRepo.Add(reservation);
        }
        public void Cancel(Reservation reservation)
        {

            CanceledReservationRepo.Delete(reservation);
        }
        public Reservation GetOne(int id)
        {
            return CanceledReservationRepo.GetOne(id);
        }
        public void Update(Reservation reservation)
        {
            CanceledReservationRepo.Update(reservation);
        }
        public List<Reservation> GetAll()
        {
            return CanceledReservationRepo.GetAll();
        }
        public List<Reservation> GetAllByAccommodation(int id)
        {
            return CanceledReservationRepo.GetAllByAccommodation(id);
        }
        public bool IsPossible(Reservation reservation)
        {
            List<Reservation> ReservationsForSameAccommodation = CanceledReservationRepo.GetAllByAccommodation(reservation.AccommodationId);
            return ReservationsForSameAccommodation.Find(res => Conflict(reservation, res)) == null;
        }
        private bool Conflict(Reservation reservation, Reservation existingReservation)
        {
            return !(reservation.StartDate > existingReservation.EndDate || reservation.EndDate < existingReservation.StartDate) && reservation.Id != existingReservation.Id;
        }
    }
}
