﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ProjectTourism.WPF.ViewModel.Guest2ViewModel;

namespace ProjectTourism.WPF.View.Guest2View
{
    /// <summary>
    /// Interaction logic for Guest2MainWindow.xaml
    /// </summary>
    public partial class Guest2MainWindow : Window
    {
        public NavigationVM _NavigationVM { get; set; }
        public Guest2MainWindow(string username)
        {
            InitializeComponent();
            this._NavigationVM = new NavigationVM(username);
            this.DataContext = this._NavigationVM;

            if (_NavigationVM.Guest2.HasNewNotifications) NotificationPopup.IsOpen = true;
            else NotificationPopup.IsOpen = false;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void DisplayNotificationsClick(object sender, RoutedEventArgs e)
        {
            NotificationPopup.IsOpen = false;
            _NavigationVM.DisplayNotifications();
        }

        private void ClosePopupClick(object sender, EventArgs e)
        {
            //NotificationPopup.Visibility = Visibility.Collapsed;
            NotificationPopup.IsOpen = false;
        }

        //private void OpenNotificationPopup(object sender, EventArgs e)
        //{
        //    if (_NavigationVM.Guest2.HasNewNotifications) NotificationPopup.IsOpen = true;
        //    else NotificationPopup.IsOpen = false;
        //}
    }
}
