using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Observer;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Services
{
    public class TourService
    {
        private ITourRepository TourRepository;
        public List<IObserver> Observers;

        public TourService(ITourRepository tourRepository)
        {
            TourRepository = tourRepository;
            Observers = new List<IObserver>();
        }
        public void Add(TourVM tourVM)
        {
            TourRepository.Add(tourVM.GetTour());
        }
        public void Delete(TourVM tourVM)
        {
            TourRepository.Delete(tourVM.GetTour());
        }
        public TourVM GetOne(int id)
        {
            return new TourVM(TourRepository.GetOne(id));
        }
        public List<TourVM> GetAll()
        {
            List<TourVM> tours = new List<TourVM>();
            foreach (var tour in TourRepository.GetAll())
            {
                List<string> pom = TourRepository.GetStops(tour);
                tour.StopsList = pom;
                tours.Add(new TourVM(tour));
            }
            return tours;
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
        public List<string> GetStops(TourVM tour)
        {
            return TourRepository.GetStops(tour.GetTour());
        }
    }
}
