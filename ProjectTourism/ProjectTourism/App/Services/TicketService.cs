using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class TicketService
    {
        private ITicketRepository TicketRepository;
        public List<IObserver> Observers;

        public TicketService(ITicketRepository itr)
        {
            TicketRepository = itr; 
            Observers = new List<IObserver>();
        }

        public TicketVM GetOne(int Id) 
        {
            return new TicketVM(TicketRepository.GetOne(Id));
        }

        public List<TicketVM> GetAll() 
        {
            List<TicketVM> tickets = new List<TicketVM>();
            foreach (var ticket in TicketRepository.GetAll())
            {
                tickets.Add(new TicketVM(ticket));
            }
            return tickets;
        }

        public void Add(TicketVM ticket) 
        {
            TicketRepository.Add(ticket.GetTicket());
        }

        public void Delete(TicketVM ticket) 
        {
            TicketRepository.Delete(ticket.GetTicket());
        }

        public void Update(TicketVM ticket) 
        {
            TicketRepository.Update(ticket.GetTicket());
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
