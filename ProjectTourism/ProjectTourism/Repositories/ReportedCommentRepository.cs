using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class ReportedCommentRepository : IReportedCommentRepository
    {
        public ReportedCommentFileHandler FileHandler { get; set; }
        public List<ReportedComment> ReportedComments { get; set; }
        public ReportedCommentRepository()
        {
            FileHandler = new ReportedCommentFileHandler();
            ReportedComments = FileHandler.Load();
        }
        public bool Add(int commentId, string username)
        {
            if (ReportedComments.Find(r => r.CommentId == commentId && r.Username.Equals(username)) != null)
                return false;
            else
            {
                ReportedComment newReport = new ReportedComment(commentId, username);
                ReportedComments.Add(newReport);
                FileHandler.Save(ReportedComments);
                return true;
            }
        }
    }
}
