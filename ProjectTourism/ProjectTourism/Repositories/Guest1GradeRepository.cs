using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class Guest1GradeRepository : IGuest1GradeRepository
    {
        public Guest1GradeFileHandler FileHandler { get; set; }
        public List<Guest1Grade> Guest1Grades { get; set; }
        public Guest1GradeRepository()
        {
            FileHandler = new Guest1GradeFileHandler();
            Guest1Grades = FileHandler.Load();
        }

        public void Add(Guest1Grade guest1Grade)
        {
            guest1Grade.Id = GenerateId();
            Guest1Grades.Add(guest1Grade);
            FileHandler.Save(Guest1Grades);
        }
        public void Delete(Guest1Grade guest1Grade)
        {
            Guest1Grades.Remove(guest1Grade);
            FileHandler.Save(Guest1Grades);
        }
        public List<Guest1Grade> GetAll()
        {
            return Guest1Grades;
        }
        public int GenerateId()
        {
            if (Guest1Grades == null) return 0;
            else return Guest1Grades.Last().Id + 1;
        }
        public Guest1Grade GetOneByReservation(int reservationId)
        {
            return Guest1Grades.Find(g => g.ReservationId == reservationId);
        }
        public Guest1Grade GetOne(int guest1GradeId)
        {
            return Guest1Grades.Find(g => g.Id == guest1GradeId);

        }
        public void Update(Guest1Grade guest1Grade)
        {
            throw new NotImplementedException();
        }
    }
}
