using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.Domain.Model
{
    public class Request : Serializable
    {
        public int Id;
        public Location Location;
        public int LocationId;
        public string Description;
        public string Language;
        public int NumberOfGuests;
        public DateOnly StartDate;
        public DateOnly EndDate;
        public string Guest2Username;

        public Request() { }

        public Request(Request request)
        {
            Id = request.Id;
            Location = request.Location;
            Description = request.Description;
            Language = request.Language;
            NumberOfGuests = request.NumberOfGuests;
            StartDate = request.StartDate;
            EndDate = request.EndDate;
            Guest2Username = request.Guest2Username;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            LocationId = int.Parse(values[1]);
            Description = values[2];
            Language = values[3];
            NumberOfGuests = int.Parse(values[4]);
            StartDate = DateOnly.Parse(values[5]);
            EndDate = DateOnly.Parse(values[6]);
            Guest2Username = values[7];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                LocationId.ToString(),
                Description,
                Language,
                NumberOfGuests.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                Guest2Username
            };
            return csvValues;
        }
    }
}
