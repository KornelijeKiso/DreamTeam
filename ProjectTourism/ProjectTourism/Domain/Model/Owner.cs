using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Owner:Serializable
    {
        public string Username;
        public string FirstName;
        public string LastName;
        public string Email;
        public double AverageGrade;
        public List<Accommodation> Accommodations;
        public List<Reservation> Reservations;
        public bool IsSuperHost;
        public int NumberOfReviews;
        public Owner()
        {
            AverageGrade = 0;
            Accommodations = new List<Accommodation>();
            Reservations = new List<Reservation>();
            IsSuperHost = false;
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
            Accommodations = new List<Accommodation>();
            Reservations = new List<Reservation>();
        }
    }
}
