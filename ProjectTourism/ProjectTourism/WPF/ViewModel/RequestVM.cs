﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;

namespace ProjectTourism.WPF.ViewModel
{
    public class RequestVM : INotifyPropertyChanged
    {
        private Request _request;
        public RequestVM(Request request)
        {
            _request = request;
        }
        public RequestVM(RequestVM request)
        {
            _request = new Request(request.GetRequest());
        }
        public Request GetRequest()
        {
            return _request;
        }

        public int Id
        {
            get => _request.Id;
            set
            {
                if (value != _request.Id)
                {
                    _request.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public LocationVM Location
        {
            get => new LocationVM(_request.Location);
            set
            {
                if (value.GetLocation() != _request.Location)
                {
                    _request.Location = value.GetLocation();
                    OnPropertyChanged();
                }
            }
        }
        public int LocationId
        {
            get => _request.LocationId;
            set
            {
                if (value != _request.LocationId)
                {
                    _request.LocationId = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => _request.Description;
            set
            {
                if (value != _request.Description)
                {
                    _request.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Language
        {
            get => _request.Language;
            set
            {
                if (value != _request.Language)
                {
                    _request.Language = value;
                    OnPropertyChanged();
                }
            }
        }
        public int NumberOfGuests
        {
            get => _request.NumberOfGuests;
            set
            {
                if (value != _request.NumberOfGuests)
                {
                    _request.NumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly StartDate
        {
            get => _request.StartDate;
            set
            {
                if (value != _request.StartDate)
                {
                    _request.StartDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateOnly EndDate
        {
            get => _request.EndDate;
            set
            {
                if (value != _request.EndDate)
                {
                    _request.EndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Guest2Username
        {
            get => _request.Guest2Username;
            set
            {
                if(value != _request.Guest2Username)
                {
                    _request.Guest2Username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
