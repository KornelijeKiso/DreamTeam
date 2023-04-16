using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class AccommodationService
    {
        private IAccommodationRepository AccommodationRepo;
        public AccommodationService(IAccommodationRepository iar)
        {
            AccommodationRepo= iar;
        }
        public void Add(Accommodation accommodation)
        {
            AccommodationRepo.Add(accommodation);
        }
        public void Delete(Accommodation accommodation)
        {
            AccommodationRepo.Delete(accommodation);
        }
        public Accommodation GetOne(int id)
        {
            return AccommodationRepo.GetOne(id);
        }
        public List<Accommodation> GetAll()
        {
            return AccommodationRepo.GetAll();
        }
        public List<Accommodation> GetAllByOwner(string username)
        {
            return AccommodationRepo.GetAllByOwner(username);
        }
    }
}
