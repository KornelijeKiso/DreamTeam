using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.ModelDAO
{
    public class RouteLocationDAO
    {
        public List<IObserver> Observers;
        public RouteLocationFileHandler FileHandler { get; set; }
        public List<RouteLocation> RouteLocations { get; set; }
        public RouteLocationDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new RouteLocationFileHandler();
            RouteLocations = FileHandler.Load();
        }
        public int GenerateId()
        {
            int id = 0;
            if(RouteLocations == null)
            {
                id = 0;
            }
            else
            {
                foreach(var routeLocation in RouteLocations)
                {
                    id = routeLocation.Id + 1;
                }
            }
            return id;
        }
        public int AddAndReturnId(RouteLocation routeLocation)
        {
            routeLocation.Id = GenerateId();
            RouteLocations.Add(routeLocation);
            FileHandler.Save(RouteLocations);
            return routeLocation.Id;
        }

        public void Delete(RouteLocation routeLocation)
        {
            RouteLocations.Remove(routeLocation);
            FileHandler.Save(RouteLocations);
        }

        public List<RouteLocation> GetAll()
        {
            return RouteLocations;
        }
        public RouteLocation? GetOne(int id)
        {
            foreach(var routeLocation in RouteLocations)
            {
                if(routeLocation.Id == id) return routeLocation;
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
