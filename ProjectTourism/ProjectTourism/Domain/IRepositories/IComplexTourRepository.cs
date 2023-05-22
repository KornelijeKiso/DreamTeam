using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IComplexTourRepository
    {
        ComplexTour GetOne(int id);
        List<ComplexTour> GetAll();
        void Add(ComplexTour complexTour);
        void Delete(ComplexTour complexTour);
        void Update(ComplexTour complexTour);
        List<ComplexTour> GetAllByGuest(Guest2 guest2);
    }
}
