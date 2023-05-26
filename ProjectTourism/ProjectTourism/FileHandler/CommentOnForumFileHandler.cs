using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class CommentOnForumFileHandler
    {
        private Serializer<CommentOnForum> Serializer;
        private readonly string Filename = "../../../References/commentsOnForums.csv";
        private List<CommentOnForum> CommentsOnForums;

        public CommentOnForumFileHandler()
        {
            Serializer = new Serializer<CommentOnForum>();
        }

        public List<CommentOnForum> Load()
        {
            CommentsOnForums = Serializer.fromCSV(Filename);
            return CommentsOnForums;
        }

        public void Save(List<CommentOnForum> commentsOnForums)
        {
            Serializer.toCSV(Filename, commentsOnForums);
        }
    }
}
