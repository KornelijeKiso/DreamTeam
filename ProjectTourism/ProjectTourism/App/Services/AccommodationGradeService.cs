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
    internal class AccommodationGradeService
    {
        public List<IObserver> Observers;
        private IAccommodationGradeRepository AccommodationGradeRepo;
        public AccommodationGradeService(IAccommodationGradeRepository iagr)
        {
            Observers = new List<IObserver>();
            AccommodationGradeRepo = iagr;
        }
        public void Add(AccommodationGradeVM accommodationGrade)
        {
            AccommodationGradeRepo.Add(accommodationGrade.GetAccommodationGrade());
        }
        public void Delete(AccommodationGradeVM accommodationGrade)
        {
            AccommodationGradeRepo.Delete(accommodationGrade.GetAccommodationGrade());
        }
        public AccommodationGradeVM GetOneByReservation(int reservationId)
        {
            return new AccommodationGradeVM( AccommodationGradeRepo.GetOneByReservation(reservationId));
        }
        public List<AccommodationGradeVM> GetAll()
        {
            List<AccommodationGradeVM> accommodationsGrade = new List<AccommodationGradeVM>();
            foreach (var accommodationGrade in AccommodationGradeRepo.GetAll())
            {
                accommodationsGrade.Add(new AccommodationGradeVM(accommodationGrade));
            }
            return accommodationsGrade;
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
