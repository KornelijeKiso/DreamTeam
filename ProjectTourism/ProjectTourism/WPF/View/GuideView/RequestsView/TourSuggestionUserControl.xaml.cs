using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectTourism.Localization;
using ProjectTourism.Model;
using ProjectTourism.WPF.View.GuideView.TourView;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.View.TourView
{
    public partial class TourSuggestionUserControl : UserControl, INotifyPropertyChanged
    {
        public TourVM NewTour { get; set; }
        public LocationVM NewLocation { get; set; }
        public GuideVM Guide { get; set; }
        public TourAppointmentVM TourAppointment { get; set; }
        private Dictionary<DateTime, List<TimeSpan>> appointments = new Dictionary<DateTime, List<TimeSpan>>();
        public TourSuggestionUserControl(GuideVM guide)
        {
            InitializeComponent();
            DataContext = this;
            Guide = guide;
            SetModels();
            calendar.BlackoutDates.AddDatesInPast();
        }
        private void SetModels()
        {
            NewTour = new TourVM(new Tour());
            NewTour.dates = new List<DateTime>();
            NewTour.GuideUsername = Guide.Username;
            NewTour.Guide = Guide;
            NewLocation = new LocationVM(new Location());

            if (Guide.TourRequests.Count > 0)
            {
                NewTour.Language = FindMostFrequentItem(Guide.TourRequests[0].TourRequestLanguagesInLastYear());
                string MostCommonLocation = FindMostFrequentItem(Guide.TourRequests[0].TourRequestLocationsInLastYear());
                NewLocation.City = MostCommonLocation.Split(",")[0];
                NewLocation.Country = MostCommonLocation.Split(",")[1].Trim();
            }
            else
            {
                MessageBox.Show("There are no requests yet!");
                HideTourCreateContents();
                ContentArea.Content = new RequestsUserControl(Guide.Username);
            }
        }
        public string FindMostFrequentItem<T>(List<T> items)
        {
            Dictionary<T, int> counts = new Dictionary<T, int>();

            foreach (T item in items)
            {
                if (counts.ContainsKey(item))
                    counts[item]++;
                else
                    counts[item] = 1;
            }

            T mostFrequentItem = default(T);
            int highestCount = 0;

            foreach (KeyValuePair<T, int> pair in counts)
            {
                if (pair.Value > highestCount)
                {
                    mostFrequentItem = pair.Key;
                    highestCount = pair.Value;
                }
            }

            return mostFrequentItem.ToString();
        }
        private void SaveTour_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsListBox.Items.Count == 0)
            {
                ShowLocalizedErrorMessage("NoDatesError");
                return;
            }
            if (NewTour.IsValid && NewLocation.IsValid)
            {
                AddTour();
                ShowPopupMessage(GetLocalizedErrorMessage("TourAdded"));
            }
                
            else
                ShowLocalizedErrorMessage("NoGoodFieldsError");
        }
        private async void ShowPopupMessage(string message)
        {
            popupText.Text = message;
            popupContainer.Visibility = Visibility.Visible;
            await Task.Delay(2500);
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(9);
                popupContainer.Opacity += -0.05;
            }
            popupContainer.Visibility = Visibility.Collapsed;
            popupContainer.Opacity = 1.0;
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
        private void AddTour()
        {
            SaveDates();
            Guide.AddTour(NewTour, NewLocation);
            HideTourCreateContents();
            ContentArea.Content = new HomeUserControl(NewTour.Guide.Username);
        }
        private void HideTourCreateContents()
        {
            datagrid.Children.Cast<UIElement>().Where(child => child is Button || child is Label || child is TextBox)
                                               .ToList().ForEach(child => child.Visibility = Visibility.Hidden);

            List<UIElement> elementsToHide = new List<UIElement>
            { textblockHours,textblockMinutes,hoursTextBox,minutesTextBox,appointmentsListBox,AddTimeButton,
                calendar,rectangle,SaveButton,CreateTourLabel };

            elementsToHide.ForEach(element => element.Visibility = Visibility.Hidden);
        }
        private void AddTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int hours, minutes;
            if (int.TryParse(hoursTextBox.Text, out hours) && int.TryParse(minutesTextBox.Text, out minutes))
            {
                foreach (DateTime date in calendar.SelectedDates)
                {
                    DateTime possibleDate = new DateTime(date.Year, date.Month, date.Day, hours, minutes, 0);
                    if (Guide.CanGuideAcceptAppointment(possibleDate))
                        AddTimeToDate(hours, minutes, date);
                    else
                        ShowLocalizedErrorMessage("GuideAlreadyBusy");
                }
                UpdateAppointmentsListBox();
            }
            else
                ShowLocalizedErrorMessage("InvalidTime");
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
        private void UpdateAppointmentsListBox()
        {
            appointmentsListBox.Items.Clear();
            foreach (KeyValuePair<DateTime, List<TimeSpan>> appointment in appointments)
            {
                AddDateToList(appointment);
            }
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            hoursTextBox.Text = "";
            minutesTextBox.Text = "";
        }
        public void Update() { }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

