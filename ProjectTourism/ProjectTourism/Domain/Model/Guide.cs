﻿using System.Collections.Generic;

namespace ProjectTourism.Model
{
    public class Guide : User, Serializable
    {
        public bool? IsSuperGuide;
        public string Biography;
        public string Language;
        public string Localization;
        public List<Tour> Tours;
        public List<TourAppointment> TourAppointments;

        public Guide()
        {
            IsSuperGuide = false;
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
            Tours = new List<Tour>();
            TourAppointments = new List<TourAppointment>();
        }
        public new string?[] ToCSV()
        {
            string?[] csvValues =
            {
                 Username, Biography, Language, Localization
            };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Username = values[0];
            Biography = values[1];
            Language = values[2];
            Localization = values[3];
        }
    }
}
