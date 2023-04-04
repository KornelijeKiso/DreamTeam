using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.ModelDAO
{
    public class TicketGradeDAO
    {
        public List<IObserver> Observers;
        public TicketGradeFileHandler TicketGradeFileHandler { get; set; }
        public List<TicketGrade> TicketGrades { get; set; }
        public TicketGradeDAO()
        {
            TicketGradeFileHandler = new TicketGradeFileHandler();
            TicketGrades = TicketGradeFileHandler.Load();
            Observers = new List<IObserver>();
        }
        public int GenerateId()
        {
            if (TicketGrades.Count == 0) return 0;
            return TicketGrades.Last<TicketGrade>().Id + 1;
        }
        public void Add(TicketGrade addedTicketGrade)
        {
            if (addedTicketGrade == null)
            {
                addedTicketGrade.Id = 0;
            }
            else
            {
                addedTicketGrade.Id = GenerateId();
                TicketGrades.Add(addedTicketGrade);
                TicketGradeFileHandler.Save(TicketGrades);
            }
        }
        public void Delete(TicketGrade ticketGrade)
        {
            TicketGrades.Remove(ticketGrade);
            TicketGradeFileHandler.Save(TicketGrades);
        }
        public List<TicketGrade> GetAll()
        {
            return TicketGrades;
        }
        public TicketGrade? GetOne(int id)
        {
            foreach (var tourAppGrade in TicketGrades)
            {
                if (tourAppGrade.Id == id) return tourAppGrade;
            }
            return null;
        }
        public TicketGrade GetOneByTicket(int ticketId)
        {
            foreach (var ticketGrade in TicketGrades)
            {
                if (ticketGrade.TicketId == ticketId) return ticketGrade;
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
