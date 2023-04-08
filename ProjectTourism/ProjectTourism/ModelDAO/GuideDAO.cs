using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public List<Tour> GetGuidesTours(string username)
        {
            List<Tour> tours = new List<Tour>();
            TourDAO tourDAO = new TourDAO();
            foreach(var tour in tourDAO.GetAll())
            {
                if (tour.GuideUsername.Equals(username))
                {
                    List<string> pom = tourDAO.GetStops(tour);
                    tour.StopsList = pom;
                    tours.Add(tour);
                }
            }
            return tours;
        }
        public List<TourAppointment> GetGuidesAppointments(string username)
        {
            List<TourAppointment> appointments = new List<TourAppointment>();
            TourAppointmentDAO tourAppDAO = new TourAppointmentDAO();
            foreach (var tourAppointment in tourAppDAO.GetAll())
            {
                if(tourAppointment.Tour.Guide.Username.Equals(username))
                    appointments.Add(tourAppointment);
            }
            return appointments;
        }
        public List<TourAppointment> GetGuidesCurrentAppointments(string username)
        {
            List<TourAppointment> appointments = new List<TourAppointment>();
            TourAppointmentDAO tourAppDAO = new TourAppointmentDAO();
            foreach (var tourAppointment in tourAppDAO.GetAll())
            {
                if (AppointmentAdditionIsValid(username, tourAppointment))
                {
                    appointments.Add(tourAppointment);
                }
            }
            return appointments;
        }

        private static bool AppointmentAdditionIsValid(string username, TourAppointment tourAppointment)
        {
            return tourAppointment.Tour.GuideUsername.Equals(username) && tourAppointment.TourDateTime.Date.Equals(DateTime.Now.Date);
        }

        public void Update(string username, bool hasTourStarted)  //UpdateHasTourStarted
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
