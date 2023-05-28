using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IComplexTourRequestPartRepository
    {
        TourRequest GetOne(int tourRequestId);
        void Add(TourRequest tourRequest);
        void Update(TourRequest tourRequest);
        void Delete(TourRequest tourRequest);
    }
}
