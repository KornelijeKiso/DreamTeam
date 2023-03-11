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
    public class ReservationController
    {
        private ReservationDAO ReservationDAO;
        public ReservationController()
        {
            ReservationDAO = new ReservationDAO();
        }
        public void Add(Reservation reservation)
        {
            ReservationDAO.Add(reservation);
        }
        public void Delete(Reservation reservation)
        {
            ReservationDAO.Delete(reservation);
        }
        public Reservation GetOne(int id)
        {
            return ReservationDAO.GetOne(id);
        }
        public List<Reservation> GetAll()
        {
            return ReservationDAO.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            ReservationDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            ReservationDAO.NotifyObservers();
        }
    }
}
