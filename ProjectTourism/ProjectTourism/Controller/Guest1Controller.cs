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
    public class Guest1Controller
    {
        private Guest1DAO Guest1DAO;
        public Guest1Controller()
        {
            Guest1DAO = new Guest1DAO();
        }
        public void Add(Guest1 guest1)
        {
            Guest1DAO.Add(guest1);
        }
        public void Delete(Guest1 guest1)
        {
            Guest1DAO.Delete(guest1);
        }
        public Guest1 GetOne(string username)
        {
            return Guest1DAO.GetOne(username);
        }
        public List<Guest1> GetAll()
        {
            return Guest1DAO.GetAll();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return Guest1DAO.GetAllAccomodations();
        }
        public void Subscribe(IObserver observer)
        {
            Guest1DAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            Guest1DAO.NotifyObservers();
        }
    }
}
