using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;

namespace ProjectTourism.WPF.ViewModel
{
    public class CurrentUserVM : INotifyPropertyChanged
    {
        private User _current;

        public CurrentUserVM(User user)
        {
            LogoutCurrentUser();
            _current = user;
            LoginCurrentUser();
        }

        public CurrentUserVM(UserVM user)
        {
            LogoutCurrentUser();
            _current = user.GetUser();
            LoginCurrentUser();
        }

        private void LoginCurrentUser()
        {
            CurrentUserService currentUserService = new CurrentUserService(new CurrentUserRepository());
            currentUserService.Add(_current);
        }

        public void LogoutCurrentUser()
        {
            CurrentUserService currentUserService = new CurrentUserService(new CurrentUserRepository());
            currentUserService.Delete();
        }

        public User GetUser()
        {
            return _current;
        }



        public USERTYPE Type
        {
            get => _current.Type;
            set
            {
                if (value != _current.Type)
                {
                    _current.Type = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Username
        {
            get => _current.Username;
            set
            {
                if (value != _current.Username)
                {
                    _current.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => _current.Password;
            set
            {
                if (value != _current.Password)
                {
                    _current.Password = value;
                    OnPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get => _current.FirstName;
            set
            {
                if (value != _current.FirstName)
                {
                    _current.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => _current.LastName;
            set
            {
                if (value != _current.LastName)
                {
                    _current.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Birthday
        {
            get => _current.Birthday;
            set
            {
                if (value != _current.Birthday)
                {
                    _current.Birthday = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get => _current.Email;
            set
            {
                if (value != _current.Email)
                {
                    _current.Email = value;
                    OnPropertyChanged();
                }
            }
        }
        public string PhoneNumber
        {
            get => _current.PhoneNumber;
            set
            {
                if (value != _current.PhoneNumber)
                {
                    _current.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
