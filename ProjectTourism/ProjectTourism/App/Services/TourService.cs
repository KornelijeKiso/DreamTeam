using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
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
        public int AddAndReturnId(Tour tour)
        {
            return TourRepository.Add(tour);
        }
        public void Delete(Tour tour)
        {
            TourRepository.Delete(tour);
        }

        public List<string> LoadStops(Tour tour)
        {
            List<string> result = tour.Stops.Split(',').ToList();
            result.Add(tour.Finish);
            result.Insert(0, tour.Start);
            return result;
        }
        public Tour GetOne(int id)
        {
            return TourRepository.GetOne(id);
        }
        public List<Tour> GetAll()
        {
            return TourRepository.GetAll();
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
        public Tour? Identify(Tour tour)
        {
            foreach (var existingTour in TourRepository.GetAll())
            {
                if (existingTour.Id == tour.Id)
                {
                    return existingTour;
                }
            }
            return null;
        }
        public List<string> GetStops(Tour tour)
        {
            return TourRepository.GetStops(tour);
        }
        public string GetNextStop(Tour tour, int checkpointIndex)
        {
            List<string> stops = GetStops(tour);
            tour.StopsList = stops;

            if (checkpointIndex < 0 || checkpointIndex >= tour.StopsList.Count - 1)
            {
                throw new ArgumentException("Invalid stop index");
            }

            return tour.StopsList[checkpointIndex + 1];
        }
    }
}
