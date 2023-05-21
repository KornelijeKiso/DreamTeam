using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Repositories
{
    public class TourAppointmentRepository : ITourAppointmentRepository
    {
        public TourAppointmentFileHandler FileHandler { get; set; }
        public List<TourAppointment> TourAppointments { get; set; }
        public TourAppointmentRepository()
        {
            FileHandler = new TourAppointmentFileHandler();
            TourAppointments = FileHandler.Load();
        }
        public int GenerateId()
        {
            if (TourAppointments.Count == 0) return 0;
            return TourAppointments.Last<TourAppointment>().Id + 1;
        }
        public void Add(TourAppointment addedTourApp)
        {
            if (addedTourApp == null)
            {
                addedTourApp.Id = 0;
            }
            else
            {
                addedTourApp.Id = GenerateId();
                TourAppointments.Add(addedTourApp);
                FileHandler.Save(TourAppointments);
            }
        }
        public void Delete(int tourAppointmentId)
        {
            TourAppointment tourAppointment = GetOne(tourAppointmentId);
            if (tourAppointment == null) return;
            TourAppointments.Remove(tourAppointment);
            FileHandler.Save(TourAppointments);
        }
        public List<TourAppointment> GetAll()
        {
            return TourAppointments;
        }

        public TourAppointment GetOne(int tourAppointmentId)
        {
            foreach(var tourAppointment in TourAppointments)
            {
                if (tourAppointment.Id == tourAppointmentId)
                    return tourAppointment;
            }
            return null;
        }

        public List<TourAppointment> GetAllByTour(int id)
        {
            List<TourAppointment> toursById = new List<TourAppointment>();
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.TourId == id)
                    toursById.Add(tourApp);
            }
            return toursById;
        }
        public TourAppointment GetByDate(int tourId, DateTime date)
        {
            foreach (TourAppointment tours in GetAllByTour(tourId))
            {
                if (tours.TourDateTime.Equals(date)) return tours;
            }
            return null;
        }
        public void Update(TourAppointment tourAppointment)
        {
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.Id == tourAppointment.Id)
                {
                    tourApp.State = tourAppointment.State;
                    if (tourApp.State == TOURSTATE.READY && tourApp.TourDateTime < DateTime.Now)
                        tourApp.State = TOURSTATE.EXPIRED;
                    tourApp.CurrentTourStop = tourAppointment.CurrentTourStop;
                }
            }
            FileHandler.Save(TourAppointments);
        }
    }
}
