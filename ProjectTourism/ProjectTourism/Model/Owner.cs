using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Owner:Serializable
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double AverageGrade { get; set; }
        public Owner()
        {
            AverageGrade = 0;
        }
        public Owner(string username, string firstName, string lastName, string email)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
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
