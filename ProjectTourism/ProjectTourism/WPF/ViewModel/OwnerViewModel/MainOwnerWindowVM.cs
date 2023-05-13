using ProjectTourism.Domain.Model;
using ProjectTourism.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using ProjectTourism.Utilities;
using System.Windows.Controls;
using ProjectTourism.DTO;
using System.Windows.Input;
using ProjectTourism.Services;

namespace ProjectTourism.WPF.ViewModel.OwnerViewModel
{
    public class MainOwnerWindowVM: ViewModelBase
    {
        private object _Content;
        public object Content
        {
            get => _Content;
            set
            {
                if(value!=_Content)
                {
                    _Content = value;
                    OnPropertyChanged();
                }
            }
        }
        public MenuItem AccommodationsItem { get; } = new MenuItem();
        public MenuItem ReservationsItem { get; } = new MenuItem();
        public MenuItem HelpItem { get; } = new MenuItem();
        public MenuItem NotificationsItem { get; } = new MenuItem();
        public MenuItem ProfileItem { get; } = new MenuItem();
        public MenuItem ForumsItem { get; } = new MenuItem();
        private Visibility _NotificationsVisibility;
        public Visibility NotificationsVisibility
        {
            get => _NotificationsVisibility;
            set
            {
                if(value != _NotificationsVisibility)
                {
                    _NotificationsVisibility = value;
                    OnPropertyChanged();
                }
            }
        }
        public MainOwnerWindowVM(string username)
        {
            Content = new YourAccommodationsMenuItem(username);
            AccommodationsItem.Background = Brushes.LightSkyBlue;
            SetOwner(username);
            NotificationsVisibility = Visibility.Collapsed;
        }
        public MainOwnerWindowVM()
        {
            
        }
        public OwnerDTO Owner { get; set; }
        public NotificationDTO SelectedNotification { get; set; }
        public void SwitchToMyAccommodations()
        {
            Content= new YourAccommodationsMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.LightSkyBlue;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();
        }

        private void ResetNotifications()
        {
            if (NotificationsVisibility == Visibility.Visible)
            {
                NotificationsVisibility = Visibility.Collapsed;
                Owner.SeenNotifications();
            }
        }

        public void SwitchToReservations()
        {
            Content = new ReservationsMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.LightSkyBlue;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();
        }
        public void SwitchToYourProfile()
        {
            Content = new YourProfileMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.LightSkyBlue;
            ResetNotifications();
        }

        public void CloseNotificationsClick()
        {
            NotificationsVisibility = Visibility.Collapsed;
            Owner.SeenNotifications();
        }
        public void ShowNotifications()
        {
            NotificationsVisibility = Visibility.Visible;
        }
        public void DismissNotificationClick()
        {
            new NotificationService().Dismiss(SelectedNotification.GetNotification());
            Owner.Notifications.Remove(SelectedNotification);
        }
        public void DismissAllNotificationClick()
        {
            Owner.DismissAllNotification();
        }
        private void SetOwner(string username)
        {
            Owner = new OwnerDTO(username);
        }
        public void AreAllGuestsGraded(object sender, RoutedEventArgs e)
        {
            foreach (var reservation in Owner.Reservations)
            {
                if (reservation.IsAbleToGrade() && !reservation.Graded)
                {
                    MessageBox.Show("There are guests who are waiting to be graded.");
                    break;
                }
            }
        }
        private ICommand _SwitchToMyAccommodationsCommand;
        public ICommand SwitchToMyAccommodationsCommand
        {
            get
            {
                return _SwitchToMyAccommodationsCommand ?? (_SwitchToMyAccommodationsCommand = new CommandHandler(() => SwitchToMyAccommodations(), () => true));
            }
        }
        private ICommand _SwitchToReservationsCommand;
        public ICommand SwitchToReservationsCommand
        {
            get
            {
                return _SwitchToReservationsCommand ?? (_SwitchToReservationsCommand = new CommandHandler(() => SwitchToReservations(), () => true));
            }
        }
        private ICommand _SwitchToYourProfileCommand;
        public ICommand SwitchToYourProfileCommand
        {
            get
            {
                return _SwitchToYourProfileCommand ?? (_SwitchToYourProfileCommand = new CommandHandler(() => SwitchToYourProfile(), () => true));
            }
        }
        private ICommand _ShowNotificationsCommand;
        public ICommand ShowNotificationsCommand
        {
            get
            {
                return _ShowNotificationsCommand ?? (_ShowNotificationsCommand = new CommandHandler(() => ShowNotifications(), () => true));
            }
        }
        private ICommand _DismissAllNotificationsCommand;
        public ICommand DismissAllNotificationsCommand
        {
            get
            {
                return _DismissAllNotificationsCommand ?? (_DismissAllNotificationsCommand = new CommandHandler(() => DismissAllNotificationClick(), () => true));
            }
        }
        private ICommand _DismissNotificationCommand;
        public ICommand DismissNotificationCommand
        {
            get
            {
                return _DismissNotificationCommand ?? (_DismissNotificationCommand = new CommandHandler(() => DismissNotificationClick(), () => true));
            }
        }
        private ICommand _CloseNotificationsCommand;
        public ICommand CloseNotificationsCommand
        {
            get
            {
                return _CloseNotificationsCommand ?? (_CloseNotificationsCommand = new CommandHandler(() => CloseNotificationsClick(), () => true));
            }
        }
    }
}
