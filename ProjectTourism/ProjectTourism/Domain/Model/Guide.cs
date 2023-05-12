using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Guide : User, Serializable
    {
        public bool? IsSuperGuide;
        public bool HasTourStarted;
        public string Biography;
        public string Language;
        public string Localization;
        public List<Tour> Tours;
        public List<TourAppointment> TourAppointments;

        public Guide()
        {
            IsSuperGuide = false;
            HasTourStarted = false;
            Tours = new List<Tour>();
            TourAppointments = new List<TourAppointment>();
        }
        public Guide(User user)
        {
            this.Username = user.Username;
            this.Password = user.Password;
            this.Type = user.Type;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Birthday = user.Birthday;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;

            IsSuperGuide = false;
            Biography = "";
            Language = "";
            HasTourStarted = false;
            Tours = new List<Tour>();
            TourAppointments = new List<TourAppointment>();
        }
        public new string?[] ToCSV()
        {
            string?[] csvValues =
            {
                 Username, Biography, Language, HasTourStarted.ToString(), Localization
            };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Username = values[0];
            Biography = values[1];
            Language = values[2];
            HasTourStarted = bool.Parse(values[3]);
            Localization = values[4];
        }
    }
}
