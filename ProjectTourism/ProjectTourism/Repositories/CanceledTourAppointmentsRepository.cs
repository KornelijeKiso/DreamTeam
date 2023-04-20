using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;

namespace ProjectTourism.Repositories
{
    public class CanceledTourAppointmentsRepository: ICanceledTourAppointmentsRepository
    {
        public CanceledTourAppointmentsFileHandler FileHandler { get; set; }
        public List<TourAppointment> CanceledAppointments { get; set; }
        public CanceledTourAppointmentsRepository()
        {
            FileHandler = new CanceledTourAppointmentsFileHandler();
            CanceledAppointments = FileHandler.Load();
        }
        public void Add(TourAppointment appointment)
        {
            CanceledAppointments.Add(appointment);
            FileHandler.Save(CanceledAppointments);
        }
        public List<TourAppointment> GetAll()
        {
            return CanceledAppointments;
        }
    }
}
