using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class TourRequestFileHandler
    {
        private Serializer<TourRequest> Serializer;
        private readonly string Filename = "../../../References/tourRequests.csv";
        private List<TourRequest> TourRequests;

        public TourRequestFileHandler()
        {
            Serializer = new Serializer<TourRequest>();
            TourRequests = Serializer.fromCSV(Filename);
        }
        public List<TourRequest> Load()
        {
            TourRequests = Serializer.fromCSV(Filename);
            return TourRequests;
        }
        public void Save(List<TourRequest> tourRequests)
        {
            Serializer.toCSV(Filename, tourRequests);
        }
    }
}
