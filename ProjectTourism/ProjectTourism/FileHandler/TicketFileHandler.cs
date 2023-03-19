using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class TicketFileHandler
    {
        private Serializer<Ticket> Serializer;
        private readonly string Filename = "../../../References/tickets.csv";
        private List<Ticket> Tickets;

        public TicketFileHandler()
        {
            Serializer = new Serializer<Ticket>();
        }

        public List<Ticket> Load()
        {
            Tickets = Serializer.fromCSV(Filename);
            return Tickets;
        }

        public void Save(List<Ticket> tickets)
        {
            Serializer.toCSV(Filename, tickets);
        }
    }
}
