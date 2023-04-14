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
    public class UserVM : INotifyPropertyChanged , IDataErrorInfo
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

        public string FirstName
        {
            get => _user.FirstName;
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
            get => _user.LastName;
            set
            {
                if (value != _user.LastName)
                {
                    _user.LastName = value;
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
                //else if (columnName == "Password")
                //{
                //    if (string.IsNullOrEmpty(Password))
                //        return "Password is required!";
                //}
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
