using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using System.Text.RegularExpressions;

namespace ProjectTourism.WPF.ViewModel
{
    public class Guest2VM : INotifyPropertyChanged, IDataErrorInfo
    {
        private Guest2 _guest2;

        public Guest2VM(Guest2 guest2)
        {
            _guest2 = guest2;
            Tickets = _guest2.Tickets.Select(r => new TicketVM(r)).ToList();
            Vouchers = _guest2.Vouchers.Select(r => new VoucherVM(r)).ToList();
        }

        public Guest2VM(string username)
        {
            Synchronize(username);
        }

        public void Synchronize(string username)
        {
            Guest2Service guest2Service = new Guest2Service(new Guest2Repository());
            _guest2 = guest2Service.GetOne(username);

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
        public string Password
        {
            get => _guest2.Password;
            set
            {
                if (value != _guest2.Password)
                {
                    _guest2.Password = value;
                    OnPropertyChanged();
                }
            }
        }
        public USERTYPE Type
        {
            get => _guest2.Type;
            set
            {
                if (value != _guest2.Type)
                {
                    _guest2.Type = value;
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
        public DateTime Birthday
        {
            get => _guest2.Birthday;
            set
            {
                if (value != _guest2.Birthday)
                {
                    _guest2.Birthday = value;
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

        public List<TicketVM> Tickets;
        public List<VoucherVM> Vouchers;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // validation
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Username is required!";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Username" };

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
