using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ICanceledTourAppointmentsRepository
    {
        public void Add(TourAppointment tourAppointment);
        public List<TourAppointment> GetAll();
    }
}
