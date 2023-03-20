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
        public List<Ticket> GetAll()
        {
            return TicketDAO.GetAll();
        }

        public Ticket GetOne(int id)
        {
            return TicketDAO.GetOne(id);
        }
        //public List<Ticket> GetByAppointment(TourAppointment tourApp)
        public List<Ticket> GetByAppointment(int tourAppId)
        {
            return TicketDAO.GetByAppointment(tourAppId);
        }

        public List<Ticket> GetByGuest(string guest2Username)
        {
            return TicketDAO.GetByGuest(guest2Username);
        }

        public Ticket GetGuest2Ticket(string guest2Username, int tourAppId)
        {
            return TicketDAO.GetGuest2Ticket(guest2Username, tourAppId);
        }

        public void CheckGuestStatus(int tourAppId, int ticketId)
        {
            TicketDAO.CheckGuestStatus(tourAppId, ticketId);
        }
        public void Subscribe(IObserver observer)
        {
            TicketDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            TicketDAO.NotifyObservers();
        }
        public void GuideCheck(Ticket ticket)
        {
            TicketDAO.GuideCheck(ticket);
        }
    }
}
