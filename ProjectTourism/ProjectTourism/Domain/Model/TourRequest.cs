using System;
using System.Globalization;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Domain.Model
{
    public enum REQUESTSTATE { PENDING, ACCEPTED, EXPIRED }
    public class TourRequest : Serializable
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
        public REQUESTSTATE State;
        public DateTime CreationDateTime;
        public TourRequest() { }
        public TourRequest(TourRequest tourRequest)
        {
            Id = tourRequest.Id;
            Location = tourRequest.Location;
            Description = tourRequest.Description;
            Language = tourRequest.Language;
            NumberOfGuests = tourRequest.NumberOfGuests;
            StartDate = tourRequest.StartDate;
            EndDate = tourRequest.EndDate;
            Guest2Username = tourRequest.Guest2Username;
            State = tourRequest.State;
            CreationDateTime = tourRequest.CreationDateTime;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            LocationId = int.Parse(values[1]);
            Description = values[2];
            Language = values[3];
            NumberOfGuests = int.Parse(values[4]);
            
            if (DateOnly.TryParse(values[5], new CultureInfo("en-GB"), DateTimeStyles.None, out var startDateTimeParsed))
                StartDate = startDateTimeParsed;
            if (DateOnly.TryParse(values[6], new CultureInfo("en-GB"), DateTimeStyles.None, out var endDateTimeParsed))
                EndDate = endDateTimeParsed;

            Guest2Username = values[7];
            switch (values[8])
            {
                case "PENDING": State = REQUESTSTATE.PENDING; break;
                case "ACCEPTED": State = REQUESTSTATE.ACCEPTED; break;
                case "EXPIRED": State = REQUESTSTATE.EXPIRED; break;
                default: State = REQUESTSTATE.PENDING;break;
            }
            
            if (DateTime.TryParse(values[9], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                CreationDateTime = dateTimeParsed;
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
                StartDate.ToString("dd.MM.yyyy"),
                EndDate.ToString("dd.MM.yyyy"),
                Guest2Username, 
                State.ToString(), 
                CreationDateTime.ToString("dd.MM.yyyy HH:mm")
            };
            return csvValues;
        }
    }
}
