﻿using ProjectTourism.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Animation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectTourism.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RenovationsWindow.xaml
    /// </summary>
    public partial class RenovationsWindow : Window, INotifyPropertyChanged
    {
        public AccommodationVM Accommodation { get; set; }
        public RenovationVM SelectedRenovation { get; set; }
        public RenovationVM NewRenovation { get; set; }
        public RenovationAppointmentVM RenovationAppointment {get; set;}
        public RenovationsWindow(AccommodationVM accommodation)
        {
            Accommodation = accommodation;
            NewRenovation = new RenovationVM();
            RenovationAppointment = new RenovationAppointmentVM();
            RenovationAppointment.AccommodationId = Accommodation.Id;
            NewRenovation.AccommodationId = Accommodation.Id;
            InitializeComponent();
            DataContext = this;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ScheduleRenovationClick(object sender, RoutedEventArgs e)
        {
            if (SelectedFreeAppointment.Text.ToString() == "") MessageBox.Show("You have to select appointment for your renovation.");
            else
            {
                Accommodation.ScheduleNewRenovation(NewRenovation);
                ShowPopupMessage("You have successfully scheduled new renovation.\n You can see it in Previously scheduled renovations down below.");
                NewRenovation.Reset();
                SelectedFreeAppointment.Text = "";
            }
        }
        public void SelectFreeAppointmentClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DurationTextBox.Text.ToString()))
            {
                MessageBox.Show("Enter a duration.");
                return;
            }
            SelectFreeAppointmentForRenovatonWindow selectFreeAppointmentForRenovatonWindow = new SelectFreeAppointmentForRenovatonWindow(RenovationAppointment);
            selectFreeAppointmentForRenovatonWindow.ShowDialog();
            if (selectFreeAppointmentForRenovatonWindow.SelectedRenovation != null)
            {
                SelectedFreeAppointment.Text = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate.ToString() + " - " + selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate.ToString();
                NewRenovation.StartDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.StartDate;
                NewRenovation.EndDate = selectFreeAppointmentForRenovatonWindow.SelectedRenovation.EndDate;
            }
        }
        private async void ShowPopupMessage(string message)
        {
            popupText.Text = message;
            popupContainer.Visibility = Visibility.Visible;
            await Task.Delay(5000);
            var fadeOutAnimation = new DoubleAnimation
            {
                From = 0.9,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            fadeOutAnimation.Completed += (s, e) =>
            {
                popupContainer.Visibility = Visibility.Collapsed;
            };
            popupBorder.BeginAnimation(OpacityProperty, fadeOutAnimation);
            InitializeComponent();
            DataContext = this;
        }
        public void DeclineClick(object sender, RoutedEventArgs e)
        {
            SelectedFreeAppointment.Text = "";
        }
        private void IntegerValidation(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            else
            {
                int res;
                if (int.TryParse(DurationTextBox.Value.ToString(), out res))
                {
                    if(res == 0)
                    {
                        e.Handled = true;
                        DurationTextBox.Value = 1;
                    }
                }
            }
        }

        public void CancelRenovationClick(object sender, RoutedEventArgs e)
        {
            Accommodation.CancelRenovation(SelectedRenovation);
            MessageBox.Show("You have successfully canceled renovation.");
        }
    }
}
