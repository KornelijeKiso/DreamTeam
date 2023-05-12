using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class RenovationRecommendationFileHandler
    {
        private Serializer<RenovationRecommendation> Serializer;
        private readonly string Filename = "../../../References/renovationRecommendations.csv";
        private List<RenovationRecommendation> RenovationRecommendations;

        public RenovationRecommendationFileHandler()
        {
            Serializer = new Serializer<RenovationRecommendation>();
        }

        public List<RenovationRecommendation> Load()
        {
            RenovationRecommendations = Serializer.fromCSV(Filename);
            return RenovationRecommendations;
        }

        public void Save(List<RenovationRecommendation> renovationRecommendations)
        {
            Serializer.toCSV(Filename, renovationRecommendations);
        }
    }
}
