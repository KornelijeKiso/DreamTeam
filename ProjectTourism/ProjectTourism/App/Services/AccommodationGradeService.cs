using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Services
{
    public class AccommodationGradeService
    {
        private IAccommodationGradeRepository AccommodationGradeRepo;
        public AccommodationGradeService()
        {
            AccommodationGradeRepo = Injector.Injector.CreateInstance<IAccommodationGradeRepository>(); ;
        }
        public void Add(AccommodationGrade accommodationGrade)
        {
            AccommodationGradeRepo.Add(accommodationGrade);
        }
        public void Delete(AccommodationGrade accommodationGrade)
        {
            AccommodationGradeRepo.Delete(accommodationGrade);
        }
        public AccommodationGrade GetOneByReservation(int reservationId)
        {
            return AccommodationGradeRepo.GetOneByReservation(reservationId);
        }
        public List<AccommodationGrade> GetAll()
        {
            return AccommodationGradeRepo.GetAll();
        }
    }
}
