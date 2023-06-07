using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Repositories
{
    public class ComplexTourRepository : IComplexTourRepository
    {
        public ComplexTourFileHandler FileHandler { get; set; }
        public List<ComplexTour> ComplexTourRequests { get; set; }
        public ComplexTourRepository()
        {
            FileHandler = new ComplexTourFileHandler();
            ComplexTourRequests = FileHandler.Load();
        }
        private int GenerateId()
        {
            if (ComplexTourRequests == null || ComplexTourRequests.Count == 0) return 0;
            else return ComplexTourRequests.Last().Id + 1;
        }
        public ComplexTour GetOne(int id)
        {
            foreach (var complexTour in ComplexTourRequests)
            {
                if (complexTour.Id == id) return complexTour;
            }
            return null;
        }
        public List<ComplexTour> GetAll()
        {
            return ComplexTourRequests;
        }
        public void Add(ComplexTour complexTour)
        {
            complexTour.Id = GenerateId();
            ComplexTourRequests.Add(complexTour);
            FileHandler.Save(ComplexTourRequests);
        }
        public void Delete(ComplexTour complexTour)
        {
            ComplexTourRequests.Remove(complexTour);
            FileHandler.Save(ComplexTourRequests);
        }
        public void Update(ComplexTour complexTour)
        {

        }
        public List<ComplexTour> GetAllByGuest(Guest2 guest2)
        {
            List<ComplexTour> complexTours = new List<ComplexTour>();
            foreach(var complexTour in ComplexTourRequests)
            {
                if (complexTour.Guest2Username.Equals(guest2.Username))
                    complexTours.Add(complexTour);
            }
            return complexTours;
        }

        public void UpdateTourRequest(TourRequest tourRequest)
        {
            throw new NotImplementedException();
        }
    }
}
