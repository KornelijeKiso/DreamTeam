﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjectTourism.Controller;
using ProjectTourism.Model;
using ProjectTourism.Observer;
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    /// <summary>
    /// Interaction logic for TourStopsWindow.xaml
    /// </summary>
    public partial class TourStopsWindow : UserControl, INotifyPropertyChanged, IObserver
    {
        public TourAppointmentVM TourAppointment { get; set; }
        public TicketVM SelectedTicket { get; set; }
        public GuideVM Guide { get; set; }
        public ObservableCollection<TicketVM> Tickets { get; set; } 

        public TourStopsWindow(TourAppointmentVM SelectedTourAppointment)
        {
            InitializeComponent();
            DataContext = this;

            Guide = new GuideVM(SelectedTourAppointment.Tour.Guide.GetGuide());
            TourAppointment = SelectedTourAppointment;
            Tickets = new ObservableCollection<TicketVM>(TourAppointment.Tickets);
            EmergencyButtonSet();
            if (SelectedTourAppointment.CurrentTourStop.Equals(SelectedTourAppointment.Tour.Finish))
            {
                EmergencyStopButton.IsEnabled = false;
                ReviewsButton.IsEnabled = true;
                StopPassedButton.IsEnabled = false;
                TourAppointment.Tour.Guide.EndTour(SelectedTourAppointment);
            }

        }
        private void EmergencyButtonSet()
        {
            if (HasTourStarted())
            {
                StopPassedButton.Content = "Stop passed";
                EmergencyStopButton.IsEnabled = true;
            }
            else
                EmergencyStopButton.IsEnabled = false;
        }

        private bool HasTourStarted()
        {
            return TourAppointment.State == TOURSTATE.STARTED;
        }
        public int PassedButtonClicks()
        {
            int number = 0;
            foreach (var stop in TourAppointment.Tour.StopsList)
            {
                if (stop.Equals(TourAppointment.CurrentTourStop))
                    break;
                number++;
            }
            return number;
        }
        public void NextStop()
        {
            int nextStopIndex = PassedButtonClicks() + 1;
            StopPassedButton.Content = "Stop passed";
            StopTextBox.Text = TourAppointment.Tour.Guide.NextStop(TourAppointment.GetTourAppointment()).Trim();
        }
        private bool CanGuidePassStop()
        {
            if (TourAppointment.State == TOURSTATE.READY)
                return !Guide.TourAppointments.Any(t => t.Id != TourAppointment.Id && t.State == TOURSTATE.STARTED);
            return true;
        }
        private void CheckpointPassedButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanGuidePassStop())
            {
                if (IsNextStopFinish())
                    TourAppointment.Tour.Guide.FinishTourAndReturnStop(TourAppointment);
                else if (CanGoNextStop())
                    NextStop();
            }
            else
                MessageBox.Show("Guide has already started a tour!");
            EmergencyButtonSet();
        }
        private bool IsNextStopFinish()
        {
            //if we are on the one before last or if the stops are null
            return PassedButtonClicks() == TourAppointment.Tour.StopsList.Count() - 2 || TourAppointment.Tour.Stops.Equals(""); 
        }
        private bool CanGoNextStop()
        {
            return (!TourAppointment.Tour.Guide.HasTourStarted || TourAppointment.State == TOURSTATE.STARTED) && !IsNextStopFinish();
        }
        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {
            TourAppointment.Tour.Guide.EmergencyStop(TourAppointment);
        }
        private void TicketStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket.ButtonColor != Brushes.Green)
            {
                SelectedTicket.ButtonColor = Brushes.IndianRed;
                TourAppointment.Tour.Guide.CheckTicket(SelectedTicket);
            }
        }
        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            HideTourStopsContent();
            ContentArea.Content = new ReviewsWindow(TourAppointment);
        }

        private List<UIElement> UIElements()
        {
            return new List<UIElement>
            {
                StopPassedButton, EmergencyStopButton, CurrentTourStopLabel,
                TourStatusTextBox, Grid1, TourStateLabel, ReviewsButton, StopTextBox
            };
        }
        private void HideTourStopsContent()
        {
            UIElements().ForEach(uiElement => uiElement.Visibility = Visibility.Hidden);
        }

        public void Update() { }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
