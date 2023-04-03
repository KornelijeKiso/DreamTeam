using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
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
            int id = 0;
            if (Guest1Grades == null)
            {
                id = 0;
            }
            else
            {
                foreach (var grades in Guest1Grades)
                {
                    id = grades.Id + 1;
                }
            }
            return id;
        }
        public Guest1Grade GetOneByReservation(int reservationId)
        {
            foreach (var guest1Grade in Guest1Grades)
            {
                if (guest1Grade.ReservationId == reservationId) return guest1Grade;
            }
            return null;
        }
        public Guest1Grade GetOne(int guest1GradeId)
        {
            foreach (var guest1Grade in Guest1Grades)
            {
                if (guest1Grade.Id == guest1GradeId) return guest1Grade;
            }
            return null;
        }
        public void Update(Guest1Grade guest1Grade)
        {
            throw new NotImplementedException();
        }
    }
}
