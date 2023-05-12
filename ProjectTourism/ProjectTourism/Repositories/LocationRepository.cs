using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class LocationRepository: ILocationRepository
    {
        public LocationFileHandler FileHandler { get; set; }
        public List<Location> Locations { get; set; }
        public LocationRepository()
        {
            FileHandler = new LocationFileHandler();
            Locations = FileHandler.Load();
        }
        public int GenerateId(Location location)
        {
            if (Locations == null) return 0;
            else return Locations.Last().Id + 1;
        }
        public int AddAndReturnId(Location location)
        {
            Location l = Locations.Find(lo => lo.City.Equals(location.City) && lo.Country.Equals(location.Country));
            if (l != null) return l.Id;
            location.Id = GenerateId(location);
            Locations.Add(location);
            FileHandler.Save(Locations);
            return location.Id;
        }

        public void Delete(Location location)
        {
            Locations.Remove(location);
            FileHandler.Save(Locations);
        }

        public List<Location> GetAll()
        {
            return Locations;
        }

        public Location GetOne(int locationId)
        {
            return Locations.Find(l => l.Id == locationId);
        }

        public void Update(Location location)
        {
            foreach (var existingLocation in Locations)
            {
                if (existingLocation.Id == location.Id)
                {
                    existingLocation.City = location.City;
                    existingLocation.Country = location.Country;
                }
            }
        }

    }
}
