using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class Guest1Service
    {
        public List<IObserver> Observers;
        private IGuest1Repository Guest1Repo;
        public Guest1Service(IGuest1Repository ig1r)
        {
            Guest1Repo = ig1r;
            Observers = new List<IObserver>();
        }
        public void Add(Guest1 guest1)
        {
            Guest1Repo.Add(guest1);
        }
        public void Delete(Guest1 guest1)
        {
            Guest1Repo.Delete(guest1);
        }
        public Guest1 GetOne(string username)
        {
            return Guest1Repo.GetOne(username);
        }
        public List<Guest1> GetAll()
        {
            return Guest1Repo.GetAll();
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
