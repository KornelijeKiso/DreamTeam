using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class GuideFileHandler
    {
        private Serializer<Guide> Serializer;
        private readonly string Filename = "../../../References/guides.csv";
        private List<Guide> Guides;
        private UserFileHandler UserFileHandler { get; set; }
        private List<User> Users { get; set; }

        public GuideFileHandler()
        {
            Serializer = new Serializer<Guide>();
            Guides = Serializer.fromCSV(Filename);
            UserFileHandler = new UserFileHandler();
            Users = UserFileHandler.Load();
        }
        public List<Guide> Load()
        {
            Guides = Serializer.fromCSV(Filename);
            AddUserData();
            return Guides;
        }

        public void Save(List<Guide> guides)
        {
            Serializer.toCSV(Filename, guides);
        }

        private void AddUserData()
        {
            foreach (var guide in Guides)
            {
                foreach (var user in Users)
                {
                    if (guide.Username.Equals(user.Username))
                    {
                        guide.Username = user.Username;
                        guide.Password = user.Password;
                        guide.FirstName = user.FirstName;
                        guide.LastName = user.LastName;
                        guide.Type = user.Type;
                        guide.Email = user.Email;
                        guide.PhoneNumber = user.PhoneNumber;
                        guide.Birthday = user.Birthday;
                    }
                }
            }
        }
    }
}
