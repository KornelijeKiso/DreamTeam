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
    public class OwnerService
    {
        public List<IObserver> Observers;
        private IOwnerRepository OwnerRepo;
        public OwnerService(IOwnerRepository ior)
        {
            OwnerRepo = ior;
            Observers = new List<IObserver>();
        }
        public void Add(Owner owner)
        {
            OwnerRepo.Add(owner);
        }
        public void Delete(Owner owner)
        {
            OwnerRepo.Delete(owner);
        }
        public Owner GetOne(string username)
        {
            return OwnerRepo.GetOne(username);
        }
        public List<Owner> GetAll()
        {
            return OwnerRepo.GetAll();
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
