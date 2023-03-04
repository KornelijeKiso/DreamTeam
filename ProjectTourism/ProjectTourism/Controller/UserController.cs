using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Controller
{
    public class UserController
    {
        public UserDAO UserDAO { get; set; }
        public UserController()
        {
            UserDAO = new UserDAO();
        }
        public List<User> GetAll()
        {
            return UserDAO.Users;
        }
        public void Add(User user)
        {
             UserDAO.Add(user);
        }

        public User Identify(User user)
        {
            return UserDAO.Identify(user);
        }

        public bool UsernameAlreadyInUse(string username)
        {
            return UserDAO.UsernameAlreadyInUse(username);
        }
    }
}
