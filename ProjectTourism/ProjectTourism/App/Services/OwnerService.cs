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
        public void Add(OwnerVM owner)
        {
            OwnerRepo.Add(owner.GetOwner());
        }
        public void Delete(OwnerVM owner)
        {
            OwnerRepo.Delete(owner.GetOwner());
        }
        public OwnerVM GetOne(string username)
        {
            return new OwnerVM(OwnerRepo.GetOne(username));
        }
        public List<OwnerVM> GetAll()
        {
            List<OwnerVM> owners= new List<OwnerVM>();
            foreach(var owner in OwnerRepo.GetAll())
            {
                owners.Add(new OwnerVM(owner));
            }
            return owners;
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
