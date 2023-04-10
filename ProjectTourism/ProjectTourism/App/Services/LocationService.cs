using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
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
        public int AddAndReturnId(Location location)
        {
            return LocationRepo.AddAndReturnId(location);
        }
        public void Delete(Location location)
        {
            LocationRepo.Delete(location);
        }
        public Location GetOne(int id)
        {
            return LocationRepo.GetOne(id);
        }
        public List<Location> GetAll()
        {
            return LocationRepo.GetAll();
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
