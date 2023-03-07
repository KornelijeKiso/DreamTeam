using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class RouteFileHandler
    {
        private Serializer<Route> Serializer;
        private readonly string FileName = "../../../References/routes.csv";
        private List<Route> Routes;
        public RouteFileHandler()
        {
            Serializer = new Serializer<Route>();
            Routes = Serializer.fromCSV(FileName);
        }
        
        public List<Route> Load()
        {
            Routes = Serializer.fromCSV(FileName);
            return Routes;
        }

        public void Save(List<Route> routes)
        {
            Serializer.toCSV(FileName, routes);
        }
    }
}
