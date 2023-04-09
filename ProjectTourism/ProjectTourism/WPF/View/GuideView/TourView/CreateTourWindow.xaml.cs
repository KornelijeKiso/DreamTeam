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
using ProjectTourism.Repositories;
using ProjectTourism.Services;
using ProjectTourism.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.TourView
{
    /// <summary>
    /// Interaction logic for CreateTourWindow.xaml
    /// </summary>
    public partial class CreateTourWindow : Window, INotifyPropertyChanged, IObserver
    {
        public TourVM Tour { get; set; }
        public TourService TourService { get; set; }
        public LocationVM NewLocation { get; set; }
        public LocationService NewLocationService { get; set; }
        public List<string> Languages { get; set; }
        public ObservableCollection<string> LanguagesObservable { get; set; }
        public TourAppointmentVM TourAppointment { get; set; }
        public TourAppointmentService TourAppointmentService { get; set; }
        private Dictionary<DateTime, List<TimeSpan>> appointments = new Dictionary<DateTime, List<TimeSpan>>();
        public CreateTourWindow(GuideVM guide)
        {
            InitializeComponent();
            DataContext = this;

            Tour = new TourVM(new Tour());
            Tour.GuideUsername = guide.Username;
            Tour.Guide = guide;
            Tour.dates = new List<DateTime>();

            TourService = new TourService(new TourRepository());
            TourService.Subscribe(this);
            NewLocation = new LocationVM(new Location());
            NewLocationService = new LocationService(new LocationRepository());
            TourAppointmentService = new TourAppointmentService(new TourAppointmentRepository());

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
            if (appointmentsListBox.Items.Count == 0)
            {
                MessageBox.Show("You have not choosed any dates for this tour!");
                return;
            }
            if (Tour.IsValid && NewLocation.IsValid) // <- this doesnt work because Tour.IsValid=false i dont know why
                AddTour();
            else
                MessageBox.Show("Tour can not be made because the fields were not correctly entered.");
        }

        private void AddTour()
        {
            NewLocation.Id = NewLocationService.AddAndReturnId(NewLocation);
            Tour.Location = NewLocation;
            Tour.LocationId = NewLocation.Id;
            SaveDates();
            TourService.Add(Tour);
            TourAppointmentService.MakeTourAppointments(Tour);
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

