using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IRequestRepository
    {
        Request GetOne(int requestId);
        List<Request> GetAll();
        void Add(Request request);
        void Update(Request request);
        void Delete(Request request);
    }
}
