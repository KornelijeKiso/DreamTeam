using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Guest1 : User, Serializable
    {
        public double AverageGrade;
        public List<Reservation> Reservations;
        public int Points;
        public Guest1()
        {
            AverageGrade = 0;
            Reservations = new List<Reservation>();
        }
        public Guest1(User user)
        {
            this.Username = user.Username;
            this.Password = user.Password;
            this.Type = user.Type;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Birthday = user.Birthday;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;

            this.AverageGrade = 0;
            this.Reservations = new List<Reservation>();
        }
        public new string[] ToCSV()
        {
            string[] csvValues =
            {
                Username,
                AverageGrade.ToString(),
                Points.ToString()
            };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Username = values[0];
            AverageGrade = double.Parse(values[1]);
            Points = int.Parse(values[2]);
        }
    }
}

