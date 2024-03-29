﻿using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.Services
{
    public class TourService
    {
        private ITourRepository TourRepository;

        public TourService()
        {
            TourRepository = Injector.Injector.CreateInstance<ITourRepository>();
        }
        public int AddAndReturnId(Tour tour)
        {
            return TourRepository.Add(tour);
        }
        public void Delete(Tour tour)
        {
            TourRepository.Delete(tour);
        }

        public List<string> LoadStops(Tour tour)
        {
            if(tour.Stops != null)
            {
                List<string> result = tour.Stops.Split(',').ToList();
                result.Add(tour.Finish);
                result.Insert(0, tour.Start);
                return result;
            }
            return null;
        }
        public Tour GetOne(int id)
        {
            return TourRepository.GetOne(id);
        }
        public List<Tour> GetAll()
        {
            return TourRepository.GetAll();
        }

        public Tour? Identify(Tour tour)
        {
            foreach (var existingTour in TourRepository.GetAll())
            {
                if (existingTour.Id == tour.Id)
                {
                    return existingTour;
                }
            }
            return null;
        }
        public List<string> GetStops(Tour tour)
        {
            return TourRepository.GetStops(tour);
        }
        public string GetNextStop(Tour tour, int checkpointIndex)
        {
            List<string> stops = GetStops(tour);
            tour.StopsList = stops;

            return tour.StopsList[checkpointIndex + 1];
        }
        public Tour GetOneByTourRequest(TourRequest tourRequest)
        {
            return TourRepository.GetOneByTourRequest(tourRequest);
        }
    }
}
