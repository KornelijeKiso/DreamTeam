using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IOwnerRepository
    {
        Owner GetOne(string ownerUsername);
        List<Owner> GetAll();
        void Add(Owner owner);
        void Delete(Owner owner);
        void Update(Owner owner);
    }
}
