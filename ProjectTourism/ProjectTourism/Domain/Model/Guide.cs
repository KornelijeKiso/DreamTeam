using System.Collections.Generic;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.Model
{
    public class Guide : User, Serializable
    {
        public bool? IsSuperGuide;
        public string Biography;
        public string Language;
        public string Localization;
        public bool DarkTheme;
        public List<Tour> Tours;
        public List<TourAppointment> TourAppointments;
        public List<ComplexTour> ComplexTours;

        public Guide()
        {
            IsSuperGuide = false;
            Tours = new List<Tour>();
            TourAppointments = new List<TourAppointment>();
            ComplexTours = new List<ComplexTour>();
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
            DarkTheme = false;
            Tours = new List<Tour>();
            TourAppointments = new List<TourAppointment>();
            ComplexTours = new List<ComplexTour>();
        }
        public new string?[] ToCSV()
        {
            string?[] csvValues =
            {
                 Username, Biography, Language, Localization, DarkTheme.ToString()
            };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Username = values[0];
            Biography = values[1];
            Language = values[2];
            Localization = values[3];
            DarkTheme = bool.Parse(values[4]);
        }
    }
}
