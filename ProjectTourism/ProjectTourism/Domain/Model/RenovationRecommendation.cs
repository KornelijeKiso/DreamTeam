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
    public class RenovationRecommendation : Serializable
    {
        public int Id;
        public int AccommodationGradeId;
        public AccommodationGrade AccommodationGrade;
        public string Comment;
        public int Level;

        public string[] ToCSV()
        {
            List<string> csv = new List<string>();
            csv.Add(Id.ToString());
            csv.Add(AccommodationGradeId.ToString());
            csv.Add(Comment);
            csv.Add(Level.ToString());
            string[] csvValues = csv.ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationGradeId = int.Parse(values[1]);
            Comment = values[2];
            Level = int.Parse(values[3]);
        }
    }
}
