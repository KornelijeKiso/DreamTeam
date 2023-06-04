using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class UserDTO: INotifyPropertyChanged
    {
        private User _user;
        public UserDTO(User u)
        {
            _user = u;
        }

        public User GetUser()
        {
            return _user;
        }

        public UserDTO(string username)
        {
            UserService userService = new UserService();
            _user = userService.GetOne(username);
        }
        public void CreateOwner()
        {
            UserService userService = new UserService();
            OwnerService ownerService = new OwnerService();
            OwnerDTO Owner = new OwnerDTO(new Model.Owner(GetUser()));
            userService.Add(this);
            ownerService.Add(Owner.GetOwner());
        }

        public void Add(UserDTO user)
        {
            UserService userService = new UserService();
            userService.Add(user);
        }
        public bool IsUsernameAlreadyInUse(string username)
        {
            UserService userService = new UserService();
            return userService.UsernameAlreadyInUse(username);
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

        public DateTime Birthday
        {
            get => _user.Birthday;
            set
            {
                if (value != _user.Birthday)
                {
                    _user.Birthday = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _user.Email;
            set
            {
                if (value != _user.Email)
                {
                    _user.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => _user.PhoneNumber;
            set
            {
                if (value != _user.PhoneNumber)
                {
                    _user.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Username is required!";
                }
                else if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName))
                        return "Name is required!";
                }
                else if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName))
                        return "Last Name is required!";
                }
                else if (columnName == "Birthday")
                {
                    if (string.IsNullOrEmpty(Birthday.ToString()))
                        return "Birthday is required!";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "Email is required!";
                }
                else if (columnName == "PhoneNumber")
                {
                    if (string.IsNullOrEmpty(PhoneNumber))
                        return "Phone Number is required!";
                }

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Username",/* "Password",*/ "FirstName", "LastName", "Birthday", "Email", "PhoneNumber" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }

    }
}
