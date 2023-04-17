using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ICurrentUserRepository
    {
        void Add(User user);
        User GetUser();
        void Delete(User user);
    }
}
