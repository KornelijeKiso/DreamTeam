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

        public void MakeTourAppointments(Route route)
        {
            foreach(var date in route.dates)
            {
                TourAppointment appointment = new TourAppointment(date, route.Id, route);
                Add(appointment);
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
                if (tours.Id == id) return tours;
            }
            return null;
        }
        public List<TourAppointment> GetByRoute(int id)
        {
            List<TourAppointment> toursById= new List<TourAppointment>();
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.Id == id)
                    toursById.Add(tourApp);
            }
            return toursById;
        }

        public List<Guest2> GetGuests(List<Ticket> tickets)
        {
            List<Guest2> guest = new List<Guest2>();
            foreach(Ticket ticket in tickets)
            {
                guest.Add(ticket.Guest2);
            }

            return guest;
        }


        /*public void MakeTourAppointments(Route route)
        {
            foreach (DateTime date in route.dates)
            {
                TourAppointment tourApp = new TourAppointment();
                tourApp.Id = GenerateId();
                tourApp.TourDateTime = route.StartDate;
                RouteDAO tourDAO = new RouteDAO();
                tourApp.Route = route;
                tourApp.CurrentTourStop = route.Start;
                tourApp.AvailableSeats = route.MaxNumberOfGuests;
                tourApp.IsNotFinished = true;
                tourApp.State = TOURSTATE.READY;
                tourApp.Tickets = new List<Ticket>();
                tourApp.HasGuestConfirmed = new List<bool>();
                tourApp.HasGuideChecked = new List<bool>();
                TourAppointments.Add(tourApp);
                FileHandler.Save(TourAppointments);
            }
        }*/


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
