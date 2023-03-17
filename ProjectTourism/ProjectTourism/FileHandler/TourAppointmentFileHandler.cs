using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class TourAppointmentFileHandler
    {
        private Serializer<TourAppointment> Serializer;
        private readonly string Filename = "../../../References/appointments.csv";
        private List<TourAppointment> TourAppointments;

        public TourAppointmentFileHandler()
        {
            Serializer = new Serializer<TourAppointment>();
            TourAppointments = Serializer.fromCSV(Filename);
        }

        public List<TourAppointment> Load()
        {
            TourAppointments = Serializer.fromCSV(Filename);
            return TourAppointments;
        }

        public void Save(List<TourAppointment> tourAppointment)
        {
            Serializer.toCSV(Filename, tourAppointment);
        }
    }
}
