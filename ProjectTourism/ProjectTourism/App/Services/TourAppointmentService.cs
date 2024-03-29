﻿using System;
using System.Collections.Generic;
using ProjectTourism.Domain.IRepositories;
using ProjectTourism.Model;

namespace ProjectTourism.Services
{
    public class TourAppointmentService
    {
        private ITourAppointmentRepository TourAppointmentRepository;
        public TourAppointmentService()
        {
            TourAppointmentRepository = Injector.Injector.CreateInstance<ITourAppointmentRepository>();
        }
        public void MakeTourAppointments(Tour tour)
        {
            foreach (var date in tour.dates)
            {
                TourAppointment tourAppointment = new TourAppointment(date, tour.Id, tour);
                TourAppointmentRepository.Add(tourAppointment);
            }
        }
        public void Add(TourAppointment tourAppointment)
        {
            TourAppointmentRepository.Add(tourAppointment);
        }
        public void Delete(int tourAppointmentId)
        {
            TourAppointmentRepository.Delete(tourAppointmentId);
        }
        public TourAppointment GetOne(int id)
        {
            return TourAppointmentRepository.GetOne(id);
        }
        public List<TourAppointment> GetAll()
        {
            return TourAppointmentRepository.GetAll();
        }
        public void Update(TourAppointment tourApp)
        {
            TourAppointmentRepository.Update(tourApp);
        }
        public TourAppointment GetByDate(int tourId, DateTime date)
        {
            return TourAppointmentRepository.GetByDate(tourId, date);
        }
        public List<TourAppointment> GetAllByTour(int id)
        {
            return TourAppointmentRepository.GetAllByTour(id);
        }
        private static bool AppointmentAdditionIsValid(string username, TourAppointment tourAppointment)
        {
            return tourAppointment.Tour.GuideUsername.Equals(username) && tourAppointment.TourDateTime.Date.Equals(DateTime.Now.Date);
        }
    }
}
