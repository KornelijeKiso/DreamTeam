﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ProjectTourism.DTO;
using ProjectTourism.Localization;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.GuideView.TourView
{
    public partial class TourStopsUserControl : UserControl, INotifyPropertyChanged
    {
        public TourAppointmentDTO TourAppointment { get; set; }
        public TicketDTO SelectedTicket { get; set; }
        public GuideDTO Guide { get; set; }
        public ObservableCollection<TicketDTO> Tickets { get; set; }
        public TourStopsUserControl(TourAppointmentDTO SelectedTourAppointment)
        {
            InitializeComponent();
            DataContext = this;

            Guide = new GuideDTO(SelectedTourAppointment.Tour.Guide.GetGuide());
            TourAppointment = SelectedTourAppointment;
            Tickets = new ObservableCollection<TicketDTO>(TourAppointment.Tickets);
            EmergencyButtonSet();
            if (SelectedTourAppointment.CurrentTourStop.Equals(SelectedTourAppointment.Tour.Finish))
            {
                EmergencyStopButton.IsEnabled = false;
                StopPassedButton.IsEnabled = false;
                TourAppointment.Tour.Guide.EndTour(SelectedTourAppointment);
            }
        }
        private void EmergencyButtonSet()
        {
            if (TourAppointment.State == TOURSTATE.STARTED)
            {
                StopPassedButton.Content = "Stop passed";
                EmergencyStopButton.IsEnabled = true;
            }
            else
                EmergencyStopButton.IsEnabled = false;
        }
        public int PassedButtonClicks()
        {
            int number = 0;
            if(TourAppointment.Tour.StopsList != null)
            {
                foreach (var stop in TourAppointment.Tour.StopsList)
                {
                    if (stop.Equals(TourAppointment.CurrentTourStop))
                        break;
                    number++;
                }
            }
            return number;
        }
        public void NextStop()
        {
            StopTextBox.Text = TourAppointment.Tour.Guide.NextStop(TourAppointment.GetTourAppointment()).Trim();
            TourStatusTextBox.Text = TOURSTATE.STARTED.ToString();
        }
        private bool CanGuidePassStop()
        {
            if ((TourAppointment.State == TOURSTATE.READY && !Guide.HasTourStarted) || 
                    (TourAppointment.State == TOURSTATE.STARTED && Guide.HasTourStarted))
                return true;
            return false;
        }
        private void CheckpointPassedButton_Click(object sender, RoutedEventArgs e)
        {
            Guide = new GuideDTO(TourAppointment.Tour.Guide.GetGuide());
            if (CanGuidePassStop())
            {
                if(TourAppointment.Tour.Stops != null)
                {
                    if (IsNextStopFinish())
                    {
                        TourAppointment.Tour.Guide.FinishTourAndReturnStop(TourAppointment);
                        StopPassedButton.IsEnabled = false;
                    }
                    else
                        NextStop();
                }
                else
                {
                    TourAppointment.Tour.Guide.FinishTourAndReturnStop(TourAppointment);
                    StopPassedButton.IsEnabled = false;
                }
            }
            else
                ShowLocalizedErrorMessage("GuideAlreadyStartedTourError");
            EmergencyButtonSet();
        }
        private bool IsNextStopFinish()
        {
            //if we are on the one before last or if the stops are null
            return PassedButtonClicks() == TourAppointment.Tour.StopsList.Count() - 2 || TourAppointment.Tour.Stops.Equals("") ; //TourAppointment.Tour.Stops.Equals("");
        }
        private void EmergencyStopButton_Click(object sender, RoutedEventArgs e)
        {
            TourAppointment.Tour.Guide.EmergencyStop(TourAppointment);

            List<UIElement> elementsToHide = new List<UIElement>
            { ReviewsButton, TourStateLabel, StopPassedButton, EmergencyStopButton, Grid1, TourNameLabel, CurrentTourStopLabel,TourStateLabel, StopTextBox, TourStatusTextBox };

            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
            ContentArea.Content = new TodaysToursUserControl(Guide.Username);
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
            if (TourAppointment.Tickets.Count != 0 && TourAppointment.TicketGrades.Count != 0)
            {
                HideTourStopsContent();
                ContentArea.Content = new ReviewsUserControl(TourAppointment);
            }
            else
                ShowLocalizedErrorMessage("NoReviewsError");
        }
        void ShowLocalizedErrorMessage(string resourceKey)
        {
            string errorMessage = GetLocalizedErrorMessage(resourceKey);
            MessageBox.Show(errorMessage);
        }

        string GetLocalizedErrorMessage(string resourceKey)
        {
            TextBlock Templabel = new TextBlock();
            LocExtension locExtension = new LocExtension(resourceKey);
            BindingOperations.SetBinding(Templabel, TextBlock.TextProperty, locExtension.ProvideValue(null) as BindingBase);
            return Templabel.Text;
        }
        private void HideTourStopsContent()
        {
            List<UIElement> elementsToHide = new List<UIElement> { StopPassedButton, EmergencyStopButton, CurrentTourStopLabel,
                                                                   TourStatusTextBox, Grid1, TourStateLabel, ReviewsButton, StopTextBox };
            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
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
