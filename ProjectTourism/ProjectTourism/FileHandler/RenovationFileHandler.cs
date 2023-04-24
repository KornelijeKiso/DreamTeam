using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class RenovationFileHandler
    {
        private Serializer<Renovation> Serializer;
        private readonly string Filename = "../../../References/renovations.csv";
        private List<Renovation> Renovations;

        public RenovationFileHandler()
        {
            Serializer = new Serializer<Renovation>();
        }

        public List<Renovation> Load()
        {
            Renovations = Serializer.fromCSV(Filename);
            return Renovations;
        }

        public void Save(List<Renovation> renovations)
        {
            Serializer.toCSV(Filename, renovations);
        }
    }
}
