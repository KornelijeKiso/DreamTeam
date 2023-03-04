using ProjectTourism.Model;
using ProjectTourism.Repository;
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
        public UserRepository Repository { get; set; }
        public List<User> Users { get; set; }
        public UserDAO() 
        { 
            Repository= new UserRepository();
            Users = Repository.GetAll();
        }
        public void Add(User addingUser)
        {
            if (!UsernameAlreadyInUse(addingUser.Username))
            {
                Users.Add(addingUser);
                Repository.Update(Users);
            }
        }
        public User Identify(User user)
        {
            Users = Repository.GetAll();
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
