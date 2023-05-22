using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class ComplexTourRepository : IComplexTourRepository
    {
        public ComplexTourFileHandler FileHandler { get; set; }
        public List<ComplexTour> ComplexTours { get; set; }
        public ComplexTourRepository()
        {
            FileHandler = new ComplexTourFileHandler();
            ComplexTours = FileHandler.Load();
        }
        public ComplexTour GetOne(int id)
        {
            foreach (var complexTour in ComplexTours)
            {
                if (complexTour.Id == id) return complexTour;
            }
            return null;
        }
        public List<ComplexTour> GetAll()
        {
            return ComplexTours;
        }
        public void Add(ComplexTour complexTour)
        {
            ComplexTours.Add(complexTour);
            FileHandler.Save(ComplexTours);
        }
        public void Delete(ComplexTour complexTour)
        {
            ComplexTours.Remove(complexTour);
            FileHandler.Save(ComplexTours);
        }
        public void Update(ComplexTour complexTour)
        {

        }
        public List<ComplexTour> GetAllByGuest(Guest2 guest2)
        {
            List<ComplexTour> complexTours = new List<ComplexTour>();
            foreach(var complexTour in ComplexTours)
            {
                if (complexTour.Guest2Username.Equals(guest2.Username))
                    complexTours.Add(complexTour);
            }
            return complexTours;
        }

    }
}
