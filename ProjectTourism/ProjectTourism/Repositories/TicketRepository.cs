using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
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
            Synchronize();
        }

        public void Synchronize() 
        {
            //ITourAppointmentRepository tourAppointmentRepository = new TourAppointmentRepository();
            IGuest2Repository guest2Repository = new Guest2Repository();
            foreach (var ticket in Tickets)
            {
                //TourAppointment tourAppointment = tourAppointmentRepository.GetOne(ticket.TourAppointmentId);
                //ticket.TourAppointment = tourAppointment;
                //ticket.TourStop = tourAppointment.Tour.Start;

                Guest2 guest2 = guest2Repository.GetOne(ticket.Guest2Username);
                ticket.Guest2 = guest2;
            }
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

        public int GenerateId()
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
                }
            }
        }

        //////////////
        public List<Ticket> GetByAppointment(int tourAppointmentId)   // same as tourAppointment.Tickets
        {
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            TourAppointment tourAppointment = tourAppointmentDAO.GetOne(tourAppointmentId);     //
            List<Ticket> ticketsByApp = new List<Ticket>();

            foreach (var ticket in Tickets)
            {
                if (tourAppointment.Id == ticket.TourAppointmentId)
                {
                    ticketsByApp.Add(ticket);
                }
            }
            return ticketsByApp;
        }

        public List<Ticket> GetByGuest(string guest2Username)
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

        public void CheckGuestStatus(int tourAppId, int ticketId)
        {
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            TourAppointment TourAppointment = tourAppointmentDAO.GetOne(tourAppId);
            Ticket ticket = GetOne(ticketId);
            if ((TourAppointment.CurrentTourStop.Equals(ticket.TourStop)) && (TourAppointment.State == TOURSTATE.STARTED))
                ticket.HasGuideChecked = true;

            FileHandler.Save(Tickets);
        }

        public void GuideCheck(Ticket ticket)
        {
            foreach (Ticket checkTicket in Tickets)
            {
                if (checkTicket.Id == ticket.Id)
                {
                    checkTicket.HasGuideChecked = true;
                    break;
                }
            }
            FileHandler.Save(Tickets);
        }
    }
}
