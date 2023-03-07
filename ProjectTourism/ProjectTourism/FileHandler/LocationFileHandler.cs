using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class LocationFileHandler
    {
        private Serializer<Location> Serializer;
        private readonly string Filename = "../../../References/locations.csv";
        private List<Location> Locations;

        public LocationFileHandler()
        {
            Serializer = new Serializer<Location>();
            Locations = Serializer.fromCSV(Filename);
        }

        public List<Location> Load()
        {
            Locations = Serializer.fromCSV(Filename);
            return Locations;
        }

        public void Save(List<Location> locations)
        {
            Serializer.toCSV(Filename, locations);
        }
    }
}
