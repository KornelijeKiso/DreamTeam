using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface IAccommodationGradeRepository
    {
        AccommodationGrade GetOne(int accommodationGradeId);
        List<AccommodationGrade> GetAll();
        int Add(AccommodationGrade accommodationGrade);
        void Delete(AccommodationGrade accommodationGrade);
        void Update(AccommodationGrade accommodationGrade);
        AccommodationGrade GetOneByReservation(int reservationId);
    }
}
