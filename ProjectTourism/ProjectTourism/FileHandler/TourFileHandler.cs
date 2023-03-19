using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class TourFileHandler
    {
        private Serializer<Tour> Serializer;
        private readonly string FileName = "../../../References/tours.csv";
        private List<Tour> Tours;
        public TourFileHandler()
        {
            Serializer = new Serializer<Tour>();
            Tours = Serializer.fromCSV(FileName);
        }
        
        public List<Tour> Load()
        {
            Tours = Serializer.fromCSV(FileName);
            return Tours;
        }

        public void Save(List<Tour> tours)
        {
            Serializer.toCSV(FileName, tours);
        }
    }
}
