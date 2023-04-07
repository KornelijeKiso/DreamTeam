using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
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
        public void Add(GuideVM guide)
        {
            GuideRepository.Add(guide.GetGuide());
        }
        public void Delete(GuideVM guide)
        {
            GuideRepository.Delete(guide.GetGuide());
        }
        public GuideVM GetOne(string username)
        {
            return new GuideVM(GuideRepository.GetOne(username));
        }
        public List<GuideVM> GetAll()
        {
            List<GuideVM> guides = new List<GuideVM>();
            foreach (var guide in GuideRepository.GetAll())
            {
                guides.Add(new GuideVM(guide));
            }
            return guides;
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
