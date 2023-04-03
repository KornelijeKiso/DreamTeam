using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class AccommodationService
    {
        private IAccommodationRepository AccommodationRepo;
        private ILocationRepository LocationRepo;
        public List<IObserver> Observers;
        public AccommodationService(IAccommodationRepository iar)
        {
            AccommodationRepo= iar;
            LocationRepo= new LocationRepository();
            Observers = new List<IObserver>();
        }
        public void Add(Accommodation accommodation)
        {
            accommodation.LocationId = LocationRepo.AddAndReturnId(accommodation.Location);
            AccommodationRepo.Add(accommodation);
        }
        public void Delete(Accommodation accommodation)
        {
            AccommodationRepo.Delete(accommodation);
        }
        public Accommodation GetOne(int id)
        {
            return AccommodationRepo.GetOne(id);
        }
        public List<Accommodation> GetAll()
        {
            return AccommodationRepo.GetAll();
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
