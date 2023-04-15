using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Owner : User, Serializable
    {
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
        public Owner(User user)
        {
            this.Username = user.Username;
            this.Password = user.Password;
            this.Type = user.Type;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Birthday = user.Birthday;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;

            AverageGrade = 0;
            Accommodations = new List<Accommodation>();
            Reservations = new List<Reservation>();
            IsSuperHost = false;
            //NumberOfReviews = 0;
        }
        public new string[] ToCSV()
        {
            string[] csvValues =
            {
                Username,
                AverageGrade.ToString()     };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Username = values[0];
            AverageGrade = double.Parse(values[1]);
            Accommodations = new List<Accommodation>();
            Reservations = new List<Reservation>();
        }
    }
}
