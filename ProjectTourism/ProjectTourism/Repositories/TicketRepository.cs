using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public TicketFileHandler FileHandler { get; set; }
        public List<Ticket> Tickets { get; set; }
        
        public TicketRepository()
        {
            FileHandler = new TicketFileHandler();
            Tickets = FileHandler.Load();
        }
        public Ticket GetOne(int id)
        {
            foreach (var ticket in Tickets)
            {
                if (ticket.Id == id) return ticket;
            }
            return null;
        }

        public List<Ticket> GetAll()
        {
            return Tickets;
        }

        private int GenerateId()
        {
            int id = 0;
            if (Tickets == null)
            {
                id = 0;
            }
            else
            {
                foreach (var ticket in Tickets)
                {
                    id = ticket.Id + 1;
                }
            }
            return id;
        }

        public void Add(Ticket tickets)
        {
            tickets.Id = GenerateId();
            Tickets.Add(tickets);
            FileHandler.Save(Tickets);
        }

        public void Delete(Ticket ticket)
        {
            Tickets.Remove(ticket);
            FileHandler.Save(Tickets);
        }

        public void Update(Ticket ticket)
        {
            foreach (var existingTicket in Tickets)
            {
                if (existingTicket.Id == ticket.Id)
                {
                    existingTicket.TourAppointmentId = ticket.TourAppointmentId;
                    existingTicket.Guest2Username = ticket.Guest2Username;
                    existingTicket.NumberOfGuests = ticket.NumberOfGuests;
                    existingTicket.TourStop = ticket.TourStop;
                    existingTicket.HasGuideChecked = ticket.HasGuideChecked;
                    existingTicket.HasGuestConfirmed = ticket.HasGuestConfirmed;
                    break;
                }
            }
            FileHandler.Save(Tickets);
        }

        public List<Ticket> GetByAppointment(int tourAppointmentId)
        {
            List<Ticket> ticketsByApp = new List<Ticket>();

            foreach (var ticket in Tickets)
            {
                if (tourAppointmentId == ticket.TourAppointmentId)
                {
                    ticketsByApp.Add(ticket);
                }
            }
            return ticketsByApp;
        }

        public List<Ticket> GetByGuest2(string guest2Username)
        {
            List<Ticket> ticketsByGuest = new List<Ticket>();

            foreach (var ticket in Tickets)
            {
                if (guest2Username.Equals(ticket.Guest2Username))
                {
                    ticketsByGuest.Add(ticket);
                }
            }
            return ticketsByGuest;
        }

        public Ticket GetGuest2Ticket(string guest2Username, int tourAppId)
        {
            foreach (var ticket in Tickets)
            {
                if ((ticket.TourAppointmentId == tourAppId) && (guest2Username.Equals(ticket.Guest2Username)))
                    return ticket;
            }
            return null;
        }
    }
}
