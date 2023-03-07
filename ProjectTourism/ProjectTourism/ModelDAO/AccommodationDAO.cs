using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.ModelDAO
{
    public class AccommodationDAO
    {
        public AccommodationFileHandler FileHandler { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public AccommodationDAO()
        {
            FileHandler = new AccommodationFileHandler();
            Accommodations = FileHandler.Load();
        }
        public int GenerateId()
        {
            int id;
            if (Accommodations == null)
            {
                id = 0;
            }
            else
            {
                id = Accommodations[-1].Id + 1;
            }
            return id;
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
        public Accommodation GetOne(int id)
        {
            foreach (var accommodation in Accommodations)
            {
                if (accommodation.Id == id) return accommodation;
            }
            return null;
        }
    }
}
