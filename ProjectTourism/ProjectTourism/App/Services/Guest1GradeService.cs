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
    public class Guest1GradeService
    {
        private IGuest1GradeRepository Guest1GradeRepo;
        public List<IObserver> Observers;
        public Guest1GradeService(IGuest1GradeRepository ig1gr)
        {
            Guest1GradeRepo = ig1gr;
            Observers = new List<IObserver>();
        }
        public void Add(Guest1GradeVM guest1Grade)
        {
            Guest1GradeRepo.Add(guest1Grade.GetGuest1Grade());
        }
        public void Delete(Guest1GradeVM guest1Grade)
        {
            Guest1GradeRepo.Delete(guest1Grade.GetGuest1Grade());
        }
        public Guest1GradeVM GetOneByReservation(int reservationId)
        {
            return new Guest1GradeVM(Guest1GradeRepo.GetOneByReservation(reservationId));
        }
        public List<Guest1GradeVM> GetAll()
        {
            List<Guest1GradeVM> guestsGrade = new List<Guest1GradeVM>();
            foreach (var guest1Grade in Guest1GradeRepo.GetAll())
            {
                guestsGrade.Add(new Guest1GradeVM(guest1Grade));
            }
            return guestsGrade;
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
