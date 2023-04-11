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
        
        public TourAppointmentService(ITourAppointmentRepository tourAppointmentRepo)
        {
            TourAppointmentRepository = tourAppointmentRepo;
            Observers = new List<IObserver>();
        }
        public void Add(TourAppointmentVM tourAppointmentVM)
        {
            TourAppointmentRepository.Add(tourAppointmentVM.GetTourAppointment());
        }
        public void Delete(int tourAppointmentId)
        {
            TourAppointmentRepository.Delete(tourAppointmentId);
        }
        public TourAppointmentVM GetOne(int id)
        {
            return new TourAppointmentVM(TourAppointmentRepository.GetOne(id));
        }
        public List<TourAppointmentVM> GetAll()
        {
            List<TourAppointmentVM> tourAppointments = new List<TourAppointmentVM>();
            foreach (var tour in TourAppointmentRepository.GetAll())
            {
                tourAppointments.Add(new TourAppointmentVM(tour));
            }
            return tourAppointments;
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
        public void ChangeState(TourAppointmentVM tourApp)
        {
            TourAppointmentRepository.ChangeState(tourApp.GetTourAppointment());
        }
        public string GetNextStop(TourVM tour, int checkpointIndex)
        {
            return TourAppointmentRepository.GetNextStop(tour, checkpointIndex);
        }
        public void ChangeCurrentStop(TourAppointmentVM tourAppVM)
        {
            TourAppointmentRepository.ChangeCurrentStop(tourAppVM);
        }

        public void MakeTourAppointments(TourVM tour)
        {
            TourAppointmentRepository.MakeTourAppointments(tour);
        }
    }
}
