using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectTourism.Model
{
    public class AccommodationGrade : Serializable
    {
        public int Id;
        public int ReservationId;
        public static readonly string[] CategoryNames = { "Cleanness", "Comfort", "Hospitality", "Price and quality ratio", "Location" };
        public string Comment;
        public Reservation Reservation;
        public Dictionary<string, int> Grades;
        public string PictureURLs;

        public AccommodationGrade()
        {
            Grades = new Dictionary<string, int>();
            foreach (var category in CategoryNames)
            {
                Grades.Add(category, 0);
            }
        }
        
        public string[] ToCSV()
        {
            List<string> csv = new List<string>();
            csv.Add(Id.ToString());
            csv.Add(ReservationId.ToString());
            csv.Add(Comment);
            csv.Add(PictureURLs);
            foreach (var category in CategoryNames)
            {
                csv.Add(Grades[category].ToString());
            }
            string[] csvValues = csv.ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Comment = values[2];
            PictureURLs = values[3];
            for (int i = 4; i < values.Length; i++)
            {
                Grades[CategoryNames[i - 4]] = int.Parse(values[i]);
            }
        }
    }
}
