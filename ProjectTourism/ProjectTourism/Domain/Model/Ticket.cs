using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjectTourism.Model
{
    public class Ticket: Serializable
    {
        public int Id;
        public SolidColorBrush ButtonColor;
        public int TourAppointmentId;
        public TourAppointment TourAppointment;
        public string TourStop;
        public string Guest2Username;
        public Guest2 Guest2;
        public int NumberOfGuests;
        public bool HasGuideChecked;
        public bool HasGuestConfirmed;
        //public int TicketGradeId;
        public TicketGrade TicketGrade;
        public bool HasVoucher;
        public Ticket() { HasVoucher = false; }

        public Ticket(int id, int tourAppId, string tourStop, string guest2Username, int numberOfGuests)
        {
            Id = id;
            TourAppointmentId = tourAppId;
            TourStop = tourStop;
            Guest2Username = guest2Username;
            NumberOfGuests = numberOfGuests;
            HasGuideChecked = false;
            HasGuestConfirmed = false;
        }

        public Ticket(int tourAppId, string tourStop, string guest2Username, int numberOfGuests)
        {
            TourAppointmentId = tourAppId;
            TourStop = tourStop;
            Guest2Username = guest2Username;
            NumberOfGuests = numberOfGuests;
            HasGuideChecked = false;
            HasGuestConfirmed = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourAppointmentId.ToString(),
                Guest2Username,
                NumberOfGuests.ToString(),
                TourStop,
                HasGuideChecked.ToString(),
                HasGuestConfirmed.ToString()
                //TicketGradeId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourAppointmentId = int.Parse(values[1]);
            Guest2Username = values[2];
            NumberOfGuests = int.Parse(values[3]);
            TourStop = values[4];
            HasGuideChecked = bool.Parse(values[5]);
            HasGuestConfirmed= bool.Parse(values[6]);
            //TicketGradeId = int.Parse(values[7]);

            if (HasGuestConfirmed)
                ButtonColor = Brushes.Green;
            else if (HasGuideChecked)
                ButtonColor = Brushes.IndianRed;
           
        }
    }
}
