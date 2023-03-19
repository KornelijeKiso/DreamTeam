using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.ModelDAO
{
    public class TourDAO
    {
        public List<IObserver> Observers;
        public TourFileHandler TourFileHandler { get; set; }
        public List<Tour> Tours { get; set; }
        public TourDAO()
        {
            TourFileHandler = new TourFileHandler();
            Tours = TourFileHandler.Load();
            Observers = new List<IObserver>();
        }
        public int GenerateId()
        {
            if(Tours.Count == 0) return 0;
            return Tours.Last<Tour>().Id + 1;
        }
        public void Add(Tour addedTour)
        {
            if (addedTour == null)
            {
                addedTour.Id = 0;
            }
            else
            {
                addedTour.Id = GenerateId();
                Tours.Add(addedTour);
                TourFileHandler.Save(Tours);
            }
        }
        public List<Tour> GetAll()
        {
            return Tours;
        }
        public Tour? GetOne(int id)
        {
            foreach(var tour in Tours)
            {
                if (tour.Id == id) return tour;
            }
            return null;
        }
        public Tour? Identify(Tour tour)
        {
            Tours = TourFileHandler.Load();
            foreach(var existingTour in Tours)
            {
                if(existingTour.Id == tour.Id)
                {
                    return existingTour;
                }
            }
            return null;
        }
        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
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
