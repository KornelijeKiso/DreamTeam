using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class AccommodationGradeRepository:IAccommodationGradeRepository
    {
        public AccommodationGradeFileHandler FileHandler { get; set; }
        public List<AccommodationGrade> AccommodationGrades { get; set; }
        public AccommodationGradeRepository()
        {
            FileHandler = new AccommodationGradeFileHandler();
            AccommodationGrades = FileHandler.Load();
        }
        public int GenerateId()
        {
            if (AccommodationGrades == null) return 0;
            else return AccommodationGrades.Last().Id + 1;
        }
        public void Add(AccommodationGrade accommodationGrade)
        {
            accommodationGrade.Id = GenerateId();
            AccommodationGrades.Add(accommodationGrade);
            FileHandler.Save(AccommodationGrades);
        }

        public void Delete(AccommodationGrade accommodationGrade)
        {
            AccommodationGrades.Remove(accommodationGrade);
            FileHandler.Save(AccommodationGrades);
        }

        public List<AccommodationGrade> GetAll()
        {
            return AccommodationGrades;
        }

        public AccommodationGrade GetOne(int accommodationGradeId)
        {
            return AccommodationGrades.Find(a => a.Id == accommodationGradeId);
        }

        public void Update(AccommodationGrade accommodationGrade)
        {
            foreach (var existingAccommodationGrade in AccommodationGrades)
            {
                if (existingAccommodationGrade.Id == accommodationGrade.Id)
                {
                    existingAccommodationGrade.ReservationId = accommodationGrade.ReservationId;
                }
            }
        }
        public AccommodationGrade GetOneByReservation(int reservationId)
        {
            return AccommodationGrades.Find(a => a.ReservationId == reservationId);
        }
    }
}
