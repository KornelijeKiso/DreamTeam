using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class ForumService
    {
        private IForumRepository ForumRepo;
        public ForumService()
        {
            ForumRepo = Injector.Injector.CreateInstance<IForumRepository>();
        }
        public void Add(Forum forum)
        {
            ForumRepo.Add(forum);
        }
        public Forum GetOne(int id)
        {
            return ForumRepo.GetOne(id);
        }
        public List<Forum> GetAll()
        {
            return ForumRepo.GetAll();
        }
    }
}
