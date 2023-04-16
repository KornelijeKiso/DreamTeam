using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


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
        public int AvailableSeats;
        public TOURSTATE State;
        public bool IsNotFinished;
        public bool IsFinished;
        public int Visits;

        public TourAppointment()
        { 
            Tickets = new List<Ticket>();
        }

        public TourAppointment(DateTime tourDateTime, int tourId, Tour tour)
        {
            TourDateTime = tourDateTime;
            TourId = tourId;
            Tour = tour;
            CurrentTourStop = Tour.Start;
            AvailableSeats = Tour.MaxNumberOfGuests;
            IsNotFinished = true;
            IsFinished = false;
            State = TOURSTATE.READY;
            Tickets = new List<Ticket>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourId.ToString(),
                TourDateTime.ToString("dd.MM.yyyy HH:mm"),
                CurrentTourStop.ToString(),
                AvailableSeats.ToString(),
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
            AvailableSeats = Convert.ToInt32(values[4]);
            switch (values[5])
            {
                case "READY":
                    { State = TOURSTATE.READY; IsNotFinished = true; break; }
                case "STARTED":
                    { State = TOURSTATE.STARTED; IsNotFinished = true; break; }
                case "FINISHED":
                    { State = TOURSTATE.FINISHED; IsNotFinished = false; break; }
                case "STOPPED":
                    { State = TOURSTATE.STOPPED; IsNotFinished = false; break; }
                default:
                    { State = TOURSTATE.READY; IsNotFinished = true; break; }
            }
        }
    }
}
