using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ITourAppointmentRepository
    {
        TourAppointment GetOne(int tourAppointmentId);
        List<TourAppointment> GetAll();
        void Add(TourAppointment tourAppointment);
        void Delete(TourAppointment tourAppointment);
        void Update(TourAppointment tourAppointment);
    }
}
