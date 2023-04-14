﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.TourView
{
    /// <summary>
    /// Interaction logic for CreateTourWindow.xaml
    /// </summary>
    public partial class CreateTourWindow : UserControl, INotifyPropertyChanged, IObserver
    {
        public TourVM NewTour { get; set; }
        public LocationVM NewLocation { get; set; }
        public GuideVM Guide { get; set; }
        public TourAppointmentVM TourAppointment { get; set; }
        public ObservableCollection<string> LanguagesObservable { get; set; }
        private Dictionary<DateTime, List<TimeSpan>> appointments = new Dictionary<DateTime, List<TimeSpan>>();
        public CreateTourWindow(GuideVM guide)
        {
            InitializeComponent();
            DataContext = this;
            Guide = guide;

            SetModels();

            NewTour.GuideUsername = guide.Username;
            NewTour.Guide = guide;
            NewTour.dates = new List<DateTime>();

            LanguagesObservable = new ObservableCollection<string>(SetLanguages());
            LanguageComboBox.ItemsSource = LanguagesObservable;
        }

        private List<string> SetLanguages()
        {
            return new List<string>
            {
                "English","Spanish","French","German","Italian","Portuguese","Russian","Japanese","Korean","Chinese",
                "Dutch","Swedish","Norwegian","Danish","Finnish","Turkish","Greek","Polish","Arabic","Hebrew",
            };
        }

        private void SetModels()
        {
            NewTour = new TourVM(new Tour());
            NewLocation = new LocationVM(new Location());
        }
        public void Update()
        {

        }
        private void SaveTour_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsListBox.Items.Count == 0)
            {
                MessageBox.Show("You did not choose any dates for this tour!");
                return;
            }
            if (NewTour.IsValid && NewLocation.IsValid)
                AddTour();
            else
                MessageBox.Show("Tour can not be made because the fields were not entered correctly.");
        }
        private void AddTour()
        {
            SaveDates();
            Guide.AddTour(NewTour, NewLocation);
            HideTourCreateContents();
            ContentArea.Content = new HomeWindow(NewTour.Guide.Username);
        }
        private void HideTourCreateContents()
        {
            foreach (var child in grid1.Children)
            {
                if (child is Button || child is Label || child is TextBox)
                {
                    (child as UIElement).Visibility = Visibility.Hidden;
                }
            }
            foreach (var child in grid2.Children)
            {
                if (child is Button || child is Label || child is TextBox)
                {
                    (child as UIElement).Visibility = Visibility.Hidden;
                }
            }
            foreach (var child in grid3.Children)
            {
                if (child is Button || child is Label || child is TextBox)
                {
                    (child as UIElement).Visibility = Visibility.Hidden;
                }
            }
            textblockHours.Visibility = Visibility.Hidden;
            textblockMinutes.Visibility =Visibility.Hidden;
            hoursTextBox.Visibility = Visibility.Hidden;
            minutesTextBox.Visibility = Visibility.Hidden;
            appointmentsListBox.Visibility = Visibility.Hidden;
            AddTimeButton.Visibility = Visibility.Hidden;
            calendar.Visibility = Visibility.Hidden;
            LanguageComboBox.Visibility = Visibility.Hidden;
            rectangle.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Hidden;
            AddLanguageButton.Visibility = Visibility.Hidden;
            CreateTourLabel.Visibility = Visibility.Hidden;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            LanguageAdditionWindow languageAdditionWindow = new LanguageAdditionWindow(LanguagesObservable);
            languageAdditionWindow.ShowDialog();
            if (languageAdditionWindow.LanguageAdded)
                LanguageComboBox.SelectedItem = LanguagesObservable.Last().ToString();
        }


        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int hours, minutes;
            if (int.TryParse(hoursTextBox.Text, out hours) && int.TryParse(minutesTextBox.Text, out minutes))
            {
                foreach (DateTime date in calendar.SelectedDates)
                {
                    AddTimeToDate(hours, minutes, date);
                }
                UpdateAppointmentsListBox();
            }
            else
            {
                MessageBox.Show("Invalid time entered.");
            }
        }

        private void AddTimeToDate(int hours, int minutes, DateTime date)
        {
            TimeSpan time = new TimeSpan(hours, minutes, 0);
            if (!appointments.ContainsKey(date))
            {
                appointments[date] = new List<TimeSpan>();
            }
            appointments[date].Add(time);
        }

        private void UpdateAppointmentsListBox()
        {
            appointmentsListBox.Items.Clear();
            foreach (KeyValuePair<DateTime, List<TimeSpan>> appointment in appointments)
            {
                AddDateToList(appointment);
            }
        }

        private void AddDateToList(KeyValuePair<DateTime, List<TimeSpan>> appointment)
        {
            string appointmentText = appointment.Key.ToShortDateString() + " ";
            foreach (TimeSpan time in appointment.Value)
            {
                appointmentText += time.ToString("hh\\:mm") + ", ";
            }
            appointmentsListBox.Items.Add(appointmentText.TrimEnd(',', ' '));
        }

        private void SaveDates()
        {
            foreach (KeyValuePair<DateTime, List<TimeSpan>> appointment in appointments)
            {
                string appointmentText = "";
                foreach (TimeSpan time in appointment.Value)
                {
                    appointmentText = appointment.Key.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern) + " ";
                    appointmentText += time.ToString("hh\\:mm");
                    if (DateTime.TryParse(appointmentText, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out var dateTimeParsed))
                        NewTour.dates.Add(dateTimeParsed);
                }
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            hoursTextBox.Text = "";
            minutesTextBox.Text = "";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

