using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjectTourism.WPF.ViewModel
{
    public class Guest2VM : INotifyPropertyChanged, IDataErrorInfo
    {
        private Guest2 _guest2;

        public Guest2VM(Guest2 guest2)
        {
            _guest2 = guest2;
        }

        public Guest2 GetGuest2()
        {
            return _guest2;
        }

        public string Username
        {
            get => _guest2.Username;
            set
            {
                if (value != _guest2.Username)
                {
                    _guest2.Username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => _guest2.FirstName;
            set
            {
                if (value != _guest2.FirstName)
                {
                    _guest2.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => _guest2.LastName;
            set
            {
                if (value != _guest2.LastName)
                {
                    _guest2.LastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Age
        {
            get => _guest2.Age;
            set
            {
                if (value != _guest2.Age)
                {
                    _guest2.Age = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => _guest2.Email;
            set
            {
                if (value != _guest2.Email)
                {
                    _guest2.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get => _guest2.PhoneNumber;
            set
            {
                if (value != _guest2.PhoneNumber)
                {
                    _guest2.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // validation
        private Regex _PhoneNumRegex = new Regex("[0-9]{3}[/]{1}[0-9]{3}-[0-9]{3}");
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(FirstName))
                        return "Name is required!";
                }
                else if (columnName == "LastName")
                {
                    if (string.IsNullOrEmpty(LastName))
                        return "Last Name is required!";
                }
                else if (columnName == "Age")
                {
                    if (string.IsNullOrEmpty(Age.ToString()))
                        return "Age is required!";
                }
                else if (columnName == "PhoneNumber")
                {
                    if (string.IsNullOrEmpty(PhoneNumber))
                        return "Phone Number is required!";

                    if (PhoneNumber.Length != 11)
                        return "Phone number should be in format DDD/DDD-DDD";
                    Match match = _PhoneNumRegex.Match(PhoneNumber);
                    if (!match.Success)
                        return "Phone number should be in format DDD/DDD-DDD";//"have 10 digits;
                }

                return null;
            }
        }
        private readonly string[] _validatedProperties = { "FirstName", "LastName", "Age", "PhoneNumber" };

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
