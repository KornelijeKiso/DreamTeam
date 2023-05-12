using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class RenovationRecommendationRepository:IRenovationRecommendationRepository
    {
        public RenovationRecommendationFileHandler FileHandler { get; set; }
        public List<RenovationRecommendation> RenovationRecommendations { get; set; }
        public RenovationRecommendationRepository()
        {
            FileHandler = new RenovationRecommendationFileHandler();
            RenovationRecommendations = FileHandler.Load();
        }
        public int GenerateId()
        {
            if (RenovationRecommendations == null || RenovationRecommendations.Count==0) return 0;
            else return RenovationRecommendations.Last().Id + 1;
        }
        public void Add(RenovationRecommendation renovationRecommendation)
        {
            renovationRecommendation.Id = GenerateId();
            RenovationRecommendations.Add(renovationRecommendation);
            FileHandler.Save(RenovationRecommendations);
        }

        public void Delete(RenovationRecommendation renovationRecommendation)
        {
            RenovationRecommendations.Remove(renovationRecommendation);
            FileHandler.Save(RenovationRecommendations);
        }

        public List<RenovationRecommendation> GetAll()
        {
            return RenovationRecommendations;
        }

        public RenovationRecommendation GetOne(int renovationRecommendationId)
        {
            return RenovationRecommendations.Find(a => a.Id == renovationRecommendationId);
        }

        public void Update(RenovationRecommendation renovationRecommendation)
        {
            foreach (var existingRenovationRecommendation in RenovationRecommendations)
            {
                if (existingRenovationRecommendation.Id == renovationRecommendation.Id)
                {
                    existingRenovationRecommendation.AccommodationGradeId = renovationRecommendation.AccommodationGradeId;
                }
            }
        }
        public RenovationRecommendation GetOneByAccommodationGrade(int accommodationGradeId)
        {
            return RenovationRecommendations.Find(a => a.AccommodationGradeId == accommodationGradeId);
        }
    }
}

