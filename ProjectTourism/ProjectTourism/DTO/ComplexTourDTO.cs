﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.Services;

namespace ProjectTourism.DTO
{
    public class ComplexTourDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private ComplexTour _complexTour { get; set; }
        public ComplexTour GetComplexTour()
        {
            return _complexTour;
        }

        public ComplexTourDTO(ComplexTour complexTour)
        {
            _complexTour = complexTour;
            SynchronizeTourRequestsList(_complexTour);
        }
        private void SynchronizeTourRequestsList(ComplexTour complexTour)
        {
            TourRequestService tourRequestService = new TourRequestService();
            complexTour.TourRequests = new List<TourRequest>();

            string[] tourRequestIDs = complexTour.TourRequestString.Split(',');
            foreach(string tourRequestId in tourRequestIDs)
            {
                tourRequestId.Trim();
                complexTour.TourRequests.Add(tourRequestService.GetOne(int.Parse(tourRequestId)));
            }
        }
        public int Id
        {
            get => _complexTour.Id;
            set
            {
                if (value != _complexTour.Id)
                {
                    _complexTour.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Guest2Username
        {
            get => _complexTour.Guest2Username;
            set
            {
                if (value != _complexTour.Guest2Username)
                {
                    _complexTour.Guest2Username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TourRequestString
        {
            get => _complexTour.TourRequestString;
            set
            {
                if (value != _complexTour.TourRequestString)
                {
                    _complexTour.TourRequestString = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TourRequestDTO> TourRequests { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // validation
        public string Error => null;
        public string? this[string columnName]
        {
            get
            {
                if (columnName == "Guest2Username")
                {
                    if (string.IsNullOrEmpty(Guest2Username))
                        return "Username is required!";
                }
                else if (columnName == "TourRequestString")
                {
                    if (string.IsNullOrEmpty(TourRequestString))
                        return "TourRequestString is required!";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Guest2Username", "TourRequestString" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }
    }
}
