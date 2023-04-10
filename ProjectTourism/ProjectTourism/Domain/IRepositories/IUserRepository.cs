using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User Identify(User user);
        bool UsernameAlreadyInUse(string username);
    }
}
