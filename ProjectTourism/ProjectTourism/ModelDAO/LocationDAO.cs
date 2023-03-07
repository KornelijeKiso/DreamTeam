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
    public class LocationDAO
    {
        public List<IObserver> Observers;
        public LocationFileHandler FileHandler { get; set; }
        public List<Location> Locations { get; set; }
        public LocationDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new LocationFileHandler();
            Locations = FileHandler.Load();
        }
        public int GenerateId()
        {
            int id = 0;
            if (Locations == null)
            {
                id = 0;
            }
            else
            {
                foreach(var location in Locations)
                {
                    id = location.Id + 1;
                }
            }
            return id;
        }
        public int AddAndReturnId(Location location)
        {
            location.Id= GenerateId();
            Locations.Add(location);
            FileHandler.Save(Locations);
            return location.Id;
        }
        public void Delete(Location location)
        {
            Locations.Remove(location);
            FileHandler.Save(Locations);
        }
        public List<Location> GetAll()
        {
            return Locations;
        }
        public Location GetOne(int id) 
        {
            foreach(var location in Locations)
            {
                if (location.Id == id) return location;
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
