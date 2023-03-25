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
    internal class AccommodationGradeDAO
    {
        public List<IObserver> Observers;
        public AccommodationGradeFileHandler FileHandler { get; set; }
        public List<AccommodationGrade> AccommodationGrades { get; set; }
        public AccommodationGradeDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new AccommodationGradeFileHandler();
            AccommodationGrades = FileHandler.Load();
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
        public int GenerateId()
        {
            int id = 0;
            if (AccommodationGrades == null)
            {
                id = 0;
            }
            else
            {
                foreach (var grades in AccommodationGrades)
                {
                    id = grades.Id + 1;
                }
            }
            return id;
        }
        public AccommodationGrade GetOneByReservation(int reservationId)
        {
            foreach (var accommodationGrade in AccommodationGrades)
            {
                if (accommodationGrade.ReservationId == reservationId) return accommodationGrade;
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
