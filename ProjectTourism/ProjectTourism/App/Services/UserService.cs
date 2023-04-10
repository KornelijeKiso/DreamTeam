using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
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
        public List<IObserver> Observers;
        private IUserRepository UserRepo;
        public UserService(IUserRepository iur)
        {
            UserRepo = iur;
            Observers = new List<IObserver>();
        }
        public void Add(UserVM user)
        {
            UserRepo.Add(user.GetUser());
        }
        public UserVM Identify(UserVM userVM)
        {
            return new UserVM(UserRepo.Identify(userVM.GetUser()));
        }

        public bool UsernameAlreadyInUse(string username)
        {
            return UserRepo.UsernameAlreadyInUse(username);
        }
        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
            }
        }
    }
}
