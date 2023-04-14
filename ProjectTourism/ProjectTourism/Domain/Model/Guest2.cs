using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.Model
{
    public class Guest2 : User, Serializable
    {
        public List<Voucher> Vouchers;
        public List<Ticket> Tickets;
        public Guest2()
        {
            Vouchers = new List<Voucher>();
            Tickets = new List<Ticket>();
        }

        public Guest2(User user)
        {
            this.Username = user.Username;
            this.Password = user.Password;
            this.Type = user.Type;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;

            Vouchers = new List<Voucher>();
            Tickets = new List<Ticket>();
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
