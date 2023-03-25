using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    internal class AccommodationGradeFileHandler
    {
        private Serializer<AccommodationGrade> Serializer;
        private readonly string Filename = "../../../References/accommodationGrades.csv";
        private List<AccommodationGrade> AccommodationGrades;

        public AccommodationGradeFileHandler()
        {
            Serializer = new Serializer<AccommodationGrade>();
        }

        public List<AccommodationGrade> Load()
        {
            AccommodationGrades = Serializer.fromCSV(Filename);
            return AccommodationGrades;
        }

        public void Save(List<AccommodationGrade> accommodationGrades)
        {
            Serializer.toCSV(Filename, accommodationGrades);
        }
    }
}
