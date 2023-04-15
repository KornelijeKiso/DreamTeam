using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class OwnerFileHandler
    {
        private Serializer<Owner> Serializer;
        private readonly string Filename = "../../../References/owners.csv";
        private List<Owner> Owners;
        private UserFileHandler UserFileHandler { get; set; }
        private List<User> Users { get; set; }

        public OwnerFileHandler()
        {
            Serializer = new Serializer<Owner>();
            Owners = Serializer.fromCSV(Filename);
            UserFileHandler = new UserFileHandler();
            Users = UserFileHandler.Load();
        }

        public List<Owner> Load()
        {
            Owners = Serializer.fromCSV(Filename);
            AddUserData();
            return Owners;
        }

        public void Save(List<Owner> owners)
        {
            Serializer.toCSV(Filename, owners);
        }

        private void AddUserData()
        {
            foreach (var owner in Owners)
            {
                foreach (var user in Users)
                {
                    if (owner.Username.Equals(user.Username))
                    {
                        owner.Username = user.Username;
                        owner.Password = user.Password;
                        owner.FirstName = user.FirstName;
                        owner.LastName = user.LastName;
                        owner.Type = user.Type;
                        owner.Email = user.Email;
                        owner.PhoneNumber = user.PhoneNumber;
                        owner.Birthday = user.Birthday;
                    }
                }
            }
        }
    }
}
