using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;

namespace ProjectTourism.Controller
{
    public class RouteController
    {
        public RouteDAO RouteDAO { get; set; }
        public RouteController()
        { 
            RouteDAO = new RouteDAO();
        }
        public List<Route> GetAll()
        {
            List<Route> routes= RouteDAO.Routes;
            foreach (Route route in routes)
            {
                List<string> pom = RouteDAO.GetStops(route);
                route.StopsList = pom;
            }
            return routes;
        }
        public Route? GetOne(int id)
        { 
            foreach(Route route in GetAll())
            {
                if (route.Id == id) return route;
            }
            return null;
        }
        public void Add(Route route)
        {
            RouteDAO.Add(route);
        }
        public Route? Identify(Route route)
        {
            return RouteDAO.Identify(route);
        }
        public void Subscribe(IObserver observer)
        {
            RouteDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            RouteDAO.NotifyObservers();
        }
        public string GetNextStop(Route route, int stopint)
        {
            if (stopint < 0 || stopint >= route.StopsList.Count - 1)
            {
                throw new ArgumentException("Invalid stop index");
            }
            return route.StopsList[stopint + 1];
        }
        public void ChangeState(Route route)
        {
            RouteDAO.ChangeState(route);
        }
        public void ChangeCurrentStop(Route route)
        {
            RouteDAO.ChangeCurrentStop(route);
        }
    }
}
