﻿using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class AccommodationRepository : IAccommodationRepository
    {
        public AccommodationFileHandler FileHandler { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public AccommodationRepository()
        {
            FileHandler= new AccommodationFileHandler();
            Accommodations = FileHandler.Load();
            Synchronize();
        }
        public void Synchronize()
        {
            ILocationRepository locationRepo = new LocationRepository();
            IReservationRepository reservationRepo = new ReservationRepository();
            foreach (Accommodation accommodation in Accommodations)
            {
                Location loc = locationRepo.GetOne(accommodation.LocationId);
                accommodation.Location = loc;
                accommodation.CityAndCountry = loc.City + ", " + loc.Country;
                foreach (Reservation reservation in reservationRepo.GetAll())
                {
                    if (reservation.AccommodationId == accommodation.Id)
                    {
                        reservation.Accommodation = accommodation;
                        accommodation.Reservations.Add(reservation);
                    }
                }
            }
        }
        public int GenerateId()
        {
            int id = 0;
            if (Accommodations == null)
            {
                id = 0;
            }
            else
            {
                foreach (var accommodation in Accommodations)
                {
                    id = accommodation.Id + 1;
                }
            }
            return id;
        }
        public void Add(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            Accommodations.Add(accommodation);
            FileHandler.Save(Accommodations);
        }

        public void Delete(Accommodation accommodation)
        {
            Accommodations.Remove(accommodation);
            FileHandler.Save(Accommodations);
        }

        public List<Accommodation> GetAll()
        {
            IOwnerRepository ownerRepository= new OwnerRepository();
            foreach(Accommodation accommodation in Accommodations)
            {
                accommodation.Owner = ownerRepository.GetOne(accommodation.OwnerUsername);
            }
            return Accommodations;
        }

        public Accommodation GetOne(int accommodationId)
        {
            foreach (var accommodation in Accommodations)
            {
                if (accommodation.Id == accommodationId) return accommodation;
            }
            return null;
        }

        public void Update(Accommodation accommodation)
        {
            foreach (var existingAccommodation in Accommodations)
            {
                if (existingAccommodation.Id == accommodation.Id)
                {
                    existingAccommodation.Name = accommodation.Name; 
                }
            }
        }
        public List<Accommodation> GetAllByOwner(string ownerUsername)
        {
            List<Accommodation> ownersAccommodations = new List<Accommodation>();
            foreach(var accommodation in Accommodations)
            {
                if(accommodation.OwnerUsername == ownerUsername)
                {
                    ownersAccommodations.Add(accommodation);
                }
            }
            return ownersAccommodations;
        }
    }
}