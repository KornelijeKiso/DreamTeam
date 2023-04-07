using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Services
{
    public class TourAppointmentService
    {
        private ITourAppointmentRepository TourAppointmentRepository;
        public List<IObserver> Observers;
        public List<TourAppointment> TourAppointments { get; set; } //Get rid off in some point
        public TourAppointmentFileHandler FileHandler { get; set; } //and this

        public TourAppointmentService(ITourAppointmentRepository tourAppointmentRepo)
        {
            TourAppointmentRepository = tourAppointmentRepo;
            Observers = new List<IObserver>();
            FileHandler = new TourAppointmentFileHandler(); //and this
            TourAppointments = FileHandler.Load(); //and this
        }
        public void Add(TourAppointmentVM tourAppointmentVM)
        {
            TourAppointmentRepository.Add(tourAppointmentVM.GetTourAppointment());
        }
        public void Delete(TourAppointmentVM tourAppointmentVM)
        {
            TourAppointmentRepository.Delete(tourAppointmentVM.GetTourAppointment());
        }
        public TourAppointmentVM GetOne(int id)
        {
            return new TourAppointmentVM(TourAppointmentRepository.GetOne(id));
        }
        public List<TourAppointmentVM> GetAll()
        {
            List<TourAppointmentVM> tourAppointments= new List<TourAppointmentVM>();
            foreach(var tour in TourAppointmentRepository.GetAll())
            {
                tourAppointments.Add(new TourAppointmentVM(tour));
            }
            return tourAppointments;
        }
        //------for a functionality-------
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
        public void MakeTourAppointments(TourVM tourVM)
        {
            Tour tour = tourVM.GetTour();
            foreach (var date in tourVM.dates)
            {
                TourAppointment tourAppointment = new TourAppointment(date, tourVM.Id, tour);
                Add(tourAppointment);
            }
        }
        //--------------------------------
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
