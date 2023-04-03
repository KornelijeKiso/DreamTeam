﻿using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class OwnerRepository:IOwnerRepository
    {
        public OwnerFileHandler FileHandler { get; set; }
        public List<Owner> Owners { get; set; }
        public OwnerRepository()
        {
            FileHandler = new OwnerFileHandler();
            Owners = FileHandler.Load();
            Synchronize();
        }

        public void Synchronize()
        {
            IAccommodationRepository accommodationRepo = new AccommodationRepository();
            foreach (var owner in Owners)
            {
                foreach (var accommodation in accommodationRepo.GetAll())
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

        public void Update(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}
