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
    public class Guest1DAO
    {
        public List<IObserver> Observers;
        public Guest1FileHandler FileHandler { get; set; }
        public List<Guest1> Guests1 { get; set; }
        public Guest1DAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new Guest1FileHandler();
            Guests1 = FileHandler.Load();
        }

        public void Add(Guest1 guest1)
        {
            Guests1.Add(guest1);
            FileHandler.Save(Guests1);
        }
        public void Delete(Guest1 guest1)
        {
            Guests1.Remove(guest1);
            FileHandler.Save(Guests1);
        }
        public List<Guest1> GetAll()
        {
            return Guests1;
        }
        public Guest1 GetOne(string username)
        {
            foreach (var guest1 in Guests1)
            {
                if (guest1.Username.Equals(username)) return guest1;
            }
            return null;
        }

        //
        //
        //
        //
        //
        // Mozda bude nekih promena ovde
        public List<Accommodation> GetAllAccomodations()
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            AccommodationDAO accommodationDAO = new AccommodationDAO();
            foreach (var accommodation in accommodationDAO.GetAll())
            {
                accommodations.Add(accommodation);
            }
            return accommodations;
        }
        //
        //
        //
        //
        //
        //


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
