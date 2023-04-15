using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Guest1 : Serializable
    {
        public string Username;
        public string FirstName;
        public string LastName;
        public string Email;
        public double AverageGrade;
        public List<Reservation> Reservations;
        public Guest1()
        {
            AverageGrade = 0;
            Reservations = new List<Reservation>();
        }
        public Guest1(string username, string firstName, string lastName, string email)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Reservations = new List<Reservation>();
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Username,
                FirstName,
                LastName,
                Email,
                AverageGrade.ToString()     };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Username = values[0];
            FirstName = values[1];
            LastName = values[2];
            Email = values[3];
            AverageGrade = double.Parse(values[4]);
        }
    }
}

