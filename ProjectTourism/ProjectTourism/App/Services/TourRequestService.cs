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
    public class TourRequestService
    {
        private ITourRequestRepository TourRequestRepository;
        public TourRequestService()
        {
            TourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }
        public void Add(TourRequest tourRequest)
        {
            TourRequestRepository.Add(tourRequest);
        }
        public void Delete(TourRequest tourRequest)
        {
            TourRequestRepository.Delete(tourRequest);
        }
        public TourRequest GetOne(int tourRequestId)
        {
            return TourRequestRepository.GetOne(tourRequestId);
        }
        public List<TourRequest> GetAll()
        {
            return TourRequestRepository.GetAll();
        }
        public void Update(TourRequest tourRequest)
        {
            TourRequestRepository.Update(tourRequest);
        }
        public List<TourRequest> GetByNewTour(Tour newTour)
        {
            return TourRequestRepository.GetByNewTour(newTour);
        }
    }
}
