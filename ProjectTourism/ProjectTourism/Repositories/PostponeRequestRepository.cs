﻿using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class PostponeRequestRepository:IPostponeRequestRepository
    {
        public PostponeRequestFileHandler FileHandler { get; set; }
        public List<PostponeRequest> PostponeRequests { get; set; }
        public PostponeRequestRepository()
        {
            FileHandler = new PostponeRequestFileHandler();
            PostponeRequests = FileHandler.Load();
        }

        public void Add(PostponeRequest postponeRequest)
        {
            PostponeRequests.Add(postponeRequest);
            FileHandler.Save(PostponeRequests);
        }
        public void Delete(PostponeRequest postponeRequest)
        {
            PostponeRequests.Remove(postponeRequest);
            FileHandler.Save(PostponeRequests);
        }
        public List<PostponeRequest> GetAll()
        {
            return PostponeRequests;
        }
        public PostponeRequest GetOne(int id)
        {
            return PostponeRequests.Find(p => p.Id == id);
        }
        public PostponeRequest GetOneByReservation(int reservationId)
        {
            return PostponeRequests.Find(p => p.ReservationId == reservationId);

        }
        public void Update(PostponeRequest postponeRequest)
        {
            foreach (var existingPostponeRequest in PostponeRequests)
            {
                if (existingPostponeRequest.Id == postponeRequest.Id)
                {
                    existingPostponeRequest.Accepted = postponeRequest.Accepted;
                    existingPostponeRequest.Rejected = postponeRequest.Rejected;
                    existingPostponeRequest.AdditionalComment= postponeRequest.AdditionalComment;
                }
            }
            FileHandler.Save(PostponeRequests);
        }
    }
}
