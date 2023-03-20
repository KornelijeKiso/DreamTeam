using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.ModelDAO
{
    public class TourAppointmentDAO
    {
        public List<IObserver> Observers;
        public TourAppointmentFileHandler FileHandler { get; set; }
        public List<TourAppointment> TourAppointments { get; set; }
        public TourAppointmentDAO()
        {
            Observers = new List<IObserver>();
            FileHandler= new TourAppointmentFileHandler();
            TourAppointments = FileHandler.Load();
        }
        public int GenerateId()
        {
            if (TourAppointments.Count == 0) return 0;
            return TourAppointments.Last<TourAppointment>().Id + 1;
        }

        public void Add(TourAppointment addedTourApp)
        {
            if (addedTourApp == null)
            {
                addedTourApp.Id = 0;
            }
            else
            {
                addedTourApp.Id = GenerateId();
                TourAppointments.Add(addedTourApp);
                FileHandler.Save(TourAppointments);
            }
        }
        public void MakeTourAppointments(Tour route)
        {
            foreach(var date in route.dates)
            {
                TourAppointment tourAppointment = new TourAppointment(date, route.Id, route);
                Add(tourAppointment);
            }
        }
        public List<TourAppointment> GetAll()
        {
            return TourAppointments;
        }
        public TourAppointment GetOne(int id)
        {
            foreach (TourAppointment tours in TourAppointments)
            {
                if (tours.Id == id) 
                    return tours;
            }
            return null;
        }
        public List<TourAppointment> GetByTour(int id)
        {
            List<TourAppointment> toursById = new List<TourAppointment>();
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.TourId == id)
                    toursById.Add(tourApp);
            }
            return toursById;
        }
        public TourAppointment GetByDate(int tourId, DateTime date)
        {
            foreach (TourAppointment tours in GetByTour(tourId))
            {
                if (tours.TourDateTime.Equals(date)) return tours;
            }
            return null;
        }
        
        public void UpdateAppointmentCreate(int tourAppointmentId, Ticket ticket)
        {
            TourAppointment tourAppointment = GetOne(tourAppointmentId);
            tourAppointment.AvailableSeats -= ticket.NumberOfGuests;
            tourAppointment.Tickets.Add(ticket);

            FileHandler.Save(TourAppointments);
            NotifyObservers();
        }
        public void UpdateAppointmentReturn(int tourAppointmentId, Ticket ReturnedTicket)
        {
            TourAppointment tourAppointment = GetOne(tourAppointmentId);
            tourAppointment.AvailableSeats += ReturnedTicket.NumberOfGuests;
            for (int i = 0; i < tourAppointment.Tickets.Count(); i++)
            {
                if (tourAppointment.Tickets[i].Id == ReturnedTicket.Id)
                {
                    tourAppointment.Tickets.Remove(ReturnedTicket);
                }
            }
            FileHandler.Save(TourAppointments);
            NotifyObservers();
        }
        public void UpdateAppointmentUpdate(int tourAppointmentId, Ticket ReturnedTicket)
        {
            TourAppointment tourAppointment = GetOne(tourAppointmentId);
            tourAppointment.AvailableSeats = tourAppointment.Tour.MaxNumberOfGuests;
            TicketDAO ticketDAO = new TicketDAO();
            List<Ticket> tickets = ticketDAO.GetByAppointment(tourAppointment.Id); // same as tourAppointment.Tickets
            foreach (Ticket ticket in tickets)
            {
                tourAppointment.AvailableSeats -= ticket.NumberOfGuests;
            }
            FileHandler.Save(TourAppointments);
            NotifyObservers();
        }
        public void ChangeState(TourAppointment tourAppointment)
        {
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.Id == tourAppointment.Id)
                {
                    tourApp.State = tourAppointment.State;
                }
            }
            FileHandler.Save(TourAppointments);
        }
        public void ChangeCurrentStop(TourAppointment tourAppointment)
        {
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.Id == tourAppointment.Id)
                {
                    tourApp.CurrentTourStop = tourAppointment.CurrentTourStop;
                }
            }
            FileHandler.Save(TourAppointments);
        }
        public string GetNextStop(Tour tour, int stopint)
        {
            TourDAO tourDAO = new TourDAO();
            List<string> stops = tourDAO.GetStops(tour);
            tour.StopsList = stops;

            if (stopint < 0 || stopint >= tour.StopsList.Count - 1)
            {
                throw new ArgumentException("Invalid stop index");
            }

            return tour.StopsList[stopint + 1];
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
