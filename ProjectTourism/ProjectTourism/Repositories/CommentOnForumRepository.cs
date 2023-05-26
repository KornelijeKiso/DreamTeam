using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class CommentOnForumRepository : ICommentOnForumRepository
    {
        public CommentOnForumFileHandler FileHandler { get; set; }
        public List<CommentOnForum> CommentsOnForums { get; set; }
        public CommentOnForumRepository()
        {
            FileHandler = new CommentOnForumFileHandler();
            CommentsOnForums = FileHandler.Load();
        }
        private int GenerateId()
        {
            if (CommentsOnForums == null) return 0;
            else return CommentsOnForums.Last().Id + 1;
        }
        public CommentOnForum Add(CommentOnForum commentOnForum)
        {
            commentOnForum.Id = GenerateId();
            commentOnForum.Reports = 0;
            commentOnForum.Published = DateTime.Now;
            CommentsOnForums.Add(commentOnForum);
            FileHandler.Save(CommentsOnForums);
            return commentOnForum;
        }

        public List<CommentOnForum> GetAllByForum(int forumId)
        {
            return CommentsOnForums.FindAll(c=>c.ForumId== forumId);
        }

        public CommentOnForum GetOne(int id)
        {
            return CommentsOnForums.Find(c => c.Id == id);
        }

        public void Report(int id)
        {
            foreach(CommentOnForum comment in CommentsOnForums)
            {
                if(comment.Id==id)
                {
                    comment.Reports++;
                    FileHandler.Save(CommentsOnForums);
                    return;
                }
            }
        }
    }
}
