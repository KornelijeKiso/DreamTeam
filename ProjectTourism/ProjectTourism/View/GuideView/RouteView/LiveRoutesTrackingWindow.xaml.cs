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
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;

namespace ProjectTourism.View.GuideView.RouteView
{
    /// <summary>
    /// Interaction logic for LiveRoutesTrackingWindow.xaml
    /// </summary>
    public partial class LiveRoutesTrackingWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Guide Guide { get; set; }
        public ObservableCollection<TourAppointment> TourAppointments { get; set; }
        public TourAppointment SelectedTourAppointment { get; set; }
        public GuideController GuideController { get; set; }
        public TourAppointmentController TourAppointmentController { get; set; }
        public LiveRoutesTrackingWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            GuideController = new GuideController();
            TourAppointmentController = new TourAppointmentController();
            Guide = GuideController.GetOne(username);
            TourAppointments = new ObservableCollection<TourAppointment>(GuideController.GetGuidesCurrentAppointments(username));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Update()
        {
            throw new NotImplementedException();
        }

        private void StartRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTourAppointment != null)
            {
                RouteStopsWindow routeStopsWindow = new RouteStopsWindow(SelectedTourAppointment.Id);
                SelectedTourAppointment.State = TOURSTATE.STARTED;
                routeStopsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must choose a route which you want to start.");
            }
        }
    }
}
