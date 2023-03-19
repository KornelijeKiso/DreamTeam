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
    public class TourController
    {
        public TourDAO TourDAO { get; set; }
        public TourController()
        { 
            TourDAO = new TourDAO();
        }
        public List<Tour> GetAll()
        {
            List<Tour> tours= TourDAO.Tours;
            TourAppointmentController tourAppointmentController = new TourAppointmentController();
            foreach (Tour tour in tours)
            {
                List<string> pom = TourDAO.GetStops(tour);
                tour.StopsList = pom;
            }
            return tours;
        }
        public Tour? GetOne(int id)
        { 
            foreach(Tour tour in GetAll())
            {
                if (tour.Id == id) return tour;
            }
            return null;
        }
        public void Add(Tour tour)
        {
            TourDAO.Add(tour);
        }
        public Tour? Identify(Tour tour)
        {
            return TourDAO.Identify(tour);
        }
        public void Subscribe(IObserver observer)
        {
            TourDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            TourDAO.NotifyObservers();
        }
       
    }
}
