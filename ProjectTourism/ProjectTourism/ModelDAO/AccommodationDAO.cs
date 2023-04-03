using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace ProjectTourism.ModelDAO
{
    public class AccommodationDAO
    {
        public List<IObserver> Observers;
        public AccommodationFileHandler FileHandler { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public AccommodationDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new AccommodationFileHandler();
            Accommodations = FileHandler.Load();
            Synchronize();
        }
        public void Synchronize()
        {
            LocationDAO locationDAO = new LocationDAO();
            ReservationDAO reservationDAO = new ReservationDAO();
            foreach(Accommodation accommodation in Accommodations)
            {
                accommodation.Location = locationDAO.GetOne(accommodation.LocationId);
                foreach(Reservation reservation in reservationDAO.GetAll())
                {
                    if(reservation.AccommodationId == accommodation.Id)
                    {
                        reservation.Accommodation = accommodation;
                        accommodation.Reservations.Add(reservation);
                    }
                }
            }
        }
        public int GenerateId()
        {
            int id = 0;
            if (Accommodations == null)
            {
                id = 0;
            }
            else
            {
                foreach(var accommodation in Accommodations)
                {
                    id = accommodation.Id + 1;
                }
            }
            return id;
        }
        public void Add(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            Accommodations.Add(accommodation);
            FileHandler.Save(Accommodations);
        }
        public void Delete(Accommodation accommodation)
        {
            Accommodations.Remove(accommodation);
            FileHandler.Save(Accommodations);
        }
        public List<Accommodation> GetAll()
        {
            return Accommodations;
        }
        public Accommodation GetOne(int id)
        {
            foreach (var accommodation in Accommodations)
            {
                if (accommodation.Id == id) return accommodation;
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
