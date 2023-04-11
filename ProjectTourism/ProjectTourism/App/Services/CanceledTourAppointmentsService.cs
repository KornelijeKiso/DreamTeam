using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Observer;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Services
{
    public class CanceledTourAppointmentsService
    {
        public List<IObserver> Observers;
        private ICanceledTourAppointmentsRepository CanceledTourAppointmentsRepo;

        public CanceledTourAppointmentsService(ICanceledTourAppointmentsRepository iar)
        {
            CanceledTourAppointmentsRepo = iar;
            Observers = new List<IObserver>();
        }
        public void Add(TourAppointmentVM tourApp)
        {
            CanceledTourAppointmentsRepo.Add(tourApp.GetTourAppointment());
        }
        public List<TourAppointmentVM> GetAll()
        {
            List<TourAppointmentVM> tourApps = new List<TourAppointmentVM>();
            foreach(var tourApp in CanceledTourAppointmentsRepo.GetAll())
            {
                tourApps.Add(new TourAppointmentVM(tourApp));
            }
            return tourApps;
        }
    }
}
