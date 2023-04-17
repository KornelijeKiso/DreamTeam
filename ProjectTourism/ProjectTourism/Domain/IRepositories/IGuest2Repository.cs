using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IGuest2Repository
    {
        Guest2 GetOne(string guest2Username);
        List<Guest2> GetAll();
        void Add(Guest2 guest2);
        void Delete(Guest2 guest2);
        void Update(Guest2 guest2);
        List<Ticket>? GetTickets(Guest2? guest2);
    }
}
