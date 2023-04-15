using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class Guest1FileHandler
    {
        private Serializer<Guest1> Serializer;
        private readonly string Filename = "../../../References/guests1.csv";
        private List<Guest1> Guests1;
        private UserFileHandler UserFileHandler { get; set; }
        private List<User> Users { get; set; }

        public Guest1FileHandler()
        {
            Serializer = new Serializer<Guest1>();
            Guests1 = Serializer.fromCSV(Filename);
            UserFileHandler = new UserFileHandler();
            Users = UserFileHandler.Load();
        }

        public List<Guest1> Load()
        {
            Guests1 = Serializer.fromCSV(Filename);
            AddUserData();
            return Guests1;
        }

        public void Save(List<Guest1> guests1)
        {
            Serializer.toCSV(Filename, guests1);
        }

        private void AddUserData()
        {
            foreach (var guest1 in Guests1)
            {
                foreach (var user in Users)
                {
                    if (guest1.Username.Equals(user.Username))
                    {
                        guest1.Username = user.Username;
                        guest1.Password = user.Password;
                        guest1.FirstName = user.FirstName;
                        guest1.LastName = user.LastName;
                        guest1.Type = user.Type;
                        guest1.Email = user.Email;
                        guest1.PhoneNumber = user.PhoneNumber;
                        guest1.Birthday = user.Birthday;
                    }
                }
            }
        }
    }
}
