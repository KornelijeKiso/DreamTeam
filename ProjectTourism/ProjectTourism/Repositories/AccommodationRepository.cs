using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class AccommodationRepository : IAccommodationRepository
    {
        public AccommodationFileHandler FileHandler { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public AccommodationRepository()
        {
            FileHandler= new AccommodationFileHandler();
            Accommodations = FileHandler.Load();
        }
        public int GenerateId()
        {
            if (Accommodations == null) return 0;
            else return Accommodations.Last().Id + 1;
        }
        public void Add(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            Accommodations.Add(accommodation);
            FileHandler.Save(Accommodations);
        }

        public void Delete(Accommodation accommodation)
        {
            Accommodations.Remove(accommodation);
            FileHandler.Save(Accommodations);
        }

        public List<Accommodation> GetAll()
        {
            return Accommodations;
        }

        public Accommodation GetOne(int accommodationId)
        {
            return Accommodations.Find(a => a.Id == accommodationId);
        }

        public void Update(Accommodation accommodation)
        {
            foreach (var existingAccommodation in Accommodations)
            {
                if (existingAccommodation.Id == accommodation.Id)
                {
                    existingAccommodation.Name = accommodation.Name; 
                }
            }
        }
        public List<Accommodation> GetAllByOwner(string ownerUsername)
        {
            return Accommodations.FindAll(a => a.OwnerUsername.Equals(ownerUsername));
        }
    }
}
