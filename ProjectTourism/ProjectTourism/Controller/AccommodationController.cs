using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Controller
{
    public class AccommodationController
    {
        private AccommodationDAO AccommodationDAO;
        private LocationDAO LocationDAO;
        public AccommodationController()
        {
            AccommodationDAO= new AccommodationDAO();
            LocationDAO= new LocationDAO();
        }
        public void Add(Accommodation accommodation)
        {
            accommodation.LocationId = LocationDAO.AddAndReturnId(accommodation.Location);
            AccommodationDAO.Add(accommodation);
        }
        public void Delete(Accommodation accommodation)
        {
            AccommodationDAO.Delete(accommodation);
        }
        public Accommodation GetOne(int id)
        {
            return AccommodationDAO.GetOne(id);
        }
        public List<Accommodation> GetAll()
        {
            return AccommodationDAO.GetAll();
        }
        public void Subscribe(IObserver observer)
        {
            AccommodationDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            AccommodationDAO.NotifyObservers();
        }
    }
}
