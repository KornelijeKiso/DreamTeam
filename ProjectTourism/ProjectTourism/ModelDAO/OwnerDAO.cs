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
    public class OwnerDAO
    {
        public List<IObserver> Observers;
        public OwnerFileHandler FileHandler { get; set; }
        public List<Owner> Owners { get; set; }
        public OwnerDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new OwnerFileHandler();
            Owners = FileHandler.Load();
        }
        
        public void Add(Owner owner)
        {
            Owners.Add(owner);
            FileHandler.Save(Owners);
        }
        public void Delete(Owner owner)
        {
            Owners.Remove(owner);
            FileHandler.Save(Owners);
        }
        public List<Owner> GetAll()
        {
            return Owners;
        }
        public Owner GetOne(string username)
        {
            foreach (var owner in Owners)
            {
                if (owner.Username.Equals(username)) return owner;
            }
            return null;
        }

        public List<Accommodation> GetOwnersAccomodations(string username)
        {
            List<Accommodation> accommodations= new List<Accommodation>();
            AccommodationDAO accommodationDAO= new AccommodationDAO();
            foreach(var accommodation in accommodationDAO.GetAll())
            {
                if (accommodation.OwnerUsername.Equals(username))
                {
                    accommodations.Add(accommodation);
                }
            }
            return accommodations;
        }
        public List<Reservation> GetOwnersReservations(string username)
        {
            List<Reservation> reservations = new List<Reservation>();
            ReservationDAO reservationDAO = new ReservationDAO();
            foreach (var reservation in reservationDAO.GetAll())
            {
                if (reservation.Accommodation.OwnerUsername.Equals(username))
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
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
