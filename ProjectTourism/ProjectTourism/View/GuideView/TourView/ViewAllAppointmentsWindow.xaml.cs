﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TrackToursWindow.xaml
    /// </summary>
    public partial class ViewAllAppointmentsWindow : Window, INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<TourAppointment> Appointments { get; set; }
        public TourAppointment SelectedAppointment { get; set; }
        public GuideController GuideController { get; set; }

        public ViewAllAppointmentsWindow(string username)
        {
            InitializeComponent();
            DataContext = this;
            GuideController = new GuideController();
            Appointments = new ObservableCollection<TourAppointment>(GuideController.GetGuidesAppointments(username));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {

        }
        private void QuitTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this appointment?", "Delete appointment", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //Deleting an appointment
                }

            }
            else
            {
                MessageBox.Show("You must choose a tour which you want to quit.");
            }
        }
    }
}