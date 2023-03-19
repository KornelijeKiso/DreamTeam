﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Model;
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;

namespace ProjectTourism.Controller
{
    public class TourAppointmentController
    {
        public TourAppointmentDAO TourAppointmentDAO { get; set; }
        public TourAppointmentController()
        {
            TourAppointmentDAO = new TourAppointmentDAO();
        }
        public void Subscribe(IObserver observer)
        {
            TourAppointmentDAO.Subscribe(observer);
        }
        public void NotifyObservers()
        {
            TourAppointmentDAO.NotifyObservers();
        }
        public List<TourAppointment> GetByTour(int id)
        {
            return TourAppointmentDAO.GetByTour(id);
        }
        public List<Guest2> GetGuests(List<Ticket> tickets)
        {
            return TourAppointmentDAO.GetGuests(tickets);
        }
        public void UpdateAppointmentCreate(int tourAppointmentId, Ticket ticket)
        {
            TourAppointmentDAO.UpdateAppointmentCreate(tourAppointmentId, ticket);
        }
        public void UpdateAppointmentReturn(int tourAppointmentId, Ticket ReturnedTicket)
        {
            TourAppointmentDAO.UpdateAppointmentReturn(tourAppointmentId, ReturnedTicket);
        }
        public void UpdateAppointmentUpdate(int tourAppointmentId, Ticket ReturnedTicket)
        {
            TourAppointmentDAO.UpdateAppointmentUpdate(tourAppointmentId, ReturnedTicket);
        }
        public void Add(TourAppointment addedTourApp)
        {
            TourAppointmentDAO.Add(addedTourApp);
        }
        public void MakeTourAppointments(Tour route)
        {
            TourAppointmentDAO.MakeTourAppointments(route);
        }
        public TourAppointment GetOne(int id) 
        {
            return TourAppointmentDAO.GetOne(id);
        }
        public TourAppointment GetByDate(int tourId, DateTime date)
        {
            return TourAppointmentDAO.GetByDate(tourId, date);
        }
        public List<TourAppointment> GetAll()
        {
            return TourAppointmentDAO.GetAll();
        }
        public void ChangeState(TourAppointment tourAppointment)
        {
            TourAppointmentDAO.ChangeState(tourAppointment);
        }
        public void ChangeCurrentStop(TourAppointment tourAppointment)
        {
            TourAppointmentDAO.ChangeState(tourAppointment);
        }
        public string GetNextStop(Tour tour, int stopint)
        {
            return TourAppointmentDAO.GetNextStop(tour, stopint);
        }
        
    }
}
