using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IRenovationRecommendationRepository
    {
        RenovationRecommendation GetOne(int accommodationGradeId);
        List<RenovationRecommendation> GetAll();
        void Add(RenovationRecommendation renovationRecommendation);
        void Delete(RenovationRecommendation renovationRecommendation);
        void Update(RenovationRecommendation renovationRecommendation);
        RenovationRecommendation GetOneByAccommodationGrade(int accommodationGradeId);
    }
}
