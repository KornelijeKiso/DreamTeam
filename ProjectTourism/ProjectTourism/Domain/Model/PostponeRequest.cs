using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.Model
{
    public class PostponeRequest : Serializable
    {
        public int Id;
        public int ReservationId;
        public Reservation Reservation;
        public DateOnly NewStartDate;
        public DateOnly NewEndDate;
        public bool Accepted;
        public bool Rejected;
        public string AdditionalComment;
        public PostponeRequest()
        {
            
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                NewStartDate.ToString("dd.MM.yyyy"),
                NewEndDate.ToString("dd.MM.yyyy"),
                Accepted.ToString(),
                Rejected.ToString(),
                AdditionalComment     
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            if (DateOnly.TryParse(values[2], new CultureInfo("en-GB"), DateTimeStyles.None, out var startDate)) NewStartDate = startDate;
            if (DateOnly.TryParse(values[3], new CultureInfo("en-GB"), DateTimeStyles.None, out var endDate)) NewEndDate = endDate;
            Accepted = bool.Parse(values[4]);
            Rejected = bool.Parse(values[5]);
            AdditionalComment = values[6];
        }
    }
}
