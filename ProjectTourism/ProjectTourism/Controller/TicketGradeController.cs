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
    public class TicketGradeController
    {
        private TicketGradeDAO TourAppointmentGradeDAO;
        public TicketGradeController()
        {
            TourAppointmentGradeDAO = new TicketGradeDAO();
        }
        public void Add(TicketGrade addedTicketGrade)
        {
            TourAppointmentGradeDAO.Add(addedTicketGrade);
        }
        public void Delete(TicketGrade ticketGrade)
        {
            TourAppointmentGradeDAO.Delete(ticketGrade);
        }
        public List<TicketGrade> GetAll()
        {
            return TourAppointmentGradeDAO.GetAll();
        }
        public TicketGrade? GetOne(int id)
        {
            return TourAppointmentGradeDAO.GetOne(id);
        }
        public TicketGrade GetOneByTicket(int ticketId)
        {
            return TourAppointmentGradeDAO.GetOneByTicket(ticketId);
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
