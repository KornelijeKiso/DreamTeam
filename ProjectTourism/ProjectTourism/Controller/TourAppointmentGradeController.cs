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
    public class TourAppointmentGradeController
    {
        private TourAppointmentGradeDAO TourAppointmentGradeDAO;
        public TourAppointmentGradeController()
        {
            TourAppointmentGradeDAO = new TourAppointmentGradeDAO();
        }
        public void Add(TourAppointmentGrade addedTourAppointmentGrade)
        {
            TourAppointmentGradeDAO.Add(addedTourAppointmentGrade);
        }
        public void Delete(TourAppointmentGrade tourAppointmentGrade)
        {
            TourAppointmentGradeDAO.Delete(tourAppointmentGrade);
        }
        public List<TourAppointmentGrade> GetAll()
        {
            return TourAppointmentGradeDAO.GetAll();
        }
        public TourAppointmentGrade? GetOne(int id)
        {
            return TourAppointmentGradeDAO.GetOne(id);
        }
        public TourAppointmentGrade GetOneByTourAppointment(int tourAppId)
        {
            return TourAppointmentGradeDAO.GetOneByTourAppointment(tourAppId);
        }
        public void Subscribe(IObserver observer)
        {
            TourAppointmentGradeDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            TourAppointmentGradeDAO.NotifyObservers();
        }
    }
}
