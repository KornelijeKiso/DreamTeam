using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Services
{
    public class GuideService
    {
        public List<IObserver> Observers;
        private IGuideRepository GuideRepository;
        public GuideService(IGuideRepository iGuideRepository)
        {
            GuideRepository = iGuideRepository;
            Observers = new List<IObserver>();
        }
        public void Add(Guide guide)
        {
            GuideRepository.Add(guide);
        }
        public void Delete(Guide guide)
        {
            GuideRepository.Delete(guide);
        }
        public Guide GetOne(string username)
        {
            return GuideRepository.GetOne(username);
        }
        public List<Guide> GetAll()
        {
            return GuideRepository.GetAll();
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
        public void Update(Guide guide)
        {
            GuideRepository.Update(guide);
        }
    }
}
