using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class RenovationRecommendationService
    {
        private IRenovationRecommendationRepository RenovationRecommendationRepo;
        public RenovationRecommendationService()
        {
            RenovationRecommendationRepo = Injector.Injector.CreateInstance<IRenovationRecommendationRepository>(); ;
        }
        public void Add(RenovationRecommendation renovationRecommendation)
        {
            RenovationRecommendationRepo.Add(renovationRecommendation);
        }
        public void Delete(RenovationRecommendation renovationRecommendation)
        {
            RenovationRecommendationRepo.Delete(renovationRecommendation);
        }
        public RenovationRecommendation GetOneByAccommodationGrade(int accommodationGradeId)
        {
            return RenovationRecommendationRepo.GetOneByAccommodationGrade(accommodationGradeId);
        }
        public List<RenovationRecommendation> GetAll()
        {
            return RenovationRecommendationRepo.GetAll();
        }
    }
}
