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
    public class RouteDateDAO
    {
        public List<IObserver> Observers;
        public RouteDateFileHandler FileHandler { get; set; }
        public List<RouteDate> RouteDates { get; set; }
        public RouteDateDAO()
        {
            Observers = new List<IObserver>();
            FileHandler= new RouteDateFileHandler();
            RouteDates = FileHandler.Load();
        }
        public int GenerateId(Route route)
        {
            return route.Id;
        }
        public List<RouteDate> GetByRoute(int id)
        {
            List<RouteDate> routesById= new List<RouteDate>();
            foreach (var routedate in RouteDates)
            {
                if (routedate.Id == id)
                    routesById.Add(routedate);
            }
            return routesById;
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
