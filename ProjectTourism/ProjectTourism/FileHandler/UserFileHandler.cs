using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class UserFileHandler
    {
        private Serializer<User> Serializer;
        private readonly string Filename = "../../../References/users.csv";
        private List<User> Users;

        public UserFileHandler()
        {
            Serializer = new Serializer<User>();
            Users = Serializer.fromCSV(Filename);
        }

        public List<User> Load()
        {
            Users = Serializer.fromCSV(Filename);
            return Users;
        }

        public void Save(List<User> users)
        {
            Serializer.toCSV(Filename, users);
        }
    }
}
