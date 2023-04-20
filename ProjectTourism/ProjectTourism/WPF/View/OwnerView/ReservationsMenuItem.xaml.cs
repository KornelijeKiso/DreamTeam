﻿using ProjectTourism.Model;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.OwnerView;
using ProjectTourism.WPF.ViewModel;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for ReservationsMenuItem.xaml
    /// </summary>
    public partial class ReservationsMenuItem : UserControl, INotifyPropertyChanged
    {
        public OwnerVM Owner { get; set; }
        public ReservationVM SelectedReservation { get; set; }
        public ReservationsMenuItem(string username)
        {
            InitializeComponent();
            DataContext = this;
            SetOwner(username);
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
        public void Update()
        {
            SetOwner(Owner.Username);        
        }
        public void GradeGuestClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            GradeGuestWindow gradeGuestWindow = new GradeGuestWindow(SelectedReservation, Owner);
            gradeGuestWindow.ShowDialog();
            Update();
        }
        public void SeeReviewClick(object sender, RoutedEventArgs e)
        {
            Guest1ReviewWindow guestsReviewWindow = new Guest1ReviewWindow(SelectedReservation);
            guestsReviewWindow.ShowDialog();
        }

        private void PostponeRequestClick(object sender, RoutedEventArgs e)
        {
            PostponeRequestWindow postponeRequestWindow = new PostponeRequestWindow(SelectedReservation);
            postponeRequestWindow.ShowDialog();
        }
    }
}
