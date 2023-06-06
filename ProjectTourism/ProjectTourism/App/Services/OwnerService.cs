using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class OwnerService
    {
        private IOwnerRepository OwnerRepo;
        public OwnerService()
        {
            OwnerRepo = Injector.Injector.CreateInstance<IOwnerRepository>();
        }
        public void Add(Owner owner)
        {
            OwnerRepo.Add(owner);
        }
        public void Delete(Owner owner)
        {
            OwnerRepo.Delete(owner);
        }
        public Owner GetOne(string username)
        {
            return OwnerRepo.GetOne(username);
        }
        public List<Owner> GetAll()
        {
            return OwnerRepo.GetAll();
        }
        public void TurnHelpOn(Owner owner)
        {
            OwnerRepo.TurnHelpOn(owner);
        }
        public void TurnHelpOff(Owner owner)
        {
            OwnerRepo.TurnHelpOff(owner);
        }
    }
}
