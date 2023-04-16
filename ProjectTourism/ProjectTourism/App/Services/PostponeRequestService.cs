using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class PostponeRequestService
    {
        private IPostponeRequestRepository PostponeRequestRepo;
        public PostponeRequestService(IPostponeRequestRepository iprr)
        {
            PostponeRequestRepo = iprr;
        }
        public void Add(PostponeRequest postponeRequest)
        {
            PostponeRequestRepo.Add(postponeRequest);
        }
        public void Delete(PostponeRequest postponeRequest)
        {
            PostponeRequestRepo.Delete(postponeRequest);
        }
        public PostponeRequest GetOne(int id)
        {
            return PostponeRequestRepo.GetOne(id);
        }
        public PostponeRequest GetOneByReservation(int id)
        {
            return PostponeRequestRepo.GetOneByReservation(id);
        }
        public List<PostponeRequest> GetAll()
        {
            return PostponeRequestRepo.GetAll();
        }
        public void Update(PostponeRequest postponeRequest)
        {
            PostponeRequestRepo.Update(postponeRequest);
        }
    }
}
