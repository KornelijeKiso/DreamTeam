using ProjectTourism.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class ReportedCommentService
    {
        private IReportedCommentRepository ReportedCommentRepo;
        public ReportedCommentService()
        {
            ReportedCommentRepo = Injector.Injector.CreateInstance<IReportedCommentRepository>(); ;
        }
        public bool Add(int commentId, string username)
        {
            return ReportedCommentRepo.Add(commentId, username);
        }
    }
}
