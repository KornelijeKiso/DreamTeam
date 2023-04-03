using ProjectTourism.ModelDAO;
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
    public class Guest1Grade:Serializable
    {
        public int Id;
        public int ReservationId;
        public static readonly string[] CategoryNames = { "Cleanness","Communication", "Following the rules"};
        public string Comment;
        public Reservation Reservation;
        public Dictionary<string, int> Grades;

        public Guest1Grade()
        {
            Grades = new Dictionary<string, int>();
            foreach(var category in CategoryNames)
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
            for(int i = 3; i < values.Length; i++)
            {
                Grades[CategoryNames[i - 3]] = int.Parse(values[i]);
            }
        }
    }
}
