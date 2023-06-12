using ProjectTourism.Domain.Model;
using ProjectTourism.WPF.View.Guest1View;
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

namespace ProjectTourism.WPF.ViewModel.Guest1ViewModel
{
    internal class Guest1MainWindowVM : INotifyPropertyChanged
    {
        private object _Content;
        public object Content
        {
            get => _Content;
            set
            {
                if (value != _Content)
                {
                    _Content = value;
                    OnPropertyChanged();
                }
            }
        }

        public MenuItem AccommodationsItem { get; } = new MenuItem();
        public MenuItem MyReservationsItem { get; } = new MenuItem();
        public MenuItem TutorialItem { get; } = new MenuItem();
        public MenuItem GradableItem { get; } = new MenuItem();
        public MenuItem MyProfileItem { get; } = new MenuItem();
        public MenuItem ForumsItem { get; } = new MenuItem();

        public Guest1MainWindowVM(string username)
        {
            Content = new Guest1Window(username);
            //AccommodationsItem.Background = Brushes.LightSkyBlue;
            Guest = new Guest1DTO(username);
        }

        public Guest1MainWindowVM()
        {

        }
        public Guest1DTO Guest { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SwitchToAccommodations(object parameter)
        {
            Content = new Guest1Window(Guest.Username);
            //AccommodationsItem.Background = Brushes.LightSkyBlue;
            //ReservationsItem.Background = Brushes.Transparent;
            //HelpItem.Background = Brushes.Transparent;
            //ForumsItem.Background = Brushes.Transparent;
            //NotificationsItem.Background = Brushes.Transparent;
            //ProfileItem.Background = Brushes.Transparent;
            //ResetNotifications();
        }
        public ICommand SwitchToAccommodationsCommand
        {
            get => new RelayCommand(SwitchToAccommodations);
        }
        public void SwitchToMyReservations(object parameter)
        {
            Content = new Guest1ReservedAccommodations(Guest.Username);
            /*AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.LightSkyBlue;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();*/
        }
        public ICommand SwitchToMyReservationsCommand
        {
            get => new RelayCommand(SwitchToMyReservations);
        }
        public void SwitchToMyProfile(object parameter)
        {
            Content = new MyProfileWindow(Guest.Username);
            /*AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.Transparent;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.LightSkyBlue;
            ResetNotifications();*/
        }
        public ICommand SwitchToMyProfileCommand
        {
            get => new RelayCommand(SwitchToMyProfile);
        }

        public void SwitchToGradable(object parameter)
        {
            Content = new GradableAccommodationsWindow(Guest.Username);
            /*AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.LightSkyBlue;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();*/
        }
        public ICommand SwitchToGradableCommand
        {
            get => new RelayCommand(SwitchToGradable);
        }
        public void SwitchToForums(object parameter)
        {
            ForumsWindow f = new ForumsWindow(Guest.Username);
            f.ShowDialog();
            /*AccommodationsItem.Background = Brushes.Transparent;
            ReservationsItem.Background = Brushes.Transparent;
            HelpItem.Background = Brushes.Transparent;
            ForumsItem.Background = Brushes.LightSkyBlue;
            NotificationsItem.Background = Brushes.Transparent;
            ProfileItem.Background = Brushes.Transparent;
            ResetNotifications();*/
        }
        public ICommand SwitchToForumsCommand
        {
            get => new RelayCommand(SwitchToForums);
        }
    }
}
