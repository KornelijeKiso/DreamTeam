using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IReportedCommentRepository
    {
        bool Add(int commentId, string username);
    }
}
