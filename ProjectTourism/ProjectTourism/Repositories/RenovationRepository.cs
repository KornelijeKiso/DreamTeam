using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class RenovationRepository : IRenovationRepository
    {
        public RenovationFileHandler FileHandler { get; set; }
        public List<Renovation> Renovations { get; set; }
        public RenovationRepository() 
        {
            FileHandler= new RenovationFileHandler();
            Renovations = FileHandler.Load();
        }
        private int GenerateId()
        {
            if (Renovations == null) return 0;
            else return Renovations.Last().Id + 1;
        }
        public int AddAndReturnId(Renovation renovation)
        {
            renovation.Id = GenerateId();
            Renovations.Add(renovation);
            FileHandler.Save(Renovations);
            return renovation.Id;
        }

        public void Delete(Renovation renovation)
        {
            Renovations.Remove(GetOne(renovation.Id));
            FileHandler.Save(Renovations);
        }

        public List<Renovation> GetAll()
        {
            return Renovations;
        }

        public List<Renovation> GetAllByAccommodation(int accommodationId)
        {
            return Renovations.FindAll(r=>r.AccommodationId==accommodationId);
        }

        public Renovation GetOne(int renovationId)
        {
            return Renovations.Find(r => r.Id == renovationId);
        }
    }
}
