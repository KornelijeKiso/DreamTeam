using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.ModelDAO
{
    public class OwnerDAO
    {
        public OwnerFileHandler FileHandler { get; set; }
        public List<Owner> Owners { get; set; }
        public OwnerDAO()
        {
            FileHandler = new OwnerFileHandler();
            Owners = FileHandler.Load();
        }
        
        public void Add(Owner owner)
        {
            Owners.Add(owner);
            FileHandler.Save(Owners);
        }
        public void Delete(Owner owner)
        {
            Owners.Remove(owner);
            FileHandler.Save(Owners);
        }
        public List<Owner> GetAll()
        {
            return Owners;
        }
        public Owner GetOne(string username)
        {
            foreach (var owner in Owners)
            {
                if (owner.Username == username) return owner;
            }
            return null;
        }
    }
}
