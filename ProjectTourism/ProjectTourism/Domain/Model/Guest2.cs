using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Guest2 : Serializable
    {
        public string Username;
        public string FirstName;
        public string LastName;
        public int Age;
        public string Email;
        public string PhoneNumber;
        public Guest2()
        { }

        public Guest2(string username, string firstName, string lastName, int age, string email, string phoneNumber)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public string[] ToCSV()
        {
            string[] csvValues = 
            {
                Username,
                FirstName,
                LastName,
                Age.ToString(),
                Email,
                PhoneNumber     };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Username = values[0];
            FirstName = values[1];
            LastName = values[2];
            Age = int.Parse(values[3]);
            Email = values[4];
            PhoneNumber = values[5];
        }
    }
}
