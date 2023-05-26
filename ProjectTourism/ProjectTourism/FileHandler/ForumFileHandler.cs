using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class ForumFileHandler
    {
        private Serializer<Forum> Serializer;
        private readonly string Filename = "../../../References/forums.csv";
        private List<Forum> Forums;

        public ForumFileHandler()
        {
            Serializer = new Serializer<Forum>();
        }

        public List<Forum> Load()
        {
            Forums = Serializer.fromCSV(Filename);
            return Forums;
        }

        public void Save(List<Forum> forums)
        {
            Serializer.toCSV(Filename, forums);
        }
    }
}
