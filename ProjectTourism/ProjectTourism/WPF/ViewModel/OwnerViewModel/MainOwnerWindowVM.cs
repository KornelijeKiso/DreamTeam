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
        public MainOwnerWindowVM(string username, int i)
        {
            Content = new Help(username);
            HelpItem.Background = Brushes.LightSkyBlue;
            SetOwner(username);
            NotificationsVisibility = Visibility.Collapsed;
        }
        public MainOwnerWindowVM()
        {
            
        }
        public OwnerDTO Owner { get; set; }
        public NotificationDTO SelectedNotification { get; set; }
        public void SwitchToMyAccommodations(object parameter)
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
        public void SwitchToReservations(object parameter)
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
        public void SwitchToHelp(object parameter)
        {
            Help h = new Help(Owner.Username);
            if(AccommodationsItem.Background==Brushes.LightSkyBlue) h.Tab.SelectedIndex= 0; 
            if(ReservationsItem.Background==Brushes.LightSkyBlue) h.Tab.SelectedIndex= 1; 
            if(ForumsItem.Background==Brushes.LightSkyBlue) h.Tab.SelectedIndex= 2; 

            Content = h;
            AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.LightSkyBlue;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();
        }
        public void SwitchToYourProfile(object parameter)
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
        public void SwitchToForumsClick(object parameter)
        {
            Content = new ForumsMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.LightSkyBlue;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();
        }

        public void CloseNotificationsClick(object parameter)
        {
            NotificationsVisibility = Visibility.Collapsed;
            Owner.SeenNotifications();
            NotificationsItem.Background = Brushes.Transparent;
        }
        public void ShowNotifications(object parameter)
        {
            if (NotificationsVisibility == Visibility.Visible)
            {
                NotificationsVisibility = Visibility.Collapsed;
                NotificationsItem.Background = Brushes.Transparent;
                Owner.SeenNotifications();
                return;
            }
            NotificationsVisibility = Visibility.Visible;
            NotificationsItem.Background = Brushes.LightSkyBlue;
        }
        public void DismissNotificationClick(object parameter)
        {
            new NotificationService().Dismiss(SelectedNotification.GetNotification());
            Owner.Notifications.Remove(SelectedNotification);
        }
        public void DismissAllNotificationClick(object parameter)
        {
            Owner.DismissAllNotification();
        }
        private void SetOwner(string username)
        {
            Owner = new OwnerDTO(username);
        }
        public ICommand SwitchToMyAccommodationsCommand
        {
            get => new RelayCommand(SwitchToMyAccommodations);
        }
        public ICommand SwitchToReservationsCommand
        {
            get => new RelayCommand(SwitchToReservations);
        }
        public ICommand SwitchToYourProfileCommand
        {
            get => new RelayCommand(SwitchToYourProfile);
        }
        public ICommand SwitchToHelpCommand
        {
            get => new RelayCommand(SwitchToHelp);
        }
        public ICommand ShowNotificationsCommand
        {
            get => new RelayCommand(ShowNotifications);
        }
        public ICommand DismissAllNotificationsCommand
        {
            get => new RelayCommand(DismissAllNotificationClick);
        }
        public ICommand DismissNotificationCommand
        {
            get => new RelayCommand(DismissNotificationClick);
        }
        public ICommand CloseNotificationsCommand
        {
            get => new RelayCommand(CloseNotificationsClick);
        }
        public ICommand SwitchToForumsCommand
        {
            get => new RelayCommand(SwitchToForumsClick);
        }
        
    }
}
