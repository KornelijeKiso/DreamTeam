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
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.View.GuideView.TourView;

namespace ProjectTourism.View.TourView
{
    /// <summary>
    /// Interaction logic for CreateTourWindow.xaml
    /// </summary>
    public partial class CreateTourWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Tour Tour { get; set; }
        public TourController TourController { get; set; }
        public Location NewLocation { get; set; }
        public LocationDAO NewLocationDAO { get; set; }
        public List<string> Languages { get; set; }
        public ObservableCollection<string> LanguagesObservable { get; set; }
        public TourAppointment TourAppointment { get; set; }
        public TourAppointmentController TourAppointmentController { get; set; }
        private Dictionary<DateTime, List<TimeSpan>> appointments = new Dictionary<DateTime, List<TimeSpan>>();
        public CreateTourWindow(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            Tour = new Tour();
            Tour.GuideUsername = guide.Username;
            Tour.Guide = guide;
            Tour.dates = new List<DateTime>();

            TourController = new TourController();
            TourController.Subscribe(this);
            NewLocation = new Location();
            NewLocationDAO = new LocationDAO();
            TourAppointmentController = new TourAppointmentController();

            Languages = new List<string>
            {
                "English",
                "Spanish",
                "French",
                "German",
                "Italian",
                "Portuguese",
                "Russian",
                "Japanese",
                "Korean",
                "Chinese",
                "Arabic",
                "Hebrew",
                "Dutch",
                "Swedish",
                "Norwegian",
                "Danish",
                "Finnish",
                "Turkish",
                "Greek",
                "Polish"
            };
            LanguagesObservable = new ObservableCollection<string>(Languages);
            LanguageComboBox.ItemsSource = LanguagesObservable;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update()
        {

        }

        private void AttachTourImagesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveTour_Click(object sender, RoutedEventArgs e)
        {
            if(appointmentsListBox.Items.Count == 0)
            {
                MessageBox.Show("You have not choosed any dates for this tour!");
                return;
            }
            if (Tour.IsValid)
                AddTour();
            else
                MessageBox.Show("Tour can not be made because the fields were not correctly entered.");
        }

        private void AddTour()
        {
            NewLocation.Id = NewLocationDAO.AddAndReturnId(NewLocation);
            Tour.Location = NewLocation;
            Tour.LocationId = NewLocation.Id;
            SaveDates();
            TourController.Add(Tour);
            TourAppointmentController.MakeTourAppointments(Tour);
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            LanguageAdditionWindow languageAdditionWindow = new LanguageAdditionWindow(LanguagesObservable);
            languageAdditionWindow.ShowDialog();
            if (LanguageAdded())
                LanguageComboBox.SelectedItem = LanguagesObservable.Last().ToString();
        }

        private bool LanguageAdded()
        {
            return LanguagesObservable.Count() > Languages.Count();
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
                    TimeSpan time = new TimeSpan(hours, minutes, 0);
                    if (!appointments.ContainsKey(date))
                    {
                        appointments[date] = new List<TimeSpan>();
                    }
                    appointments[date].Add(time);
                }
                UpdateAppointmentsListBox();
            }
            else
            {
                MessageBox.Show("Invalid time entered.");
            }
        }

        private void UpdateAppointmentsListBox()
        {
            appointmentsListBox.Items.Clear();
            foreach (KeyValuePair<DateTime, List<TimeSpan>> appointment in appointments)
            {
                string appointmentText = appointment.Key.ToShortDateString() + " ";
                foreach (TimeSpan time in appointment.Value)
                {
                    appointmentText += time.ToString("hh\\:mm") + ", ";
                }
                appointmentsListBox.Items.Add(appointmentText.TrimEnd(',', ' '));
            }
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
                        Tour.dates.Add(dateTimeParsed);
                }
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            hoursTextBox.Text = "";
            minutesTextBox.Text = "";
        }
    }
}

