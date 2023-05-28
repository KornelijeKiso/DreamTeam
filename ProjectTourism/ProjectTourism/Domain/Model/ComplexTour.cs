using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.Model
{
    public class ComplexTour : Serializable
    {
        public int Id;
        public string Guest2Username;
        public string TourRequestString;
        public List<TourRequest> TourRequests;

        public ComplexTour() { }
        public ComplexTour(string username, string tourRequestString) 
        {
            Guest2Username = username;
            TourRequestString = tourRequestString;
            TourRequests = new List<TourRequest>();
        }
        public ComplexTour(string username, List<TourRequest> tourRequests)
        {
            Guest2Username = username;
            TourRequests = tourRequests;
            TourRequestString = "";
            foreach (var tourRequest in tourRequests)
            {
                TourRequestString += tourRequest.Id.ToString() + ", ";
            }
        }

        public new string[] ToCSV()
        {
            string[] csvValues =
            {   Id.ToString(), Guest2Username, TourRequestString    };
            return csvValues;
        }

        public new void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Guest2Username = values[1];
            TourRequestString = values[2];
            TourRequests = new List<TourRequest>();
        }
    }
}
