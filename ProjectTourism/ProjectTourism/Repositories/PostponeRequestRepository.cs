using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class PostponeRequestRepository
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
            foreach (var postponeRequest in PostponeRequests)
            {
                if (postponeRequest.Id == id) return postponeRequest;
            }
            return null;
        }
        public PostponeRequest GetOneByReservation(int reservationId)
        {
            foreach (var postponeRequest in PostponeRequests)
            {
                if (postponeRequest.ReservationId == reservationId) return postponeRequest;
            }
            return null;
        }

        public void Update(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}
