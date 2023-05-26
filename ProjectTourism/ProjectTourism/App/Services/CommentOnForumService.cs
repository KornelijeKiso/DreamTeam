using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.App.Services
{
    public class CommentOnForumService
    {
        private ICommentOnForumRepository CommentOnForumRepo;
        public CommentOnForumService()
        {
            CommentOnForumRepo = Injector.Injector.CreateInstance<ICommentOnForumRepository>();
        }
        public CommentOnForum Add(CommentOnForum commentOnForum)
        {
            return CommentOnForumRepo.Add(commentOnForum);
        }
        public CommentOnForum GetOne(int id)
        {
            return CommentOnForumRepo.GetOne(id);
        }
        public List<CommentOnForum> GetAllByForum(int forumId)
        {
            return CommentOnForumRepo.GetAllByForum(forumId);
        }
        public void Report(int id)
        {
            CommentOnForumRepo.Report(id);
        }
    }
}
