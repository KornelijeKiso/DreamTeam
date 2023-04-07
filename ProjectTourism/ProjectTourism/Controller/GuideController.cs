using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;

namespace ProjectTourism.Controller
{
    public class GuideController
    {
        public GuideDAO GuideDAO { get; set; }
        public GuideController()
        {
            GuideDAO = new GuideDAO();
        }
        public List<Guide> GetAll()
        {
            return GuideDAO.Guides;
        }
        public Guide? GetOne(string username)
        {
            return GuideDAO.GetOne(username);
        }
        public List<Tour> GetGuidesTours(string username)
        {
            return GuideDAO.GetGuidesTours(username);
        }
        public List<TourAppointment> GetGuidesAppointments(string username)
        {
            return GuideDAO.GetGuidesAppointments(username);
        }
        public List<TourAppointment> GetGuidesCurrentAppointments(string username)
        {
            return GuideDAO.GetGuidesCurrentAppointments(username);
        }
        public void Add(Guide guide)
        {
            GuideDAO.Add(guide);
        }
        public void Update(string username, bool hasTourStarted)
        {
            GuideDAO.Update(username, hasTourStarted);
        }
        public void Subscribe(IObserver observer)
        {
            GuideDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            GuideDAO.NotifyObservers();
        }
    }
}
