using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.Model
{
    public class Guest2 : User, Serializable
    {
        public List<Voucher> Vouchers;
        public List<Ticket> Tickets;
        public List<TourRequest> TourRequests;
        public List<ComplexTour> ComplexTours;
        public Guest2()
        {
            Vouchers = new List<Voucher>();
            Tickets = new List<Ticket>();
            TourRequests = new List<TourRequest>();
            ComplexTours = new List<ComplexTour>();
        }

        public Guest2(User user)
        {
            this.Username = user.Username;
            this.Password = user.Password;
            this.Type = user.Type;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Birthday = user.Birthday;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;

            Vouchers = new List<Voucher>();
            Tickets = new List<Ticket>();
            TourRequests = new List<TourRequest>(); 
            ComplexTours = new List<ComplexTour>();
        }

        public new string[] ToCSV()
        {
            string[] csvValues = 
            {
                Username };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Username = values[0];
        }
    }
}
