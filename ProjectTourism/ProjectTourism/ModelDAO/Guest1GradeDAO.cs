using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.ModelDAO
{
    public class Guest1GradeDAO
    {
        public List<IObserver> Observers;
        public Guest1GradeFileHandler FileHandler { get; set; }
        public List<Guest1Grade> Guest1Grades { get; set; }
        public Guest1GradeDAO()
        {
            Observers = new List<IObserver>();
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
        public List<Guest1Grade> GetAllByGuest1(string username)
        {
            List<Guest1Grade> retVal = new List<Guest1Grade>();
            foreach (var guest1Grade in Guest1Grades)
            {
                if (guest1Grade.Reservation.Guest1Username.Equals(username)) retVal.Add(guest1Grade);
            }
            return retVal;
        }
        public Guest1Grade GetOneByReservation(int reservationId)
        {
            foreach (var guest1Grade in Guest1Grades)
            {
                if (guest1Grade.ReservationId==reservationId) return guest1Grade;
            }
            return null;
        }

        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
            }
        }
    }
}
