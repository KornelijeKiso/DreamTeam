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
            return RouteDAO.Routes;
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
    }
}
