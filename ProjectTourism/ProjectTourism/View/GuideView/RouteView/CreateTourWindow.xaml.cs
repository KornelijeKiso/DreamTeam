using System;
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
using ProjectTourism.View.GuideView.RouteView;

namespace ProjectTourism.View.RouteView
{
    /// <summary>
    /// Interaction logic for CreateRouteWindow.xaml
    /// </summary>
    public partial class CreateTourWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Route Route { get; set; }
        public RouteController RouteController { get; set; }
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
            Route = new Route();
            Route.GuideUsername = guide.Username;
            Route.Guide = guide;
            Route.dates = new List<DateTime>();

            RouteController = new RouteController();
            RouteController.Subscribe(this);
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

        private void AttachRouteImagesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveRoute_Click(object sender, RoutedEventArgs e)
        {
            if (Route.IsValid)
                AddRoute();
            else
                MessageBox.Show("Tour can not be made because the fields were not correctly entered.");
        }

        private void AddRoute()
        {
            NewLocation.Id = NewLocationDAO.AddAndReturnId(NewLocation);
            Route.Location = NewLocation;
            Route.LocationId = NewLocation.Id;
            SaveDates();
            RouteController.Add(Route);
            TourAppointmentController.MakeTourAppointments(Route);
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
                        Route.dates.Add(dateTimeParsed);
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

