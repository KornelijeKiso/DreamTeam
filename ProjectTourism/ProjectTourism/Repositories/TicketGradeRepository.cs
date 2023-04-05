using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    internal class TicketGradeRepository : ITicketGradeRepository
    {
        public TicketGradeFileHandler FileHandler { get; set; }
        public List<TicketGrade> TicketGrades { get; set; }
        public TicketGradeRepository()
        {
            FileHandler = new TicketGradeFileHandler();
            TicketGrades = FileHandler.Load();
        }

        public TicketGrade GetOne(int id)
        {
            foreach (var ticketGrade in TicketGrades)
            {
                if (ticketGrade.Id == id) return ticketGrade;
            }
            return null;
        }

        public List<TicketGrade> GetAll()
        {
            return TicketGrades;
        }

        public int GenerateId()
        {
            int id = 0;
            if (TicketGrades == null)
            {
                id = 0;
            }
            else
            {
                foreach (var ticketGrade in TicketGrades)
                {
                    id = ticketGrade.Id + 1;
                }
            }
            return id;
        }

        public void Add(TicketGrade addedTicketGrade)
        {
            addedTicketGrade.Id = GenerateId();
            TicketGrades.Add(addedTicketGrade);
            FileHandler.Save(TicketGrades);
        }

        public void Delete(TicketGrade ticketGrade)
        {
            TicketGrades.Remove(ticketGrade);
            FileHandler.Save(TicketGrades);
        }

        public void Update(TicketGrade ticketGrade)
        {
            foreach (var existingTicketGrade in TicketGrades)
            {
                if (existingTicketGrade.Id == ticketGrade.Id)
                {
                    existingTicketGrade.TicketId = ticketGrade.TicketId;
                    existingTicketGrade.Comment = ticketGrade.Comment;
                    existingTicketGrade.PictureURLs = ticketGrade.PictureURLs;
                }
            }
        }

        public TicketGrade GetOneByTicket(int ticketId)
        {
            foreach (var ticketGrade in TicketGrades)
            {
                if (ticketGrade.TicketId == ticketId) return ticketGrade;
            }
            return null;
        }
    }
}
