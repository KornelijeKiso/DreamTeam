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
        public Tour? Identify(Tour tour)
        {
            foreach (var existingTour in Tours)
            {
                if (existingTour.Id == tour.Id)
                {
                    return existingTour;
                }
            }
            return null;
        }
        public int GenerateId()
        {
            if (Tours.Count == 0) return 0;
            return Tours.Last<Tour>().Id + 1;
        }
        public void Add(Tour tour)
        {
            tour.Id = GenerateId();
            List<string> pom = GetStops(tour);
            tour.StopsList = pom;
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
        public List<string> GetStops(Tour tour)
        {
            List<string> stops = new List<string>();
            string[] str = tour.Stops.Split(',');
            foreach (string s in str)
            {
                stops.Add(s.Trim());
            }
            stops.Insert(0, tour.Start);
            stops.Add(tour.Finish);
            return stops;
        }
    }
}
