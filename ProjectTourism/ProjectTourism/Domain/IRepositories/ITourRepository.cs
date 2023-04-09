using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ITourRepository
    {
        Tour GetOne(int tourId);
        List<Tour> GetAll();
        void Add(Tour tour);
        void Delete(Tour tour);
        void Update(Tour tour);
        List<string> GetStops(Tour tour);
    }
}
