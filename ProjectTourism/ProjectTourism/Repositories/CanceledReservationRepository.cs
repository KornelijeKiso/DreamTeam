using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class CanceledReservationRepository : ICanceledReservationRepository
    {
        public CanceledReservationFileHandler FileHandler { get; set; }
        public List<Reservation> Reservations { get; set; }
        public CanceledReservationRepository()
        {
            FileHandler = new CanceledReservationFileHandler();
            Reservations = FileHandler.Load();
        }
        public void Add(Reservation reservation)
        {
            Reservations.Add(reservation);
            FileHandler.Save(Reservations);
        }

        public void Delete(Reservation reservation)
        {
            Reservations.Remove(GetOne(reservation.Id));
            FileHandler.Save(Reservations);
        }

        public List<Reservation> GetAll()
        {
            return Reservations;
        }

        public Reservation GetOne(int reservationId)
        {
            foreach (var accommodation in Reservations)
            {
                if (accommodation.Id == reservationId) return accommodation;
            }
            return null;
        }

        public void Update(Reservation reservation)
        {
            foreach (var existingAccommodation in Reservations)
            {
                if (existingAccommodation.Id == reservation.Id)
                {
                    existingAccommodation.StartDate = reservation.StartDate;
                    existingAccommodation.EndDate = reservation.EndDate;
                }
            }
            FileHandler.Save(Reservations);
        }
        public List<Reservation> GetAllByAccommodation(int accommodationId)
        {
            List<Reservation> accommodationReservations = new List<Reservation>();
            foreach (var reservation in Reservations)
            {
                if (reservation.AccommodationId == accommodationId)
                {
                    accommodationReservations.Add(reservation);
                }
            }
            return accommodationReservations;
        }
        public List<Reservation> GetAllByGuest1(string username)
        {
            List<Reservation> accommodationReservations = new List<Reservation>();
            foreach (var reservation in Reservations)
            {
                if (reservation.Guest1Username == username)
                {
                    accommodationReservations.Add(reservation);
                }
            }
            return accommodationReservations;
        }
    }
}
