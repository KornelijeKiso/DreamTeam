using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class RouteLocationFileHandler
    {
        private Serializer<RouteLocation> Serializer;
        private readonly string Filename = "../../../References/routeLocations.csv";
        private List<RouteLocation> RouteLocations;

        public RouteLocationFileHandler()
        {
            Serializer = new Serializer<RouteLocation>();
            RouteLocations = Serializer.fromCSV(Filename);
        }

        public List<RouteLocation> Load()
        {
            RouteLocations = Serializer.fromCSV(Filename);
            return RouteLocations;
        }

        public void Save(List<RouteLocation> routeLocations)
        {
            Serializer.toCSV(Filename, routeLocations);
        }
    }
}
