using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
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
        public void Add(TourAppointment tourApp)
        {
            CanceledTourAppointmentsRepo.Add(tourApp);
        }
        public List<TourAppointment> GetAll()
        {
            return CanceledTourAppointmentsRepo.GetAll();
        }
    }
}
