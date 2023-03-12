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
    public class Guest2DAO
    {
        public List<IObserver> Observers;
        public Guest2FileHandler FileHandler { get; set; }
        public List<Guest2> Guests { get; set; }
        public Guest2DAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new Guest2FileHandler();
            Guests = FileHandler.Load();
        }

        public void Add(Guest2 guest)
        {
            Guests.Add(guest);
            FileHandler.Save(Guests);
        }

        public void Delete(Guest2 guest)
        {
            Guests.Remove(guest);
            FileHandler.Save(Guests);
        }

        public List<Guest2> GetAll()
        {
            return Guests;
        }

        public Guest2 GetOne(string username)
        {
            foreach (var guest in Guests)
            {
                if (guest.Username.Equals(username)) return guest;
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
