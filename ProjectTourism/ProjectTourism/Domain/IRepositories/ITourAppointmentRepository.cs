using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ITourAppointmentRepository
    {
        TourAppointment GetOne(int tourAppointmentId);
        List<TourAppointment> GetAll();
        void Add(TourAppointment tourAppointment);
        void Delete(int tourAppointmentId);
        void Update(TourAppointment tourAppointment);
        TourAppointment GetByDate(int tourId, DateTime date);
        List<TourAppointment> GetAllByTour(int id);
    }
}
