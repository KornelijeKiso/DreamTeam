using ProjectTourism.Domain.IRepositories;
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
    public class Guest2Repository : IGuest2Repository
    {
        public Guest2FileHandler FileHandler { get; set; }
        public List<Guest2> Guests { get; set; }
        public Guest2Repository()
        {
            FileHandler = new Guest2FileHandler();
            Guests = FileHandler.Load();
        }
        public Guest2 GetOne(string username)
        {
            foreach (var guest in Guests)
            {
                if (guest.Username.Equals(username)) return guest;
            }
            return null;
        }
        public List<Guest2> GetAll()
        {
            return Guests;
        }

        public void Add(Guest2 guest)
        {
            Guests.Add(guest);
            FileHandler.Save(Guests);
        }

        public void Delete(Guest2 guest)
        {
            Guests.Remove(guest);
            FileHandler.Save(Guests);
        }
        
        public void Update(Guest2 guest)
        {
        }

        public List<Ticket>? GetTickets(Guest2? guest2)
        {
            if (guest2 == null) return null;
            return guest2.Tickets;
        }
    }
}
