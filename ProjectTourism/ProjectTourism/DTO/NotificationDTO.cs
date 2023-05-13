﻿using ProjectTourism.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.DTO
{
    public class NotificationDTO : INotifyPropertyChanged
    {
        private Notification _notification;
        public NotificationDTO(Notification notification)
        {
            _notification = notification;
        }
        public Notification GetNotification()
        {
            return _notification;
        }
        public int Id
        {
            get => _notification.Id;
            set
            {
                if (value != _notification.Id)
                {
                    _notification.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string OwnerUsername
        {
            get => _notification.OwnerUsername;
            set
            {
                if (!value.Equals(_notification.OwnerUsername))
                {
                    _notification.OwnerUsername = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Title
        {
            get => _notification.Title;
            set
            {
                if (!value.Equals(_notification.Title))
                {
                    _notification.Title = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Text
        {
            get => _notification.Text;
            set
            {
                if (!value.Equals(_notification.Text))
                {
                    _notification.Text = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime Time
        {
            get => _notification.Time;
            set
            {
                if (!value.Equals(_notification.Time))
                {
                    _notification.Time = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool New
        {
            get => _notification.New;
            set
            {
                if (!value.Equals(_notification.New))
                {
                    _notification.New = value;
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
