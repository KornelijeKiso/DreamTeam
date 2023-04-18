using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class ReservationRepository:IReservationRepository
    {
        public ReservationFileHandler FileHandler { get; set; }
        public List<Reservation> Reservations { get; set; }
        public ReservationRepository()
        {
            FileHandler = new ReservationFileHandler();
            Reservations = FileHandler.Load();
        }
        
        public int GenerateId()
        {
            if (Reservations == null) return 0;
            else return Reservations.Last().Id + 1;
        }
        public void Add(Reservation reservation)
        {
            reservation.Id = GenerateId();
            Reservations.Add(reservation);
            FileHandler.Save(Reservations);
        }

        public void Delete(Reservation reservation)
        {
            Reservations.Remove(reservation);
            FileHandler.Save(Reservations);
        }

        public List<Reservation> GetAll()
        {
            return Reservations;
        }

        public Reservation GetOne(int reservationId)
        {
            return Reservations.Find(r => r.Id == reservationId);
        }

        public void Update(Reservation reservation)
        {
            foreach (var existingAccommodation in Reservations)
            {
                if (existingAccommodation.Id == reservation.Id)
                {
                    existingAccommodation.StartDate = reservation.StartDate;
                    existingAccommodation.EndDate = reservation.EndDate;
                    break;
                }
            }
            FileHandler.Save(Reservations);
        }
        public List<Reservation> GetAllByAccommodation(int accommodationId)
        {
            return Reservations.FindAll(r=>r.AccommodationId == accommodationId);
        }
        public List<Reservation> GetAllByGuest1(string username)
        {
            return Reservations.FindAll(r => r.Guest1Username.Equals(username));
        }
    }
}
