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
    public class TicketController
    {
        private TicketDAO TicketDAO;
        public TicketController()
        {
            TicketDAO = new TicketDAO();
        }
        public void Add(Ticket ticket)
        {
            TicketDAO.Add(ticket);
        }

        public void Update(Ticket ticket)
        {
            TicketDAO.Update(ticket);
        }
        public void Delete(Ticket ticket)
        {
            TicketDAO.Delete(ticket);
        }
        public Ticket GetOne(int id)
        {
            return TicketDAO.GetOne(id);
        }

        public List<Ticket> GetByAppointment(TourAppointment tourApp)
        {
            return TicketDAO.GetByAppointment(tourApp);
        }

        public List<Ticket> GetByGuest(Guest2 guest2)
        {
            return TicketDAO.GetByGuest(guest2);
        }

        public void ChangeAppointment(Ticket ticket)
        {
            TicketDAO.ChangeAppointment(ticket);
        }

        public Ticket GetGuest2Ticket(Guest2 guest2, TourAppointment tourApp)
        {
            return TicketDAO.GetGuest2Ticket(guest2, tourApp);
        }

        public List<Ticket> GetAll()
        {
            return TicketDAO.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            TicketDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            TicketDAO.NotifyObservers();
        }
    }
}
