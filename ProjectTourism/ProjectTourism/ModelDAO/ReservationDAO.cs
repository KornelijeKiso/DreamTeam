using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.ModelDAO
{
    public class ReservationDAO
    {
        public List<IObserver> Observers;
        public ReservationFileHandler FileHandler { get; set; }
        public List<Reservation> Reservations { get; set; }
        public ReservationDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new ReservationFileHandler();
            Reservations = FileHandler.Load();
        }
        public int GenerateId()
        {
            int id = 0;
            if (Reservations == null)
            {
                id = 0;
            }
            else
            {
                foreach (var reservations in Reservations)
                {
                    id = reservations.Id + 1;
                }
            }
            return id;
        }
        public void Add(Reservation reservations)
        {
            reservations.Id = GenerateId();
            Reservations.Add(reservations);
            FileHandler.Save(Reservations);
        }
        public void Delete(Reservation reservation)
        {
            Reservations.Remove(reservation);
            FileHandler.Save(Reservations);
        }
        public List<Reservation> GetAll()
        {
            return Reservations;
        }
        public Reservation GetOne(int id)
        {
            foreach (var reservation in Reservations)
            {
                if (reservation.Id == id) return reservation;
            }
            return null;
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
