using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IGuideRepository
    {
        Guide GetOne(string username);
        List<Guide> GetAll();
        void Add(Guide guide);
        void Update(Guide guide);
        void UpdateLocalization(Guide guide);
        void Delete(Guide guide);
    }
}
