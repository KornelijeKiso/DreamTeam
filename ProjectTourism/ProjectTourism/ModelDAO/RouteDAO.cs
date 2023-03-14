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
    public class RouteDAO
    {
        public List<IObserver> Observers;
        public RouteFileHandler RouteFileHandler { get; set; }
        public List<Route> Routes { get; set; }
        public RouteDAO()
        {
            RouteFileHandler = new RouteFileHandler();
            Routes = RouteFileHandler.Load();
            Observers = new List<IObserver>();
        }

        public int GenerateId()
        {
            if(Routes.Count == 0) return 0;
            return Routes.Last<Route>().Id + 1;
        }
        public void Add(Route addedRoute)
        {
            if (addedRoute == null)
            {
                addedRoute.Id = 0;
            }
            else
            {
                addedRoute.Id = GenerateId();
                Routes.Add(addedRoute);
                RouteFileHandler.Save(Routes);
            }
        }
        public List<Route> GetAll()
        {
            return Routes;
        }
        public Route? GetOne(int id)
        {
            foreach(var route in Routes)
            {
                if (route.Id == id) return route;
            }
            return null;
        }
        public Route? Identify(Route route)
        {
            Routes = RouteFileHandler.Load();
            foreach(var existingRoute in Routes)
            {
                if(existingRoute.Id == route.Id)
                {
                    return existingRoute;
                }
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

        public List<string> GetStops(Route route)
        {
            List<string> stops = new List<string>();
            string[] str = route.Stops.Split(',');
            foreach (string s in str) 
            {   
                stops.Add(s.Trim());
            }
            stops.Insert(0, route.Start);
            stops.Add(route.Finish);
            return stops;
        }

    }
}
