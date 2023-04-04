using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Observer;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class LocationService
    {
        public List<IObserver> Observers;
        private ILocationRepository LocationRepo;
        public LocationService(ILocationRepository ilr)
        {
            LocationRepo = ilr;
            Observers = new List<IObserver>();
        }
        public int AddAndReturnId(LocationVM location)
        {
            return LocationRepo.AddAndReturnId(location.GetLocation());
        }
        public void Delete(LocationVM location)
        {
            LocationRepo.Delete(location.GetLocation());
        }
        public LocationVM GetOne(int id)
        {
            return new LocationVM(LocationRepo.GetOne(id));
        }
        public List<LocationVM> GetAll()
        {
            List<LocationVM> locations = new List<LocationVM>();
            foreach (var location in LocationRepo.GetAll())
            {
                locations.Add(new LocationVM(location));
            }
            return locations;
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
