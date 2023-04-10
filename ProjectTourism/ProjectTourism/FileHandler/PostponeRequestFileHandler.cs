using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class PostponeRequestFileHandler
    {
        private Serializer<PostponeRequest> Serializer;
        private readonly string Filename = "../../../References/postponeRequests.csv";
        private List<PostponeRequest> PostponeRequests;

        public PostponeRequestFileHandler()
        {
            Serializer = new Serializer<PostponeRequest>();
            PostponeRequests = Serializer.fromCSV(Filename);
        }

        public List<PostponeRequest> Load()
        {
            PostponeRequests = Serializer.fromCSV(Filename);
            return PostponeRequests;
        }

        public void Save(List<PostponeRequest> postponeRequests)
        {
            Serializer.toCSV(Filename, postponeRequests);
        }
    }
}
