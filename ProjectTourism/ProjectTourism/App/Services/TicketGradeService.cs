using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class TicketGradeService
    {
        public List<IObserver> Observers;
        private ITicketGradeRepository TicketGradeRepository;

        public TicketGradeService(ITicketGradeRepository itgr)
        {
            Observers = new List<IObserver>();
            TicketGradeRepository = itgr;
        }
        public void Update(TicketGrade ticketGrade)
        {
            TicketGradeRepository.Update(ticketGrade);
        }
        public TicketGrade GetOne(int Id)
        {
            return TicketGradeRepository.GetOne(Id);
        }
        public List<TicketGrade> GetAll()
        {
            return TicketGradeRepository.GetAll();
        }
        public void Add(TicketGrade addedTicketGrade)
        {
            TicketGradeRepository.Add(addedTicketGrade);
        }
        public void Delete(TicketGrade ticketGrade)
        {
            TicketGradeRepository.Delete(ticketGrade);
        }
        public TicketGrade GetOneByTicket(int ticketId)
        {
            return TicketGradeRepository.GetOneByTicket(ticketId);
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
