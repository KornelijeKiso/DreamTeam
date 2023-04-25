using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Services
{
    public class RequestService
    {
        private IRequestRepository RequestRepository;
        public RequestService()
        {
            RequestRepository = Injector.Injector.CreateInstance<IRequestRepository>();
        }
        public void Add(Request request)
        {
            RequestRepository.Add(request);
        }
        public void Delete(Request request)
        {
            RequestRepository.Delete(request);
        }
        public Request GetOne(int requestId)
        {
            return RequestRepository.GetOne(requestId);
        }
        public List<Request> GetAll()
        {
            return RequestRepository.GetAll();
        }
        public void Update(Request request)
        {
            RequestRepository.Update(request);
        }
    }
}
