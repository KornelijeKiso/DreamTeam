using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;

namespace ProjectTourism.Repositories
{
    public class RequestRepository: IRequestRepository
    {
        public RequestFileHandler FileHandler { get; set; }
        public List<Request> Requests { get; set; }
        public RequestRepository()
        {
            FileHandler = new RequestFileHandler();
            Requests = FileHandler.Load();
            CheckIfValid();
        }

        private void CheckIfValid()
        {
            foreach (var request in Requests)
            {
                if (request.State == WPF.ViewModel.REQUESTSTATE.PENDING 
                    && (DateTime.Compare(request.StartDate.ToDateTime(TimeOnly.MinValue), DateTime.Now.AddDays(2)) < 0))
                {
                    request.State = WPF.ViewModel.REQUESTSTATE.DISMISSED;
                }
            }
            FileHandler.Save(Requests);
        }
        public int GenerateId()
        {
            if (Requests == null) return 0;
            else return Requests.Last().Id + 1;
        }
        public void Add(Request request)
        {
            request.Id = GenerateId();
            Requests.Add(request);
            FileHandler.Save(Requests);
        }
        public void Delete(Request request)
        {
            Requests.Remove(request);
            FileHandler.Save(Requests);
        }
        public List<Request> GetAll()
        {
            return Requests;
        }
        public Request? GetOne(int requestId)
        {
            return Requests.Find(r => r.Id == requestId);
        }
        public void Update(Request request)
        {
            foreach(var oldRequest in Requests)
            {
                if(oldRequest.Id == request.Id)
                {
                    oldRequest.Location = request.Location;
                    oldRequest.LocationId = request.LocationId;
                    oldRequest.Description = request.Description;
                    oldRequest.Language = request.Language;
                    oldRequest.NumberOfGuests = request.NumberOfGuests;
                    oldRequest.StartDate = request.StartDate;
                    oldRequest.EndDate = request.EndDate;
                }
            }
            FileHandler.Save(Requests);
        }
    }
}
