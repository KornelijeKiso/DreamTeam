using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class UserDTO:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private User _user;
        public UserDTO(User u)
        {
            _user = u;
        }
        public string Username
        {
            get=>_user.Username;
            set
            {
                if (value != _user.Username)
                {
                    _user.Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string FirstName
        {
            get=>_user.FirstName;
            set
            {
                if (value != _user.FirstName)
                {
                    _user.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get=>_user.LastName;
            set
            {
                if (value != _user.LastName)
                {
                    _user.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public USERTYPE Type
        {
            get=>_user.Type;
            set
            {
                if (value != _user.Type)
                {
                    _user.Type = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
