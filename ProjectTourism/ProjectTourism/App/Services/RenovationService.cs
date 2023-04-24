using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class RenovationService
    {
        private IRenovationRepository RenovationRepo;
        public RenovationService()
        {
            RenovationRepo = Injector.Injector.CreateInstance<IRenovationRepository>();
        }
        public List<Renovation> GetAllByAccommodation(int accommodationId)
        {
            return RenovationRepo.GetAllByAccommodation(accommodationId);
        }
        public List<Renovation> GetAll()
        {
            return RenovationRepo.GetAll();
        }
        public void Cancel(Renovation renovation)
        {
            RenovationRepo.Delete(renovation);
        }
        public void Schedule(Renovation renovation)
        {
            RenovationRepo.Add(renovation);
        }
        public Renovation GetOne(int id)
        {
            return RenovationRepo.GetOne(id);
        }
    }
}
