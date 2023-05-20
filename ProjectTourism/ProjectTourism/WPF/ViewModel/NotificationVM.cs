using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTourism.WPF.ViewModel
{
    public class NotificationVM:INotifyPropertyChanged
    {
        private Notification _notification;
        public NotificationVM(Notification notification)
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
            get => _notification.Username;
            set
            {
                if (!value.Equals(_notification.Username))
                {
                    _notification.Username = value;
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
