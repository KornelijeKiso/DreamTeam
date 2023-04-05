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
    public class Guest2Service
    {
        public List<IObserver> Observers;
        private IGuest2Repository Guest2Repository;

        public Guest2Service(IGuest2Repository ig2r)
        {
            Guest2Repository = ig2r;
            Observers = new List<IObserver>();
        }

        public Guest2VM GetOne(string username)
        {
            return new Guest2VM(Guest2Repository.GetOne(username));
        }

        public List<Guest2VM> GetAll()
        {
            List<Guest2VM> guests = new List<Guest2VM>();
            foreach (var guest2 in Guest2Repository.GetAll())
            {
                guests.Add(new Guest2VM(guest2));
            }
            return guests;
        }
        public void Add(Guest2VM guest)
        {
            Guest2Repository.Add(guest.GetGuest2());
        }

        public void Delete(Guest2VM guest)
        {
            Guest2Repository.Delete(guest.GetGuest2());
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
