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
        public void Delete(Ticket ticket)
        {
            TicketDAO.Delete(ticket);
        }
        public Ticket GetOne(int id)
        {
            return TicketDAO.GetOne(id);
        }

        public List<Ticket> GetByRoute(Route route)
        {
            return TicketDAO.GetByRoute(route);
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
