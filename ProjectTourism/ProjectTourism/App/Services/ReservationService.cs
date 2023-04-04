using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
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
    public class ReservationService
    {
        public List<IObserver> Observers;
        private IReservationRepository ReservationRepo;
        public ReservationService(IReservationRepository irr)
        {
            ReservationRepo = irr;
            Observers = new List<IObserver>();
        }
        public void Add(ReservationVM reservation)
        {
            ReservationRepo.Add(reservation.GetReservation());
        }
        public void Delete(ReservationVM reservation)
        {
            ReservationRepo.Delete(reservation.GetReservation());
        }
        public ReservationVM GetOne(int id)
        {
            return new ReservationVM(ReservationRepo.GetOne(id));
        }
        public List<ReservationVM> GetAll()
        {
            List<ReservationVM> reservations = new List<ReservationVM>();
            foreach (var reservation in ReservationRepo.GetAll())
            {
                reservations.Add(new ReservationVM(reservation));
            }
            return reservations;
        }
        public bool IsPossible(ReservationVM reservation)
        {
            List<Reservation> ReservationsForSameAccommodation = ReservationRepo.GetAll().FindAll(res => res.AccommodationId == reservation.AccommodationId);
            return ReservationsForSameAccommodation.Find(res => Conflict(reservation.GetReservation(), res)) == null;
        }
        private bool Conflict(Reservation reservation, Reservation existingReservation)
        {
            return !(reservation.StartDate > existingReservation.EndDate || reservation.EndDate < existingReservation.StartDate);
        }
        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
            }
        }
    }
}
