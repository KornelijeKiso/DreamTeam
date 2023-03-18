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
        public List<TourAppointment> GetByRoute(int id)
        {
            return TourAppointmentDAO.GetByRoute(id);
        }
        public List<Guest2> GetGuests(List<Ticket> tickets)
        {
            return TourAppointmentDAO.GetGuests(tickets);
        }

        public void Add(TourAppointment addedTourApp)
        {
            TourAppointmentDAO.Add(addedTourApp);
        }
        public TourAppointment GetOne(int id) 
        {
            return TourAppointmentDAO.GetOne(id);
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

        public string GetNextStop(Route route, int stopint)
        {
            RouteDAO routeDAO = new RouteDAO();
            List<string> pom = routeDAO.GetStops(route);
            route.StopsList = pom;

            if (stopint < 0 || stopint >= route.StopsList.Count - 1)
            {
                throw new ArgumentException("Invalid stop index");
            }
            
            return route.StopsList[stopint + 1];
        }

        public void MakeTourAppointments(Route route)
        {
            TourAppointmentDAO.MakeTourAppointments(route);
        }
    }
    
}