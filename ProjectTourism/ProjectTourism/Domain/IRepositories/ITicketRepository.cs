using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ITicketRepository
    {
        Ticket GetOne(int Id);
        List<Ticket> GetAll();
        void Add(Ticket ticket);
        void Delete(Ticket ticket); 
        void Update(Ticket ticket);
        List<Ticket> GetByAppointment(int tourAppId);
        List<Ticket> GetAllByGuest(string guest2Username);
        Ticket GetGuest2Ticket(string guest2Username, int tourAppId);
    }
}
