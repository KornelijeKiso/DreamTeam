using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class RequestFileHandler
    {
        private Serializer<Request> Serializer;
        private readonly string Filename = "../../../References/requests.csv";
        private List<Request> Requests;

        public RequestFileHandler()
        {
            Serializer = new Serializer<Request>();
            Requests = Serializer.fromCSV(Filename);
        }
        public List<Request> Load()
        {
            Requests = Serializer.fromCSV(Filename);
            return Requests;
        }
        public void Save(List<Request> requests)
        {
            Serializer.toCSV(Filename, requests);
        }
    }
}
