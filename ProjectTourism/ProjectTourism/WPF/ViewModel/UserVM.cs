using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class UserVM : INotifyPropertyChanged
    {
        private User _user;

        public UserVM(User user)
        {
            _user = user;
        }
        public User GetUser()
        {
            return _user;
        }

        public USERTYPE Type
        {
            get => _user.Type;
            set
            {
                if (value != _user.Type)
                {
                    _user.Type = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Username
        {
            get => _user.Username;
            set
            {
                if (value != _user.Username)
                {
                    _user.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => _user.Password;
            set
            {
                if (value != _user.Password)
                {
                    _user.Password = value;
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
