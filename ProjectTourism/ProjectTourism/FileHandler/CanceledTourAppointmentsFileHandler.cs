using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;

namespace ProjectTourism.FileHandler
{
    public class CanceledTourAppointmentsFileHandler
    {
        private Serializer<TourAppointment> Serializer;
        private readonly string Filename = "../../../References/canceledAppointments.csv";
        private List<TourAppointment> CanceledAppointments;

        public CanceledTourAppointmentsFileHandler()
        {
            Serializer = new Serializer<TourAppointment>();
        }

        public List<TourAppointment> Load()
        {
            CanceledAppointments = Serializer.fromCSV(Filename);
            return CanceledAppointments;
        }
        public void Save(List<TourAppointment> canceledAppointments)
        {
            Serializer.toCSV(Filename, canceledAppointments);
        }
    }
}
