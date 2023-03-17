using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.ModelDAO
{
    public class TicketDAO
    {
        public List<IObserver> Observers;
        public TicketFileHandler FileHandler { get; set; }
        public List<Ticket> Tickets { get; set; }
        public TicketDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new TicketFileHandler();
            Tickets = FileHandler.Load();
        }
        public int GenerateId()
        {
            int id = 0;
            if (Tickets == null)
            {
                id = 0;
            }
            else
            {
                foreach (var tickets in Tickets)
                {
                    id = tickets.Id + 1;
                }
            }
            return id;
        }
        public void Add(Ticket tickets)
        {
            tickets.Id = GenerateId();
            Tickets.Add(tickets);
            FileHandler.Save(Tickets);
        }

        public void Update(Ticket ticket)
        {
            Ticket updated = GetOne(ticket.Id);
            updated = ticket;
            //Delete(GetOne(ticket.Id));
            //Add(updated);
            FileHandler.Save(Tickets);
            NotifyObservers();
        }
        public void Delete(Ticket ticket)
        {
            Tickets.Remove(ticket);
            FileHandler.Save(Tickets);
        }
        public List<Ticket> GetAll()
        {
            return Tickets;
        }
        public Ticket GetOne(int id)
        {
            foreach (var ticket in Tickets)
            {
                if (ticket.Id == id) return ticket;
            }
            return null;
        }

        public List<Ticket> GetByRoute(Route route)
        {
            List<Ticket> ticketsByRoute = new List<Ticket>();

            foreach (var ticket in Tickets)
            {
                if (route.Id == ticket.RouteId)
                {
                    ticketsByRoute.Add(ticket);
                }
            }
            return ticketsByRoute;
        }
        
        // returns all guest's tickets
        public List<Ticket> GetByGuest(Guest2 guest2)
        {
            List<Ticket> ticketsByGuest = new List<Ticket>();

            foreach (var ticket in Tickets)
            {
                if (guest2.Username == ticket.Guest2Username)
                {
                    ticketsByGuest.Add(ticket);
                }
            }
            return ticketsByGuest;
        }
        
        // returns guest's ticket that should be updated
        public Ticket GetGuest2Ticket(Guest2 guest2, Route route)
        {
            List<Ticket> ticketsByGuest = GetByGuest(guest2);

            foreach (var ticket in ticketsByGuest)
            {
                if (ticket.RouteId == route.Id)
                {
                    return ticket;
                }
            }
            return null;
        }

        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
            }
        }
        public void GuideCheck(Ticket ticket)
        {
            foreach(Ticket t in Tickets)
            {
                if(t.Id== ticket.Id)
                {
                    t.HasGuideChecked= true;
                }
            }
            FileHandler.Save(Tickets);
        }
    }
}
