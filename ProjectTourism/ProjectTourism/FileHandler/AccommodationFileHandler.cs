using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class AccommodationFileHandler
    {
        private Serializer<Accommodation> Serializer;
        private readonly string Filename = "../../../References/accommodations.csv";
        private List<Accommodation> Accommodations;

        public AccommodationFileHandler()
        {
            Serializer = new Serializer<Accommodation>();
            Accommodations = Serializer.fromCSV(Filename);
        }

        public List<Accommodation> Load()
        {
            Accommodations = Serializer.fromCSV(Filename);
            return Accommodations;
        }

        public void Save(List<Accommodation> accommodations)
        {
            Serializer.toCSV(Filename, accommodations);
        }
    }
}
