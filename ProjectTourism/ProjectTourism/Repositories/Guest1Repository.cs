using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class Guest1Repository:IGuest1Repository
    {
        public Guest1FileHandler FileHandler { get; set; }
        public List<Guest1> Guests1 { get; set; }
        public Guest1Repository()
        {
            FileHandler = new Guest1FileHandler();
            Guests1 = FileHandler.Load();
        }

        public void Add(Guest1 guest1)
        {
            Guests1.Add(guest1);
            FileHandler.Save(Guests1);
        }
        public void Delete(Guest1 guest1)
        {
            Guests1.Remove(guest1);
            FileHandler.Save(Guests1);
        }
        public List<Guest1> GetAll()
        {
            return Guests1;
        }

        public Guest1 GetOne(string username)
        {
            foreach (var guest1 in Guests1)
            {
                if (guest1.Username.Equals(username)) return guest1;
            }
            return null;
        }

        public void Update(Guest1 guest1)
        {
            throw new NotImplementedException();
        }
    }
}
