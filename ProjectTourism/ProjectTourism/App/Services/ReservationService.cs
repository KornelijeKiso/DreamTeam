﻿using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class ReservationService
    {
        private IReservationRepository ReservationRepo;
        public ReservationService()
        {
            ReservationRepo = Injector.Injector.CreateInstance<IReservationRepository>();
        }
        public void Add(Reservation reservation)
        {
            ReservationRepo.Add(reservation);
        }
        public void Cancel(Reservation reservation)
        {

            ReservationRepo.Delete(reservation);
        }
        public Reservation GetOne(int id)
        {
            return ReservationRepo.GetOne(id);
        }
        public void Update(Reservation reservation)
        {
            ReservationRepo.Update(reservation);
        }
        public List<Reservation> GetAll()
        {
            return ReservationRepo.GetAll().OrderByDescending(r=>r.StartDate).ToList();
        }
        public List<Reservation> GetAllByAccommodation(int id)
        {
            return ReservationRepo.GetAllByAccommodation(id);
        }
        public bool IsPossible(Reservation reservation)
        {
            List<Renovation> RenovationsForSameAccommdoation = new RenovationService().GetAllByAccommodation(reservation.AccommodationId);
            List<Reservation> ReservationsForSameAccommodation = ReservationRepo.GetAllByAccommodation(reservation.AccommodationId);
            return ReservationsForSameAccommodation.Find(res => Conflict(reservation, res)) == null
                && RenovationsForSameAccommdoation.Find(ren => RenovationConflict(reservation, ren)) == null;
        }
        private bool Conflict(Reservation reservation, Reservation existingReservation)
        {
            return !(reservation.StartDate >= existingReservation.EndDate || reservation.EndDate <= existingReservation.StartDate) && reservation.Id!=existingReservation.Id;
        }
        private bool RenovationConflict(Reservation reservation, Renovation renovation)
        {
            return !(reservation.StartDate > renovation.EndDate || reservation.EndDate < renovation.StartDate);
        }
    }
}
