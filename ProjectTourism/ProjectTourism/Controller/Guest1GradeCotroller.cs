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
    public class Guest1GradeCotroller
    {
        private Guest1GradeDAO Guest1GradeDAO;
        public Guest1GradeCotroller()
        {
            Guest1GradeDAO = new Guest1GradeDAO();
        }
        public void Add(Guest1Grade guest1Grade)
        {
            Guest1GradeDAO.Add(guest1Grade);
        }
        public void Delete(Guest1Grade guest1Grade)
        {
            Guest1GradeDAO.Delete(guest1Grade);
        }
        public Guest1Grade GetOneByReservation(int reservationId)
        {
            return Guest1GradeDAO.GetOneByReservation(reservationId);
        }
        public List<Guest1Grade> GetAll()
        {
            return Guest1GradeDAO.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            Guest1GradeDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            Guest1GradeDAO.NotifyObservers();
        }
    }
}
