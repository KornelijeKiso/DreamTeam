using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IRenovationRepository
    {
        Renovation GetOne(int renovationId);
        List<Renovation> GetAll();
        int AddAndReturnId(Renovation renovation);
        void Delete(Renovation renovation);
        List<Renovation> GetAllByAccommodation(int accommodationId);
    }
}
