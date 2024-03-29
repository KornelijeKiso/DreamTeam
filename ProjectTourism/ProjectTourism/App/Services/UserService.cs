﻿using ProjectTourism.Domain.IRepositories;
using ProjectTourism.DTO;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class UserService
    {
        private IUserRepository UserRepo;
        public UserService()
        {
            UserRepo = Injector.Injector.CreateInstance<IUserRepository>();
        }
        public void Add(UserDTO user)
        {
            UserRepo.Add(user.GetUser());
        }
        public UserDTO Identify(UserDTO userVM)
        {
            return new UserDTO(UserRepo.Identify(userVM.GetUser()));
        }

        public bool UsernameAlreadyInUse(string username)
        {
            return UserRepo.UsernameAlreadyInUse(username);
        }

        public User GetOne(string username)
        {
            return UserRepo.GetOne(username);
        }
    }
}
