using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Guide : Serializable
    {
        public bool? IsSuperGuide;
        public bool HasTourStarted;
        public string? Username;
        public string? Name;
        public string? Surname;
        public string? Biography;
        public string? Language;
        public string? Email;
        public string? Phone;
        public List<Tour> Tours;
        public List<TourAppointment> TourAppointments;
        
        public Guide()
        {
            IsSuperGuide = false;
            HasTourStarted = false;
            Tours = new List<Tour>();
            TourAppointments = new List<TourAppointment>();
        }
        public Guide(string username, string name, string surname, string biography, string language, string email, string phone)
        {
            IsSuperGuide = false;
            Username = Username;
            Name = name;
            Surname = surname;
            Biography = biography;
            Language = language;
            HasTourStarted = false;
            Tours = new List<Tour>();
            TourAppointments = new List<TourAppointment>();
            Email = email;
            Phone = phone;
        }
        public string?[] ToCSV()
        {
            string?[] csvValues =
            {
                Username, Name, Surname, Biography, Language, HasTourStarted.ToString(), Email, Phone
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Username = values[0];
            Name = values[1];
            Surname = values[2];
            Biography = values[3];
            Language = values[4];
            HasTourStarted = bool.Parse(values[5]);
            Email = values[6];
            Phone = values[7];
        }
    }
}
