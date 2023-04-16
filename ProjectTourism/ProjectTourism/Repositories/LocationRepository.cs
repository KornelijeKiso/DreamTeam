using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
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
        public int GenerateId()
        {
            int id = 0;
            if (Locations == null)
            {
                id = 0;
            }
            else
            {
                foreach (var location in Locations)
                {
                    id = location.Id + 1;
                }
            }
            return id;
        }
        public int AddAndReturnId(Location location)
        {
            location.Id = GenerateId();
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
            foreach (var location in Locations)
            {
                if (location.Id == locationId) return location;
            }
            return null;
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
