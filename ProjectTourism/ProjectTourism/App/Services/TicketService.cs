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

        public Ticket GetOne(int Id) 
        {
            return TicketRepository.GetOne(Id);
        }

        public List<Ticket> GetAll() 
        {
            return TicketRepository.GetAll();
        }

        public void Add(Ticket ticket) 
        {
            TicketRepository.Add(ticket);
        }

        public void Delete(Ticket ticket) 
        {
            TicketRepository.Delete(ticket);
        }

        public void Update(Ticket ticket) 
        {
            TicketRepository.Update(ticket);
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
        public List<Ticket> GetByAppointment(int tourAppointmentId)
        {
            return TicketRepository.GetByAppointment(tourAppointmentId);
        }
        public List<Ticket> GetByGuest2(string guest2Username)
        {
            return TicketRepository.GetByGuest2(guest2Username);
        }
    }
}
