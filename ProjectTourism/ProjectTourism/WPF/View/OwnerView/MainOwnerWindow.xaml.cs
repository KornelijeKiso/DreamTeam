using ProjectTourism.Services;
using ProjectTourism.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectTourism.Repositories;
using ProjectTourism.WPF.ViewModel;
using ProjectTourism.WPF.View.OwnerView;

namespace ProjectTourism.View.OwnerView
{
    /// <summary>
    /// Interaction logic for MainOwnerWindow.xaml
    /// </summary>
    public partial class MainOwnerWindow : Window, INotifyPropertyChanged
    {
        public OwnerVM Owner { get; set; }
        public NotificationVM SelectedNotification { get; set; }

        public MainOwnerWindow(string username)
        {
            InitializeComponent();
            DataContext = this;

            Content.Content = new YourAccommodationsMenuItem(username);
            AccommodationsItem.Background = Brushes.LightSkyBlue;
            SetOwner(username);
            
        }
        public void SwitchToMyAccommodations(object sender, EventArgs e)
        {
            Content.Content = new YourAccommodationsMenuItem(Owner.Username);
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
            if (Notifications.Visibility == Visibility.Visible)
            {
                Notifications.Visibility = Visibility.Collapsed;
                Owner.SeenNotifications();
            }
        }

        public void SwitchToReservations(object sender, EventArgs e)
        {
            Content.Content = new ReservationsMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.LightSkyBlue;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();
        }
        public void SwitchToYourProfile(object sender, EventArgs e)
        {
            Content.Content = new YourProfileMenuItem(Owner.Username);
            AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.LightSkyBlue;
            ResetNotifications();
        }

        public void CloseNotificationsClick(object sender, EventArgs e)
        {
            Notifications.Visibility = Visibility.Collapsed;
            Owner.SeenNotifications();
        }
        public void ShowNotifications(object sender, EventArgs e)
        {
            Notifications.Visibility = Visibility.Visible;
        }
        public void DismissNotificationClick(object sender, EventArgs e)
        {
            Owner.DismissNotification(SelectedNotification);
        }
        public void DismissAllNotificationClick(object sender, EventArgs e)
        {
            Owner.DismissAllNotification();
        }
        private void SetOwner(string username)
        {
            Owner = new OwnerVM(username);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AreAllGuestsGraded(object sender, EventArgs e)
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

    }
}
