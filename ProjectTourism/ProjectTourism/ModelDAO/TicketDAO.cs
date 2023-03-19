using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.ModelDAO
{
    public class TicketDAO
    {
        public List<IObserver> Observers;
        public TicketFileHandler FileHandler { get; set; }
        public List<Ticket> Tickets { get; set; }
        public TicketDAO()
        {
            Observers = new List<IObserver>();
            FileHandler = new TicketFileHandler();
            Tickets = FileHandler.Load();
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
                foreach (var tickets in Tickets)
                {
                    id = tickets.Id + 1;
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

        public void Update(Ticket ticket)
        {
            Ticket updated = GetOne(ticket.Id);
            updated.TourAppointmentId = ticket.TourAppointmentId;
            updated.Guest2Username = ticket.Guest2Username;
            updated.NumberOfGuests = ticket.NumberOfGuests;
            updated.TourStop = ticket.TourStop;

            //Delete(GetOne(ticket.Id));
            //Add(updated);
            FileHandler.Save(Tickets);
            NotifyObservers();
        }

        public void ChangeAppointment(Ticket ticket)
        {
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            //TourAppointment newAppointment = tourAppointmentDAO.GetByDate(ticket.TourAppointment.TourDateTime);
            /*foreach (var ticket1 in Tickets)
            {
                if (ticket.Id == ticket1.Id)
                {
                    ticket1.TourAppointmentId = newAppointment.Id;
                    ticket1.TourAppointment = newAppointment;
                }
            }
            FileHandler.Save(Tickets);*/
        }
        public void Delete(Ticket ticket)
        {
            Tickets.Remove(ticket);
            FileHandler.Save(Tickets);
        }
        public List<Ticket> GetAll()
        {
            return Tickets;
        }
        public Ticket GetOne(int id)
        {
            foreach (var ticket in Tickets)
            {
                if (ticket.Id == id) return ticket;
            }
            return null;
        }

        public List<Ticket> GetByAppointment(TourAppointment tourApp)
        {
            List<Ticket> ticketsByApp = new List<Ticket>();

            foreach (var ticket in Tickets)
            {
                if (tourApp.Id == ticket.TourAppointmentId)
                {
                    ticketsByApp.Add(ticket);
                }
            }
            return ticketsByApp;
        }
        
        // returns all guest's tickets
        public List<Ticket> GetByGuest(Guest2 guest2)
        {
            List<Ticket> ticketsByGuest = new List<Ticket>();

            foreach (var ticket in Tickets)
            {
                if (guest2.Username.Equals(ticket.Guest2Username))
                {
                    ticketsByGuest.Add(ticket);
                }
            }
            return ticketsByGuest;
        }
        
        // returns guest's ticket that should be updated
        public Ticket GetGuest2Ticket(Guest2 guest2, TourAppointment tourApp)
        {
            List<Ticket> ticketsByGuest = GetByGuest(guest2);

            foreach (var ticket in ticketsByGuest)
            {
                if (ticket.TourAppointmentId == tourApp.Id)
                {
                    return ticket;
                }
            }
            return null;
        }

        public void CheckGuestStatus(int tourAppId, int ticketId)
        {
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            TourAppointment TourAppointment = tourAppointmentDAO.GetOne(tourAppId);

            Ticket ticket = GetOne(ticketId);
            if (TourAppointment.CurrentTourStop.Equals(ticket.TourStop) && TourAppointment.State == TOURSTATE.STARTED)
                ticket.HasGuideChecked = true;

            //if (TourAppointment.CurrentTourStop.Equals(TourAppointment.Tour.Finish)) //This is a situation where guests confirmed their arrival at the last stop
            //    ticket.HasGuestConfirmed = true;

            FileHandler.Save(Tickets);
            NotifyObservers();
        }

        public void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Update();
            }
        }
    }
}
