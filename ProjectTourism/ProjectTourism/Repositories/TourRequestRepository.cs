﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;

namespace ProjectTourism.Repositories
{
    public class TourRequestRepository: ITourRequestRepository
    {
        public TourRequestFileHandler FileHandler { get; set; }
        public List<TourRequest> TourRequests { get; set; }
        public TourRequestRepository()
        {
            FileHandler = new TourRequestFileHandler();
            TourRequests = FileHandler.Load();
            CheckIfValid();
        }

        private void CheckIfValid()
        {
            foreach (var tourRequest in TourRequests)
            {
                if (tourRequest.State == WPF.ViewModel.REQUESTSTATE.PENDING 
                    && (DateTime.Compare(tourRequest.StartDate.ToDateTime(TimeOnly.MinValue), DateTime.Now.AddDays(2)) < 0))
                {
                    tourRequest.State = WPF.ViewModel.REQUESTSTATE.EXPIRED;
                }
            }
            FileHandler.Save(TourRequests);
        }
        public int GenerateId()
        {
            if (TourRequests == null) return 0;
            else return TourRequests.Last().Id + 1;
        }
        public void Add(TourRequest tourRequest)
        {
            tourRequest.Id = GenerateId();
            TourRequests.Add(tourRequest);
            FileHandler.Save(TourRequests);
        }
        public void Delete(TourRequest tourRequest)
        {
            TourRequests.Remove(tourRequest);
            FileHandler.Save(TourRequests);
        }
        public List<TourRequest> GetAll()
        {
            return TourRequests;
        }
        public TourRequest? GetOne(int tourRequestId)
        {
            return TourRequests.Find(r => r.Id == tourRequestId);
        }
        public void Update(TourRequest tourRequest)
        {
            foreach(var oldTourRequest in TourRequests)
            {
                if(oldTourRequest.Id == tourRequest.Id)
                {
                    oldTourRequest.Location = tourRequest.Location;
                    oldTourRequest.LocationId = tourRequest.LocationId;
                    oldTourRequest.Description = tourRequest.Description;
                    oldTourRequest.Language = tourRequest.Language;
                    oldTourRequest.NumberOfGuests = tourRequest.NumberOfGuests;
                    oldTourRequest.StartDate = tourRequest.StartDate;
                    oldTourRequest.EndDate = tourRequest.EndDate;
                }
            }
            FileHandler.Save(TourRequests);
        }
    }
}