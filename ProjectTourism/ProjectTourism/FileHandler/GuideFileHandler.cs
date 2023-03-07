using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class GuideFileHandler
    {
        private Serializer<Guide> Serializer;
        private readonly string Filename = "../../../References/guides.csv";
        private List<Guide> Guides;

        public GuideFileHandler()
        {
            Serializer = new Serializer<Guide>();
            Guides = Serializer.fromCSV(Filename);
        }
        public List<Guide> Load()
        {
            Guides = Serializer.fromCSV(Filename);
            return Guides;
        }

        public void Save(List<Guide> guides)
        {
            Serializer.toCSV(Filename, guides);
        }
    }
}
