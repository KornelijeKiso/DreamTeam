using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ICanceledReservationRepository
    {
        Reservation GetOne(int reservationId);
        List<Reservation> GetAll();
        void Add(Reservation reservation);
        void Delete(Reservation reservation);
        void Update(Reservation reservation);
        List<Reservation> GetAllByGuest1(string username);
        List<Reservation> GetAllByAccommodation(int accommodationId);
    }
}
