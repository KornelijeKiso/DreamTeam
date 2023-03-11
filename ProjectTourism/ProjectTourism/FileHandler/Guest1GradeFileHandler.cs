using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class Guest1GradeFileHandler
    {
        private Serializer<Guest1Grade> Serializer;
        private readonly string Filename = "../../../References/guests1grades.csv";
        private List<Guest1Grade> Guest1Grades;

        public Guest1GradeFileHandler()
        {
            Serializer = new Serializer<Guest1Grade>();
            Guest1Grades = Serializer.fromCSV(Filename);
        }

        public List<Guest1Grade> Load()
        {
            Guest1Grades = Serializer.fromCSV(Filename);
            return Guest1Grades;
        }

        public void Save(List<Guest1Grade> guest1grades)
        {
            Serializer.toCSV(Filename, guest1grades);
        }
    }
}
