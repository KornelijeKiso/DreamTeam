using ProjectTourism.Model;
using ProjectTourism.FileHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.ModelDAO
{
    public class UserDAO
    {
        public UserFileHandler Repository { get; set; }
        public List<User> Users { get; set; }
        public UserDAO() 
        { 
            Repository= new UserFileHandler();
            Users = Repository.Load();
        }
        public void Add(User addingUser)
        {
            if (!UsernameAlreadyInUse(addingUser.Username))
            {
                Users.Add(addingUser);
                Repository.Save(Users);
            }
        }
        public User Identify(User user)
        {
            Users = Repository.Load();
            foreach(var existingUser in Users)
            {
                if(user.Username.Equals(existingUser.Username) && user.Password.Equals(existingUser.Password))
                {
                    user.Type = existingUser.Type;
                    return user;
                }
            }
            return null;
        }

        public bool UsernameAlreadyInUse(string username)
        {
            foreach(var user in Users)
            {
                if (user.Username.Equals(username))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
