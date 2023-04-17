﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProjectTourism.Model
{
    public class TicketGrade : Serializable
    {
        public int Id;
        public int TicketId;
        public Ticket Ticket;
        public static readonly string[] CategoryNames = { "GuidesKnowledge", "GuidesLanguage", "Interesting" };
        public Dictionary<string, int> Grades;
        public string Comment;
        public string? PictureURLs;
        public string[] Pictures;
        public bool IsNotReported;


        public TicketGrade()
        {
            Grades = new Dictionary<string, int>();
            foreach (var category in CategoryNames)
            {
                Grades.Add(category, 0);
            }
            IsNotReported = true;
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

        public string[] ToCSV()
        {
            List<string> csv = new List<string>();
            csv.Add(Id.ToString());
            csv.Add(TicketId.ToString());
            foreach (var category in CategoryNames)
            {
                csv.Add(Grades[category].ToString());
            }
            csv.Add(Comment);
            csv.Add(PictureURLs);
            csv.Add(IsNotReported.ToString());
            string[] csvValues = csv.ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TicketId = int.Parse(values[1]);
            for (int i = 2; i < 5; i++)
            {
                Grades[CategoryNames[i - 2]] = int.Parse(values[i]);
            }
            Comment = values[5];
            PictureURLs = values[6];
            IsNotReported = bool.Parse(values[7]);
            Pictures = GetPictureURLsFromCSV();
        }
    }
}
