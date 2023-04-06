using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories.IRepositories
{
    public interface IAccommodationRepository
    {
        Accommodation GetOne(int accommodationId);
        List<Accommodation> GetAll();
        void Add(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        void Update(Accommodation accommodation);
        List<Accommodation> GetAllByOwner(string owner);
    }
}
