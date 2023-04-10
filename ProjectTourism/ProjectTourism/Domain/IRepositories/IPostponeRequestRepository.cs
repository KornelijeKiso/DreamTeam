using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IPostponeRequestRepository
    {
        PostponeRequest GetOne(int id);
        PostponeRequest GetOneByReservation(int reservationId);
        List<PostponeRequest> GetAll();
        void Add(PostponeRequest postponeRequest);
        void Delete(PostponeRequest postponeRequest);
        void Update(PostponeRequest postponeRequest);
    }
}
