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
    public class Guest2Controller
    {
        private Guest2DAO Guest2DAO;

        public Guest2Controller()
        {
            Guest2DAO = new Guest2DAO();
        }

        public void Add(Guest2 guest)
        {
            Guest2DAO.Add(guest);
        }

        public void Delete(Guest2 guest)
        {
            Guest2DAO.Delete(guest);
        }

        public List<Guest2> GetAll()
        {
            return Guest2DAO.GetAll();
        }
        public Guest2 GetOne(string username)
        {
            return Guest2DAO.GetOne(username);
        }
        public void Subscribe(IObserver observer)
        {
            Guest2DAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            Guest2DAO.NotifyObservers();
        }
    }
}
