using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Services
{
    public class CanceledTourAppointmentsService
    {
        private ICanceledTourAppointmentsRepository CanceledTourAppointmentsRepo;

        public CanceledTourAppointmentsService(ICanceledTourAppointmentsRepository iar)
        {
            CanceledTourAppointmentsRepo = iar;
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
