using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Domain.Model;

namespace ProjectTourism.Services
{
    public class ComplexTourRequestPartService
    {
        private IComplexTourRequestPartRepository ComplexTourRequestPartRepository;
        public ComplexTourRequestPartService()
        {
            ComplexTourRequestPartRepository = Injector.Injector.CreateInstance<IComplexTourRequestPartRepository>();
        }
        public TourRequest GetOne(int tourRequestId)
        {
            return ComplexTourRequestPartRepository.GetOne(tourRequestId);
        }
        public void Add(TourRequest tourRequest)
        {
            ComplexTourRequestPartRepository.Add(tourRequest);
        }
        public void Update(TourRequest tourRequest)
        {
            ComplexTourRequestPartRepository.Update(tourRequest);        }
        public void Delete(TourRequest tourRequest)
        {
            ComplexTourRequestPartRepository.Delete(tourRequest);        }
    }
}
