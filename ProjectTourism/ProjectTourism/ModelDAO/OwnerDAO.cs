﻿using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace ProjectTourism.ModelDAO
{
    public class OwnerDAO
    {
        public List<IObserver> Observers;
        public OwnerFileHandler FileHandler { get; set; }
        public List<Owner> Owners { get; set; }
        public OwnerDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new OwnerFileHandler();
            Owners = FileHandler.Load();
            Synchronize();
        }
        
        public void Synchronize()
        {
            AccommodationDAO accommodationDAO = new AccommodationDAO();
            foreach(var owner in Owners)
            {
                foreach (var accommodation in accommodationDAO.GetAll())
                {
                    if (accommodation.OwnerUsername == owner.Username)
                    {
                        accommodation.Owner = owner;
                        owner.Accommodations.Add(accommodation);
                        owner.Reservations.AddRange(accommodation.Reservations);
                    }
                }
            }
        }

        public void Add(Owner owner)
        {
            Owners.Add(owner);
            FileHandler.Save(Owners);
        }
        public void Delete(Owner owner)
        {
            Owners.Remove(owner);
            FileHandler.Save(Owners);
        }
        public List<Owner> GetAll()
        {
            return Owners;
        }
        public Owner GetOne(string username)
        {
            foreach (var owner in Owners)
            {
                if (owner.Username.Equals(username)) return owner;
            }
            return null;
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