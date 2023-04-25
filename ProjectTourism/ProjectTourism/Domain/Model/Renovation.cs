using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.Model
{
    public class Renovation: Serializable
    {
        public int Id;
        public int AccommodationId;
        public Accommodation Accommodation;
        public DateOnly StartDate;
        public DateOnly EndDate;
        public string Description;
        public Renovation() { }
        public new string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString("dd.MM.yyyy"),
                EndDate.ToString("dd.MM.yyyy"),
                Description
            };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            if (DateOnly.TryParse(values[2], new CultureInfo("en-GB"), DateTimeStyles.None, out var startDate)) StartDate = startDate;
            if (DateOnly.TryParse(values[3], new CultureInfo("en-GB"), DateTimeStyles.None, out var endDate)) EndDate = endDate;
            Description= values[4];
        }
    }
}
