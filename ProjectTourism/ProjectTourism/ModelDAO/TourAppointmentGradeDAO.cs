using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.ModelDAO
{
    public class TourAppointmentGradeDAO
    {
        public List<IObserver> Observers;
        public TourAppointmentGradeFileHandler TourAppointmentGradeFileHandler { get; set; }
        public List<TourAppointmentGrade> TourAppointmentGrades { get; set; }
        public TourAppointmentGradeDAO()
        {
            TourAppointmentGradeFileHandler = new TourAppointmentGradeFileHandler();
            TourAppointmentGrades = TourAppointmentGradeFileHandler.Load();
            Observers = new List<IObserver>();
        }
        public int GenerateId()
        {
            if (TourAppointmentGrades.Count == 0) return 0;
            return TourAppointmentGrades.Last<TourAppointmentGrade>().Id + 1;
        }
        public void Add(TourAppointmentGrade addedTourAppointmentGrade)
        {
            if (addedTourAppointmentGrade == null)
            {
                addedTourAppointmentGrade.Id = 0;
            }
            else
            {
                addedTourAppointmentGrade.Id = GenerateId();
                TourAppointmentGrades.Add(addedTourAppointmentGrade);
                TourAppointmentGradeFileHandler.Save(TourAppointmentGrades);
            }
        }
        public void Delete(TourAppointmentGrade tourAppointmentGrade)
        {
            TourAppointmentGrades.Remove(tourAppointmentGrade);
            TourAppointmentGradeFileHandler.Save(TourAppointmentGrades);
        }
        public List<TourAppointmentGrade> GetAll()
        {
            return TourAppointmentGrades;
        }
        public TourAppointmentGrade? GetOne(int id)
        {
            foreach (var tourAppGrade in TourAppointmentGrades)
            {
                if (tourAppGrade.Id == id) return tourAppGrade;
            }
            return null;
        }
        public TourAppointmentGrade GetOneByTourAppointment(int tourAppId)
        {
            foreach (var tourAppGrade in TourAppointmentGrades)
            {
                if (tourAppGrade.TourAppointmentId == tourAppId) return tourAppGrade;
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
