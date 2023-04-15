using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class Guest2FileHandler
    {
        private Serializer<Guest2> Serializer;
        private readonly string Filename = "../../../References/guest2.csv";
        private List<Guest2> Guests;
        private UserFileHandler UserFileHandler { get; set; }
        private List<User> Users { get; set; }


        public Guest2FileHandler()
        {
            Serializer = new Serializer<Guest2>();
            Guests = Serializer.fromCSV(Filename);
            UserFileHandler = new UserFileHandler();
            Users = UserFileHandler.Load();
        }

        public List<Guest2> Load()
        {
            Guests = Serializer.fromCSV(Filename);
            AddUserData();
            return Guests;
        }

        public void Save(List<Guest2> guests)
        {
            Serializer.toCSV(Filename, guests);
        }

        private void AddUserData()
        {
            foreach (var guest2 in Guests)
            {
                foreach (var user in Users)
                {
                    if (guest2.Username.Equals(user.Username))
                    {
                        guest2.Username = user.Username;
                        guest2.Password = user.Password;
                        guest2.FirstName = user.FirstName;
                        guest2.LastName = user.LastName;
                        guest2.Type = user.Type;
                        guest2.Email = user.Email;
                        guest2.PhoneNumber = user.PhoneNumber;
                        guest2.Birthday = user.Birthday;
                    }
                }
            }
        }
    }
}
