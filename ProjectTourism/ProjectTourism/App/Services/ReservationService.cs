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
        public void Add(Reservation reservation)
        {
            ReservationRepo.Add(reservation);
        }
        public void Delete(Reservation reservation)
        {
            ReservationRepo.Delete(reservation);
        }
        public Reservation GetOne(int id)
        {
            return ReservationRepo.GetOne(id);
        }
        public List<Reservation> GetAll()
        {
            return ReservationRepo.GetAll();
        }
        public List<Reservation> GetAllByAccommodation(int id)
        {
            return ReservationRepo.GetAllByAccommodation(id);
        }
        public bool IsPossible(Reservation reservation)
        {
            List<Reservation> ReservationsForSameAccommodation = ReservationRepo.GetAllByAccommodation(reservation.AccommodationId);
            return ReservationsForSameAccommodation.Find(res => Conflict(reservation, res)) == null;
        }
        private bool Conflict(Reservation reservation, Reservation existingReservation)
        {
            return !(reservation.StartDate > existingReservation.EndDate || reservation.EndDate < existingReservation.StartDate) && reservation.Id!=existingReservation.Id;
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
