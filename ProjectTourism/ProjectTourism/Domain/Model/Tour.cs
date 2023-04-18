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
using System.Xml.Linq;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace ProjectTourism.Model
{
    public class Tour : Serializable
    {
        public int Id;
        public string Name;
        public int LocationId;
        public Location Location;
        public string? Description;
        public string Language;
        public int MaxNumberOfGuests;
        public string? Stops;
        public string Start;
        public string Finish;        
        public List<DateTime> dates { get; set; }
        public double Duration;
        public string? PictureURLs;
        public string[] Pictures;
        public string GuideUsername;
        public List<string> StopsList { get; set; }
        public Guide Guide;
        public List<TourAppointment> TourAppointments { get; set; }
        

        public Tour()
        {
            StopsList = new List<string>();
            dates = new List<DateTime>();
            TourAppointments = new List<TourAppointment>();
        }

        public Tour(Tour t)
        {
            Id = t.Id;
            Name = t.Name;
            Location = t.Location;
            Description = t.Description;
            Language = t.Language;
            MaxNumberOfGuests = t.MaxNumberOfGuests;
            Start = t.Start;
            Stops = t.Stops;
            Finish = t.Finish;
            Duration = t.Duration;
            PictureURLs = t.PictureURLs;
            GuideUsername = t.GuideUsername;
            Pictures = t.Pictures;
            StopsList = t.StopsList;
            dates = t.dates;
        }
        public Tour(string name, Location location, string description, string language, int maxNumberOfGuests, string start, string stops, string finish, DateTime startDate, double duration, string images, string guideUsername)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberOfGuests = maxNumberOfGuests;
            Start = start;
            Stops = stops;
            Finish = finish;
            Duration = duration;
            PictureURLs = images;
            GuideUsername = guideUsername;
            StopsList = new List<string>();
            dates = new List<DateTime>();
        }
        public Tour(int id, string name, Location location, string description, string language, int maxNumberOfGuests, string start, string stops, string finish, DateTime startDate, double duration, string images, string guideUsername)
        {
            Id = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxNumberOfGuests = maxNumberOfGuests;
            Start = start;
            Stops = stops;
            Finish = finish;
            Duration = duration;
            PictureURLs = images;
            GuideUsername = guideUsername;
            Pictures = GetPictureURLsFromCSV();
            StopsList = new List<string>();
            dates = new List<DateTime>();
        }

        
        public string[] GetPictureURLsFromCSV()
        {
            string[] pictures = PictureURLs.Split(',');
            foreach (var picture in pictures)
            {
                picture.Trim();
            }
            return pictures;
        }

        private List<string> GetStops(string start, string stops, string finish)
        {
            List<string> listStops = new List<string>();
            listStops.Add(start);
            string[] oneStop = stops.Split(',');
            foreach(string stop in oneStop)
            {
                stop.Trim();
                listStops.Add(stop);
            }
            listStops.Add(finish);
            return listStops;
        }
       
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            Language = values[3];
            MaxNumberOfGuests = int.Parse(values[4]);
            Start = values[5];
            Stops = values[6];
            Finish = values[7];
            Duration = double.Parse(values[8]);
            PictureURLs = values[9];
            GuideUsername = values[10];
            LocationId = int.Parse(values[11]);
            Pictures = GetPictureURLsFromCSV();
            StopsList = GetStops(Start, Stops, Finish);
        }

        
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Description,
                Language,
                MaxNumberOfGuests.ToString(),
                Start,
                Stops,
                Finish,
                Duration.ToString(),
                PictureURLs,
                GuideUsername,
                LocationId.ToString()
            };
            return csvValues;
        }
    }
}
