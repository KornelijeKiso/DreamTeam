using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.FileHandler;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Repositories
{
    public class TourAppointmentRepository : ITourAppointmentRepository
    {
        public TourAppointmentFileHandler FileHandler { get; set; }
        public List<TourAppointment> TourAppointments { get; set; }
        public TourAppointmentRepository()
        {
            FileHandler = new TourAppointmentFileHandler();
            TourAppointments = FileHandler.Load();
            Synchronize();
        }
        public void Synchronize()
        {
            ITourRepository tourRepository = new TourRepository();
            foreach (var tourApp in TourAppointments)
            {
                Tour tour = tourRepository.GetOne(tourApp.TourId);
                tourApp.Tour = tour;
            }

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
        public void MakeTourAppointments(TourVM tourVM)
        {
            Tour tour = tourVM.GetTour();
            foreach (var date in tourVM.dates)
            {
                TourAppointment tourAppointment = new TourAppointment(date, tourVM.Id, tour);
                Add(tourAppointment);
            }
        }
        public void Delete(int tourAppointmentId)
        {
            TourAppointment tourAppointment = GetOne(tourAppointmentId);
            if (tourAppointment == null) return;
            TourAppointments.Remove(tourAppointment);
            FileHandler.Save(TourAppointments);
        }
        public void Delete(TourAppointment tourAppointment)
        {
            TourAppointments.Remove(tourAppointment);
            FileHandler.Save(TourAppointments);
        }

        public List<TourAppointment> GetAll()
        {
            ITourRepository tourRepository = new TourRepository();
            foreach(var tourApp in TourAppointments)
            {
                tourApp.Tour = tourRepository.GetOne(tourApp.TourId);
            }
            return TourAppointments;
        }

        public TourAppointment GetOne(int tourAppointmentId)
        {
            foreach(var tourAppointment in TourAppointments)
            {
                if (tourAppointment.Id == tourAppointmentId)
                    return tourAppointment;
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
        }
        public void UpdateAppointmentTicket(int tourAppointmentId, Ticket ReturnedTicket)
        {
            TourAppointment tourAppointment = GetOne(tourAppointmentId);
            tourAppointment.AvailableSeats = tourAppointment.Tour.MaxNumberOfGuests;
            TicketDAO ticketDAO = new TicketDAO();
            List<Ticket> tickets = ticketDAO.GetByAppointment(tourAppointment.Id);
            foreach (Ticket ticket in tickets)
            {
                tourAppointment.AvailableSeats -= ticket.NumberOfGuests;
            }
            FileHandler.Save(TourAppointments);
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
        public string GetNextStop(TourVM tour, int checkpointIndex)
        {
            TourService tourService = new TourService(new TourRepository());
            List<string> stops = tourService.GetStops(tour);
            tour.StopsList = stops;

            if (checkpointIndex < 0 || checkpointIndex >= tour.StopsList.Count - 1)
            {
                throw new ArgumentException("Invalid stop index");
            }

            return tour.StopsList[checkpointIndex + 1];
        }
        public void ChangeCurrentStop(TourAppointmentVM tourAppVM)
        {
            foreach (var tourApp in TourAppointments)
            {
                if (tourApp.Id == tourAppVM.Id)
                {
                    tourApp.CurrentTourStop = tourAppVM.CurrentTourStop;
                }
            }
            FileHandler.Save(TourAppointments);
        }
        public void Update(TourAppointment tourAppointment)
        {
            throw new NotImplementedException();
        }
    }
}
