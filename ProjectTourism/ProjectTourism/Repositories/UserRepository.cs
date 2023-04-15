using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    internal class UserRepository:IUserRepository
    {
        public UserFileHandler FileHandler { get; set; }
        public List<User> Users { get; set; }
        public UserRepository()
        {
            FileHandler = new UserFileHandler();
            Users = FileHandler.Load();
        }
        public void Add(User addingUser)
        {
            if (!UsernameAlreadyInUse(addingUser.Username))
            {
                Users.Add(addingUser);
                FileHandler.Save(Users);
            }
        }
        public User Identify(User user)
        {
            Users = FileHandler.Load();
            foreach (var existingUser in Users)
            {
                if (user.Username.Equals(existingUser.Username) && user.Password.Equals(existingUser.Password))
                {
                    user.Type = existingUser.Type;
                    return user;
                }
            }
            return null;
        }

        public bool UsernameAlreadyInUse(string username)
        {
            foreach (var user in Users)
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
