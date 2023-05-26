using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class ForumRepository : IForumRepository
    {
        public ForumFileHandler FileHandler { get; set; }
        public List<Forum> Forums { get; set; }
        public ForumRepository()
        {
            FileHandler = new ForumFileHandler();
            Forums = FileHandler.Load();
        }
        private int GenerateId()
        {
            if (Forums == null) return 0;
            else return Forums.Last().Id + 1;
        }
        public Forum Add(Forum forum)
        {
            if (Forums.Find(f => f.LocationId == forum.LocationId) != null)
                return Forums.Find(f => f.LocationId == forum.LocationId);
            forum.Id = GenerateId();
            forum.OpenedOn = DateTime.Now;
            Forums.Add(forum);
            FileHandler.Save(Forums);
            return forum;
        }

        public List<Forum> GetAll()
        {
            return Forums;
        }

        public Forum GetOne(int id)
        {
            return Forums.Find(f => f.Id == id);
        }
    }
}
