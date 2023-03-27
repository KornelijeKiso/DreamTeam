﻿using ProjectTourism.ModelDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProjectTourism.Model
{
    public class TourAppointmentGrade : Serializable, INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _TourAppointmentId;
        public int TourAppointmentId
        {
            get => _TourAppointmentId;
            set
            {
                if (value != _TourAppointmentId)
                {
                    _TourAppointmentId = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private TourAppointment _TourAppointment;
        public TourAppointment TourAppointment
        {
            get => _TourAppointment;
            set
            {
                if (value != _TourAppointment)
                {
                    _TourAppointment = value;
                    OnPropertyChanged();
                }
            }
        }

        public static readonly string[] CategoryNames = { "Guide's knoweledge", "Guide's language", "Interesting" };

        private Dictionary<string, int> _Grades;
        public Dictionary<string, int> Grades
        {
            get => _Grades;
            set
            {
                if (value != _Grades)
                {
                    _Grades = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _Comment;
        public string Comment
        {
            get => _Comment;
            set
            {
                if (value != _Comment)
                {
                    _Comment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _PictureURLs;
        public string? PictureURLs
        {
            get => _PictureURLs;
            set
            {
                _PictureURLs = value;
                OnPropertyChanged();
            }
        }

        private string[] _Pictures;
        public string[] Pictures
        {
            get => _Pictures;
            set
            {
                if (value != _Pictures)
                {
                    _Pictures = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourAppointmentGrade()
        {
            Grades = new Dictionary<string, int>();
            foreach (var category in CategoryNames)
            {
                Grades.Add(category, 0);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private TourAppointment FindTourAppointment(int tourAppointmentId)
        {
            TourAppointmentDAO tourAppointmentDAO = new TourAppointmentDAO();
            TourAppointment tourApp = tourAppointmentDAO.GetOne(tourAppointmentId);
            return tourApp;
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
            csv.Add(TourAppointmentId.ToString());
            foreach (var category in CategoryNames)
            {
                csv.Add(Grades[category].ToString());
            }
            csv.Add(Comment);
            csv.Add(PictureURLs);
            string[] csvValues = csv.ToArray();
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourAppointmentId = int.Parse(values[1]);
            TourAppointment = FindTourAppointment(TourAppointmentId);
            for (int i = 2; i < 5; i++)
            {
                Grades[CategoryNames[i - 3]] = int.Parse(values[i]);
            }
            Comment = values[5];
            PictureURLs = values[6];
            Pictures = GetPictureURLsFromCSV();
        }
    }
}
