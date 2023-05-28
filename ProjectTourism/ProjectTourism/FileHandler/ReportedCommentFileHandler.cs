using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class ReportedCommentFileHandler
    {
        private Serializer<ReportedComment> Serializer;
        private readonly string Filename = "../../../References/reportedComments.csv";
        private List<ReportedComment> ReportedComments;

        public ReportedCommentFileHandler()
        {
            Serializer = new Serializer<ReportedComment>();
        }

        public List<ReportedComment> Load()
        {
            ReportedComments = Serializer.fromCSV(Filename);
            return ReportedComments;
        }

        public void Save(List<ReportedComment> reported)
        {
            Serializer.toCSV(Filename, reported);
        }

    }
}
