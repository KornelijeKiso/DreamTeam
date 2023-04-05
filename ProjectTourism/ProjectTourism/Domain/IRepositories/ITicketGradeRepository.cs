using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ITicketGradeRepository
    {
        TicketGrade GetOne(int Id);
        List<TicketGrade> GetAll();
        void Add(TicketGrade ticketGrade);
        void Delete(TicketGrade ticketGrade);
        void Update(TicketGrade ticketGrade);
        TicketGrade GetOneByTicket(int ticketId);
    }
}
