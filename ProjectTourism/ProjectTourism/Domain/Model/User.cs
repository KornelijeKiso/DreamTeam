using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public enum USERTYPE{ OWNER, GUIDE, GUEST1, GUEST2 };
    public class User:Serializable
    {
        public USERTYPE Type;
        
        public string Username;
        public string Password;
        public string FirstName;
        public string LastName;
        public DateTime Birthday;
        public string Email;
        public string PhoneNumber;

        public User()
        {

        }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User(string username, string password, string firstName, string lastName, DateTime birthday, string email, string phoneNumber)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Email = email;
            PhoneNumber = phoneNumber;
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
                type.ToString(),
                FirstName,
                LastName,
                Birthday.ToString("dd.MM.yyyy HH:mm"),
                Email,
                PhoneNumber     };
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
            FirstName = values[3];
            LastName = values[4];
            if (DateTime.TryParse(values[5], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                Birthday = dateTimeParsed;
            Email = values[6];
            PhoneNumber = values[7];
        }
    }
}
