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
using ProjectTourism.ModelDAO;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.ViewModel;

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
        public DateTime StartDate;
        public List<DateTime> dates { get; set; }
        public double Duration;
        public string? PictureURLs;
        public string[] Pictures;
        public string GuideUsername;
        public List<string> StopsList { get; set; }
        public Guide Guide;
        public bool IsValid;
        

        public Tour()
        {
            StopsList = new List<string>();
            dates = new List<DateTime>();
            IsValid = false;
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
            StartDate = startDate;
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
            StartDate = startDate;
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

        public List<DateTime> FromString(string dateString)
        {
            List<DateTime> dates = new List<DateTime>();
            string[] oneDate = dateString.Split(',');
            foreach (var date in oneDate)
            {
                if (DateTime.TryParse(date.Trim(), new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                    dates.Add(dateTimeParsed);
            }
            return dates;
        }
        //public string ToString(List<DateTime> dates)
        //{
        //    string dateString = "";
        //    int i;
        //    for (i = 0; i < dates.Count(); i++)
        //    {
        //        dateString += dates[i].ToString("dd.MM.yyyy HH:mm") + ",";
        //    }
        //    return dateString;
        //}

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
         //   dates = FromString(values[8]);
            Duration = double.Parse(values[8]);
            PictureURLs = values[9];
            GuideUsername = values[10];
            LocationId = int.Parse(values[11]);
            Pictures = GetPictureURLsFromCSV();
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
             //   ToString(dates),
                Duration.ToString(),
                PictureURLs,
                GuideUsername,
                LocationId.ToString()
            };
            return csvValues;
        }
    }
}
