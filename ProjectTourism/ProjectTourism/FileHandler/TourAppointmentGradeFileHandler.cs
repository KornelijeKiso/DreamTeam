using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class TourAppointmentGradeFileHandler
    {
        private Serializer<TourAppointmentGrade> Serializer;
        private readonly string Filename = "../../../References/tourAppointmentGrades.csv";
        private List<TourAppointmentGrade> TourAppointmentGrades;

        public TourAppointmentGradeFileHandler()
        {
            Serializer = new Serializer<TourAppointmentGrade>();
        }

        public List<TourAppointmentGrade> Load()
        {
            TourAppointmentGrades = Serializer.fromCSV(Filename);
            return TourAppointmentGrades;
        }

        public void Save(List<TourAppointmentGrade> tourAppGrades)
        {
            Serializer.toCSV(Filename, tourAppGrades);
        }
    }
}
