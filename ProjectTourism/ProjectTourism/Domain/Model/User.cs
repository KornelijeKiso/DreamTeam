using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public enum USERTYPE{ OWNER, GUIDE, GUEST1, GUEST2 };
    public class User:Serializable, INotifyPropertyChanged
    {
        private USERTYPE _Type;
        public USERTYPE Type
        {
            get => _Type;
            set
            {
                if (value != _Type)
                {
                    _Type = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Username;
        public string Username
        {
            get => _Username;
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                    OnPropertyChanged();
                }
            }
        }
        public User(string username, string password, USERTYPE type)
        {
            this.Username = username;
            this.Password = password;
            this.Type = type;  
        }
        public User() {
            this.Username = "";
            this.Password = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            int type;
            switch (Type)
            {
                case USERTYPE.OWNER: { type = 0; break; }
                case USERTYPE.GUIDE: { type = 1; break; }
                case USERTYPE.GUEST1: { type = 2; break; }
                case USERTYPE.GUEST2: { type = 3; break; }
                default: { type = 2; break; }
            }
            string[] csvValues =
            {
                Username,
                Password,
                type.ToString()          };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Username = values[0];
            Password = values[1];
            int type1 = int.Parse(values[2]);
            switch(type1) 
            {
                case 0: { Type = USERTYPE.OWNER; break; }
                case 1: { Type = USERTYPE.GUIDE; break; }
                case 2: { Type = USERTYPE.GUEST1; break;}
                case 3: { Type = USERTYPE.GUEST2; break;}
            }
        }
    }
}
