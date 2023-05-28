using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class ComplexTourService
    {
        private IComplexTourRepository ComplexTourRepo;
        public ComplexTourService()
        {
            ComplexTourRepo = Injector.Injector.CreateInstance<IComplexTourRepository>();
        }
        public ComplexTour GetOne(int id)
        {
            return ComplexTourRepo.GetOne(id);
        }
        public List<ComplexTour> GetAll()
        {
            return ComplexTourRepo.GetAll();
        }
        public void Add(ComplexTour complexTour)
        {
            ComplexTourRepo.Add(complexTour);
        }
        public void Delete(ComplexTour complexTour)
        {
            ComplexTourRepo.Delete(complexTour);
        }
        public void Update(ComplexTour complexTour)
        {
            ComplexTourRepo.Update(complexTour);
        }
        public List<ComplexTour> GetAllByGuest(Guest2 guest2)
        {
            return ComplexTourRepo.GetAllByGuest(guest2);
        }
    }
}
