﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProjectTourism.Model
{
    public class TourAppointment: Serializable
    {
        public int Id;
        public DateTime TourDateTime;
        public string CurrentTourStop;
        public int TourId;
        public Tour Tour;
        public List<Ticket> Tickets;
        public TOURSTATE State;
        public List<TicketGrade> TicketGrades { get; set; }
        public TourAppointment()
        { 
            Tickets = new List<Ticket>();
            TicketGrades = new List<TicketGrade>();
        }
        public TourAppointment(DateTime tourDateTime, int tourId, Tour tour)
        {
            TourDateTime = tourDateTime;
            TourId = tourId;
            Tour = tour;
            CurrentTourStop = Tour.Start;
            State = TOURSTATE.READY;
            Tickets = new List<Ticket>();
            TicketGrades = new List<TicketGrade>();
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                TourDateTime.ToString("dd.MM.yyyy HH:mm"),
                CurrentTourStop.ToString(),
                State.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            if (DateTime.TryParse(values[2], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                TourDateTime = dateTimeParsed;
            CurrentTourStop = values[3];
            switch (values[4])
            {
                case "READY":
                    { State = TOURSTATE.READY; break; }
                case "STARTED":
                    { State = TOURSTATE.STARTED;break; }
                case "FINISHED":
                    { State = TOURSTATE.FINISHED; break; }
                case "STOPPED":
                    { State = TOURSTATE.STOPPED; break; }
                case "CANCELED":
                    { State = TOURSTATE.CANCELED; break; }
                case "EXPIRED":
                    { State = TOURSTATE.EXPIRED; break; }
                default:
                    { State = TOURSTATE.READY; break; }
            }
        }
    }
}
