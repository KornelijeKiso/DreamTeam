using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
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
        public List<IObserver> Observers;
        public AccommodationService(IAccommodationRepository iar)
        {
            AccommodationRepo= iar;
            Observers = new List<IObserver>();
        }
        public void Add(AccommodationVM accommodation)
        {
            AccommodationRepo.Add(accommodation.GetAccommodation());
        }
        public void Delete(AccommodationVM accommodation)
        {
            AccommodationRepo.Delete(accommodation.GetAccommodation());
        }
        public AccommodationVM GetOne(int id)
        {
            return new AccommodationVM(AccommodationRepo.GetOne(id));
        }
        public List<AccommodationVM> GetAll()
        {
            List<AccommodationVM> accommodations = new List<AccommodationVM>();
            foreach (var accommodation in AccommodationRepo.GetAll())
            {
                accommodations.Add(new AccommodationVM(accommodation));
            }
            return accommodations;
        }
        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
            }
        }
    }
}
