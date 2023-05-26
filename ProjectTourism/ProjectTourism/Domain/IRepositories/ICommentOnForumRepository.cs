using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ICommentOnForumRepository
    {
        CommentOnForum GetOne(int id);
        List<CommentOnForum> GetAllByForum(int forumId);
        CommentOnForum Add(CommentOnForum commentOnForum);
        void Report(int id);
    }
}
