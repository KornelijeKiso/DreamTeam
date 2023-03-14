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
        public void Delete(Ticket tickets)
        {
            Tickets.Remove(tickets);
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
    }
}
