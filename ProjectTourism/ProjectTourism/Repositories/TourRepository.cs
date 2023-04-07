using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Repositories
{
    public class TourRepository: ITourRepository
    {
        public TourFileHandler FileHandler { get; set; }
        public List<Tour> Tours { get; set; }
        public TourRepository()
        { 
            FileHandler= new TourFileHandler();
            Tours = FileHandler.Load();
            Synchronize();
        }

        public void Synchronize()
        {
        }
        public int GenerateId()
        {
            int id = 0;
            if (Tours == null)
                id = 0;
            else
                foreach (var tour in Tours)
                {
                    id = tour.Id + 1;
                }
            return id;
        }
        public void Add(Tour tour)
        {
            tour.Id = GenerateId();
            Tours.Add(tour);
            FileHandler.Save(Tours);
        }

        public void Delete(Tour tour)
        {
            Tours.Remove(tour);
            FileHandler.Save(Tours);
        }

        public List<Tour> GetAll()
        {
            return Tours;
        }

        public Tour GetOne(int tourId)
        {
            foreach(var tour in Tours)
            {
                if(tour.Id == tourId)
                    return tour;
            }
            return null;
        }

        public void Update(Tour tour)
        {
            foreach (var existingTour in Tours)
            {
                if (existingTour.Id == tour.Id)
                {
                    existingTour.Name = tour.Name;
                }
            }
        }
    }
}
