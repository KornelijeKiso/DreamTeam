using ProjectTourism.Domain.Model;
using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Model
{
    public class Reservation: Serializable
    {
        public int Id;
        public DateOnly StartDate;
        public DateOnly EndDate;
        public DateOnly GradingDeadline;
        public Accommodation Accommodation;
        public int AccommodationId;
        public bool AccommodationGraded;
        public string Guest1Username;
        public Guest1 Guest1;
        public bool Graded;
        public bool CanBeGraded;
        public bool VisibleReview;
        public string GradingDeadlineMessage;
        public AccommodationGrade AccommodationGrade;
        public Guest1Grade Guest1Grade;
        public PostponeRequest PostponeRequest;
        public Reservation()
        {

        }
        public Reservation(Reservation r)
        {
            Id = r.Id;
            GradingDeadline = r.GradingDeadline;
            Accommodation = r.Accommodation;
            AccommodationId = r.AccommodationId;
            AccommodationGraded = r.AccommodationGraded;
            Guest1Username = r.Guest1Username;
            Guest1 = r.Guest1;
            Graded = r.Graded;
            CanBeGraded = r.CanBeGraded;
            VisibleReview = r.VisibleReview;
            GradingDeadlineMessage = r.GradingDeadlineMessage;
            AccommodationGrade = r.AccommodationGrade;
            Guest1Grade = r.Guest1Grade;
            PostponeRequest = r.PostponeRequest;
    }
        public bool IsAbleToGrade()
        {
            return DateOnly.FromDateTime(DateTime.Now) > EndDate && DateOnly.FromDateTime(DateTime.Now) < GradingDeadline;
        }
        public string[] ToCSV()
        {
            
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString("dd.MM.yyyy"),
                EndDate.ToString("dd.MM.yyyy"),
                Guest1Username,       };
            return csvValues;
        }
        
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            if(DateOnly.TryParse(values[2],new CultureInfo("en-GB"),DateTimeStyles.None,out var startDate)) StartDate = startDate;
            if (DateOnly.TryParse(values[3], new CultureInfo("en-GB"), DateTimeStyles.None, out var endDate)) EndDate = endDate;
            Guest1Username = values[4];
            GradingDeadline = EndDate.AddDays(5);
        }
    }
}
