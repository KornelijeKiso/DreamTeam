using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.FileHandler
{
    public class TicketGradeFileHandler
    {
        private Serializer<TicketGrade> Serializer;
        private readonly string Filename = "../../../References/ticketGrades.csv";
        private List<TicketGrade> TicketGrades;

        public TicketGradeFileHandler()
        {
            Serializer = new Serializer<TicketGrade>();
            TicketGrades = Serializer.fromCSV(Filename);
        }

        public List<TicketGrade> Load()
        {
            TicketGrades = Serializer.fromCSV(Filename);
            return TicketGrades;
        }

        public void Save(List<TicketGrade> ticketGrades)
        {
            Serializer.toCSV(Filename, ticketGrades);
        }
    }
}
