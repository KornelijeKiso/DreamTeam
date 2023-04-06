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

        public TicketGradeVM GetOne(int Id)
        {
            return new TicketGradeVM(TicketGradeRepository.GetOne(Id));
        }
        public List<TicketGradeVM> GetAll()
        {
            List<TicketGradeVM> ticketsGrade = new List<TicketGradeVM>();
            foreach (var ticketGrade in TicketGradeRepository.GetAll())
            {
                ticketsGrade.Add(new TicketGradeVM(ticketGrade));
            }
            return ticketsGrade;
        }
        public void Add(TicketGradeVM addedTicketGrade)
        {
            TicketGradeRepository.Add(addedTicketGrade.GetTicketGrade());
        }

        public void Delete(TicketGradeVM ticketGrade)
        {
            TicketGradeRepository.Delete(ticketGrade.GetTicketGrade());
        }

        public TicketGradeVM GetOneByTicket(int ticketId)
        {
            return new TicketGradeVM(TicketGradeRepository.GetOneByTicket(ticketId));
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
