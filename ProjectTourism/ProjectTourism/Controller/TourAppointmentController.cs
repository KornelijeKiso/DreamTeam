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
    public class TourAppointmentController
    {
        public TourAppointmentDAO TourAppointmentDAO { get; set; }
        public TourAppointmentController()
        {
            TourAppointmentDAO = new TourAppointmentDAO();
        }
        public void MakeTourAppointments(Tour route)
        {
            TourAppointmentDAO.MakeTourAppointments(route);
        }
        public List<TourAppointment> GetAll()
        {
            return TourAppointmentDAO.GetAll();
        }
        public TourAppointment GetOne(int id)
        {
            return TourAppointmentDAO.GetOne(id);
        }
        public List<TourAppointment> GetByTour(int id)
        {
            return TourAppointmentDAO.GetByTour(id);
        }
        public TourAppointment GetByDate(int tourId, DateTime date)
        {
            return TourAppointmentDAO.GetByDate(tourId, date);
        }
        public void UpdateAppointmentCreate(int tourAppointmentId, Ticket ticket)
        {
            TourAppointmentDAO.UpdateAppointmentCreate(tourAppointmentId, ticket);
        }
        public void UpdateAppointmentReturn(int tourAppointmentId, Ticket ReturnedTicket)
        {
            TourAppointmentDAO.UpdateAppointmentReturn(tourAppointmentId, ReturnedTicket);
        }
        public void Delete(int tourAppointmentId)
        {
            TourAppointmentDAO.Delete(tourAppointmentId);
        }
        public void UpdateAppointmentTicket(int tourAppointmentId, Ticket ReturnedTicket)
        {
            TourAppointmentDAO.UpdateAppointmentTicket(tourAppointmentId, ReturnedTicket);
        }
        public void ChangeState(TourAppointment tourAppointment)
        {
            TourAppointmentDAO.ChangeState(tourAppointment);
        }
        public void ChangeCurrentStop(TourAppointment tourAppointment)
        {
            TourAppointmentDAO.ChangeState(tourAppointment);
        }
        public string GetNextStop(Tour tour, int stopint)
        {
            return TourAppointmentDAO.GetNextStop(tour, stopint);
        }
        public void Subscribe(IObserver observer)
        {
            TourAppointmentDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            TourAppointmentDAO.NotifyObservers();
        }
    }
}
