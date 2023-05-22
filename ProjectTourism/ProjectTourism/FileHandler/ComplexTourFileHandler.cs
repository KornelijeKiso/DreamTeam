using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.FileHandler
{
    public class ComplexTourFileHandler
    {
        private Serializer<ComplexTour> Serializer;
        private readonly string Filename = "../../../References/complexTours.csv";
        private List<ComplexTour> ComplexTours;

        public ComplexTourFileHandler()
        {
            Serializer = new Serializer<ComplexTour>();
            ComplexTours = Serializer.fromCSV(Filename);
        }

        public List<ComplexTour> Load()
        {
            ComplexTours = Serializer.fromCSV(Filename);
            return ComplexTours;
        }

        public void Save(List<ComplexTour> ComplexTours)
        {
            Serializer.toCSV(Filename, ComplexTours);
        }
    }
}
