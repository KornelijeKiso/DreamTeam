using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class RouteDateFileHandler
    {
        private Serializer<RouteDate> Serializer;
        private readonly string Filename = "../../../References/routeDate.csv";
        private List<RouteDate> RouteDates;

        public RouteDateFileHandler()
        {
            Serializer = new Serializer<RouteDate>();
            RouteDates = Serializer.fromCSV(Filename);
        }

        public List<RouteDate> Load()
        {
            RouteDates = Serializer.fromCSV(Filename);
            return RouteDates;
        }

        public void Save(List<RouteDate> routeDates)
        {
            Serializer.toCSV(Filename, routeDates);
        }
    }
}
