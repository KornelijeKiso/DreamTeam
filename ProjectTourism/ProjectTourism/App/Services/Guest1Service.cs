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
    public class Guest1Service
    {
        public List<IObserver> Observers;
        private IGuest1Repository Guest1Repo;
        public Guest1Service(IGuest1Repository ig1r)
        {
            Guest1Repo = ig1r;
            Observers = new List<IObserver>();
        }
        public void Add(Guest1VM guest1)
        {
            Guest1Repo.Add(guest1.GetGuest1());
        }
        public void Delete(Guest1VM guest1)
        {
            Guest1Repo.Delete(guest1.GetGuest1());
        }
        public Guest1VM GetOne(string username)
        {
            Guest1 guest = Guest1Repo.GetOne(username);
            guest.Reservations = Guest1Repo.GetReservationsByGuest(username);
            return new Guest1VM(guest);
        }
        public List<Guest1VM> GetAll()
        {
            List<Guest1VM> guests = new List<Guest1VM>();
            foreach (var guest1 in Guest1Repo.GetAll())
            {
                guests.Add(new Guest1VM(guest1));
            }
            return guests;
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
