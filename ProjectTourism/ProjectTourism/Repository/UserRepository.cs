using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repository
{
    public class UserRepository
    {
        private Serializer<User> Serializer;
        private readonly string Filename = "../../../References/users.csv";
        private List<User> Users;

        public UserRepository()
        {
            Serializer = new Serializer<User>();
            Users = Serializer.fromCSV(Filename);
        }

        public User GetOne(string username)
        {
            foreach(User user in Users)
            {
                if (user.Username.Equals(username))
                {
                    return user;
                }
            }
            return null;
        }

        public List<User> GetAll()
        {
            Users = Serializer.fromCSV(Filename);
            return Users;
        }

        public void Update(List<User> users)
        {
            Serializer.toCSV(Filename, users);
        }
    }
}
