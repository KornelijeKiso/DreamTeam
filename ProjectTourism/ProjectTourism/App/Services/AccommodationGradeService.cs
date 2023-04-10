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
    public class AccommodationGradeService
    {
        public List<IObserver> Observers;
        private IAccommodationGradeRepository AccommodationGradeRepo;
        public AccommodationGradeService(IAccommodationGradeRepository iagr)
        {
            Observers = new List<IObserver>();
            AccommodationGradeRepo = iagr;
        }
        public void Add(AccommodationGrade accommodationGrade)
        {
            AccommodationGradeRepo.Add(accommodationGrade);
        }
        public void Delete(AccommodationGrade accommodationGrade)
        {
            AccommodationGradeRepo.Delete(accommodationGrade);
        }
        public AccommodationGrade GetOneByReservation(int reservationId)
        {
            return AccommodationGradeRepo.GetOneByReservation(reservationId);
        }
        public List<AccommodationGrade> GetAll()
        {
            return AccommodationGradeRepo.GetAll();
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
