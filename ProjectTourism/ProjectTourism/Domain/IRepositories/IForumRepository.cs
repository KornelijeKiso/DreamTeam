using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IForumRepository
    {
        Forum GetOne(int id);
        List<Forum> GetAll();
        Forum Add(Forum forum);
    }
}
