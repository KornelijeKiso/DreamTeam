using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class CurrentUserService
    {
        private ICurrentUserRepository CurrentUserRepo;
        public CurrentUserService()
        {
            CurrentUserRepo = Injector.Injector.CreateInstance<ICurrentUserRepository>();
        }
        public void Add(User user)
        {
            CurrentUserRepo.Add(user);
        }
        public User GetUser()
        {
            return CurrentUserRepo.GetUser();
        }
        public void Delete()
        {
            CurrentUserRepo.Delete();
        }
    }
}
