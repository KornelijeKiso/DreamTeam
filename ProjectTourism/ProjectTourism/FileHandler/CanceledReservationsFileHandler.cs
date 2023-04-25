using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class CanceledReservationFileHandler
    {
        private Serializer<Reservation> Serializer;
        private readonly string Filename = "../../../References/canceledReservations.csv";
        private List<Reservation> Reservations;

        public CanceledReservationFileHandler()
        {
            Serializer = new Serializer<Reservation>();
        }

        public List<Reservation> Load()
        {
            Reservations = Serializer.fromCSV(Filename);
            return Reservations;
        }

        public void Save(List<Reservation> reservations)
        {
            Serializer.toCSV(Filename, reservations);
        }
    }
}
