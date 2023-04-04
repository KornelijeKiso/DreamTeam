using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories.IRepositories
{
    public interface ILocationRepository
    {
        public Location GetOne(int locationId);
        public List<Location> GetAll();
        public int AddAndReturnId(Location location);
        public void Delete(Location location);
        public void Update(Location location);
    }
}
