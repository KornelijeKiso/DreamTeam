using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IGuest1Repository
    {
        Guest1 GetOne(string guest1Username);
        List<Guest1> GetAll();
        void Add(Guest1 guest1);
        void Delete(Guest1 guest1);
        void Update(Guest1 guest1);
        List<Reservation> GetReservationsByGuest(string guestUsername);
    }
}
