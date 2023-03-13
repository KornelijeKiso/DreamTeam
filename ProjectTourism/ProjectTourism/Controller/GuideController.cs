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
        public List<Route> GetGuidesRoutes(string username)
        {
            return GuideDAO.GetGuidesRoutes(username);
        }
        public List<Route> GetGuidesRoutesCurrent(string username)
        {
            return GuideDAO.GetGuidesRoutesCurrent(username);
        }
        public void Add(Guide guide)
        {
            GuideDAO.Add(guide);
        }
        public Guide? Identify(string username)
        {
            return GuideDAO.Identify(username);
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
