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
    public class CurrentUserRepository : ICurrentUserRepository
    {
        public CurrentUserFileHandler FileHandler { get; set; }
        public List<User> User { get; set; }
        public CurrentUserRepository()
        {
            FileHandler = new CurrentUserFileHandler();
            User = FileHandler.Load();
            if (User.Count > 1)
            {
                throw new Exception("More than one user can't be logged on!");
            }
        }
        public void Add(User user)
        {
            User.Add(user);
            FileHandler.Save(User);
        }
        public User GetUser()
        {
            if (User.Count == 1)
            {
                return User[0];
            }
            return null;
        }
        public void Delete()
        {
            User.Clear();
            FileHandler.Save(User);
        }
    }
}
