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
    public class GuideDAO
    {
        public List<IObserver> Observers;
        public GuideFileHandler GuideFileHandler { get; set; }

        public List <Guide> Guides { get; set; }
        public GuideDAO()
        {
            Observers = new List<IObserver>();
            GuideFileHandler= new GuideFileHandler();
            Guides = GuideFileHandler.Load();
        }
        public void Add(Guide guide)
        {
            Guides.Add(guide);
            GuideFileHandler.Save(Guides);
        }
        public Guide GetOne(string username)
        {
            foreach(Guide guide in Guides)
            {
                if (guide.Username.Equals(username))
                {
                    return guide;
                }
            }
            return null;
        }
        public Guide? Identify(string username)
        {
            Guides = GuideFileHandler.Load();
            foreach(Guide guide in Guides)
            {
                if(guide.Surname.Equals(username))
                {
                    return guide;
                }
            }
            return null;
        }
        public List<Route> GetGuidesRoutes(string username)
        {
            List<Route> routes = new List<Route>();
            RouteDAO routeDAO = new RouteDAO();
            foreach(var route in routeDAO.GetAll())
            {
                if (route.GuideUsername.Equals(username))
                {
                    List<string> pom = routeDAO.GetStops(route);
                    route.StopsList = pom;
                    routes.Add(route);
                }
            }
            return routes;
        }

        public List<TourAppointment> GetGuidesCurrentAppointments(string username)
        {
            List<TourAppointment> appointments = new List<TourAppointment>();
            TourAppointmentDAO tourAppDAO = new TourAppointmentDAO();
            foreach (var tourAppointment in tourAppDAO.GetAll())
            {
                if (tourAppointment.Route.GuideUsername.Equals(username) && tourAppointment.TourDateTime.Date.Equals(DateTime.Now.Date))
                {
                    //List<string> pom = routeDAO.GetStops(route);
                    //route.StopsList = pom;
                    appointments.Add(tourAppointment);
                }
            }
            return appointments;
        }

        public void Update(string username, bool hasTourStarted)
        {
            Guide Guide = GetOne(username);
            Guide.HasTourStarted = hasTourStarted;
            GuideFileHandler.Save(Guides);
            NotifyObservers();
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
