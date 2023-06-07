namespace ProjectTourism.Model
{
    public class Ticket: Serializable
    {
        public int Id;
        public int TourAppointmentId;
        public TourAppointment TourAppointment;
        public string TourStop;
        public string Guest2Username;
        public Guest2 Guest2;
        public int NumberOfGuests;
        public bool HasGuideChecked;
        public bool HasGuestConfirmed;
        public TicketGrade TicketGrade;
        public bool HasVoucher;
        public Ticket() 
        { 
            HasVoucher = false; 
            NumberOfGuests = 1;
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
        }
    }
}
