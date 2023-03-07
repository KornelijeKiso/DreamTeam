using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;

namespace ProjectTourism.ModelDAO
{
    public class RouteDAO
    {
        public RouteFileHandler RouteFileHandler { get; set; }
        public List<Route> Routes { get; set; }
        public RouteDAO()
        {
            RouteFileHandler = new RouteFileHandler();
            Routes = RouteFileHandler.Load();
        }

        public int GenerateId()
        {
            return Routes[-1].Id + 1;
        }
        public void Add(Route addedRoute)
        {
            if (addedRoute == null)
            {
                addedRoute.Id = 0;
            }
            else
            {
                addedRoute.Id = GenerateId();
                Routes.Add(addedRoute);
                RouteFileHandler.Save(Routes);
            }
        }

        public Route? Identify(Route route)
        {
            Routes = RouteFileHandler.Load();
            foreach(var existingRoute in Routes)
            {
                if(existingRoute.Id == route.Id)
                {
                    return existingRoute;
                }
            }
            return null;
        }
    }
}
