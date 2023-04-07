using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
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
            Synchronize();
        }
        public void Synchronize()
        {

        }
        public int GenerateId()
        {
            int id = 0;
            if (TourAppointments == null)
                id = 0;
            else
                foreach (var reservation in TourAppointments)
                {
                    id = reservation.Id + 1;
                }
            return id;
        }
        public void Add(TourAppointment tourAppointment)
        {
            tourAppointment.Id = GenerateId();
            TourAppointments.Add(tourAppointment);
            FileHandler.Save(TourAppointments);
        }

        public void Delete(TourAppointment tourAppointment)
        {
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

        public void Update(TourAppointment tourAppointment)
        {
            throw new NotImplementedException();
        }
    }
}
