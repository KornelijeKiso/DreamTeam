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
    internal class AccommodationGradeController
    {
        private AccommodationGradeDAO AccommodationGradeDAO;
        public AccommodationGradeController()
        {
            AccommodationGradeDAO = new AccommodationGradeDAO();
        }
        public void Add(AccommodationGrade accommodationGrade)
        {
            AccommodationGradeDAO.Add(accommodationGrade);
        }
        public void Delete(AccommodationGrade accommodationGrade)
        {
            AccommodationGradeDAO.Delete(accommodationGrade);
        }
        public AccommodationGrade GetOneByReservation(int reservationId)
        {
            return AccommodationGradeDAO.GetOneByReservation(reservationId);
        }
        public List<AccommodationGrade> GetAll()
        {
            return AccommodationGradeDAO.GetAll();
        }
        public void Subscribe(IObserver observer)
        {
            AccommodationGradeDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            AccommodationGradeDAO.NotifyObservers();
        }
    }
}
