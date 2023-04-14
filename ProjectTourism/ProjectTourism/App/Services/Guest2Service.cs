using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class Guest2Service
    {
        public List<IObserver> Observers;
        private IGuest2Repository Guest2Repository;
        public Guest2Service(IGuest2Repository ig2r)
        {
            Guest2Repository = ig2r;
            Observers = new List<IObserver>();
        }
        public Guest2 GetOne(string username)
        {
            return Guest2Repository.GetOne(username);
        }
        public List<Guest2> GetAll()
        {
            return Guest2Repository.GetAll();
        }
        public void Add(Guest2 guest)
        {
            Guest2Repository.Add(guest);
        }

        public void Delete(Guest2 guest)
        {
            Guest2Repository.Delete(guest);
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
