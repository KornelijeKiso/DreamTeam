using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Services
{
    public class GuideService
    {
        private IGuideRepository GuideRepository;
        public GuideService(IGuideRepository iGuideRepository)
        {
            GuideRepository = iGuideRepository;
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
        public void Update(Guide guide)
        {
            GuideRepository.Update(guide);
        }
    }
}
