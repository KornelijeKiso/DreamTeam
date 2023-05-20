using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ProjectTourism.Domain.Model
{
    public class Notification:Serializable
    {
        public int Id;
        public string Username;
        public string Title;
        public string Text;
        public DateTime Time;
        public bool New;

        public Notification() { }
        public Notification(string title, string text, string username) 
        {
            Title = title;
            Text = text;
            Username = username;
            Time = DateTime.Now;
            New = true;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Username,
                Title,
                Text,
                New.ToString(),
                Time.ToString("dd.MM.yyyy HH:mm")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Title = values[2];
            Text = values[3];
            New = bool.Parse(values[4]);
            if (DateTime.TryParse(values[5], new CultureInfo("en-GB"), DateTimeStyles.None, out var dateTimeParsed))
                Time = dateTimeParsed;
        }
    }
}
