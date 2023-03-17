using System;
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
using ProjectTourism.ModelDAO;
using ProjectTourism.Observer;
using ProjectTourism.View.GuideView.RouteView;

namespace ProjectTourism.View.RouteView
{
    /// <summary>
    /// Interaction logic for CreateRouteWindow.xaml
    /// </summary>
    public partial class CreateRouteWindow : Window, INotifyPropertyChanged, IObserver
    {
        public Route Route { get; set; }
        public RouteController RouteController { get; set; }
        public Location NewLocation { get; set; }
        public LocationDAO NewLocationDAO { get; set; }
        public List<string> Languages { get; set; }
        public ObservableCollection<string> LanguagesObservable { get; set; }


        public CreateRouteWindow(Guide guide)
        {
            InitializeComponent();
            DataContext = this;
            Route = new Route();
            Route.GuideUsername= guide.Username;
            Route.Guide = guide;
            RouteController = new RouteController();
            RouteController.Subscribe(this);
            NewLocation = new Location();
            NewLocationDAO = new LocationDAO();
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
            if(Route.IsValid)
            {
                NewLocation.Id = NewLocationDAO.AddAndReturnId(NewLocation);
                Route.Location = NewLocation;
                Route.LocationId = NewLocation.Id;
                RouteController.Add(Route);
                Close();
            }
            else
            {
                MessageBox.Show("Route can not be made because the fields were not correctly entered.");
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddLanguageButton_Click(object sender, RoutedEventArgs e)
        {
            LanguageAdditionWindow languageAdditionWindow = new LanguageAdditionWindow(LanguagesObservable);
            languageAdditionWindow.ShowDialog();
            if (LanguagesObservable.Count() > Languages.Count())
                LanguageComboBox.SelectedItem = LanguagesObservable.Last().ToString();
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        // calendar - multiple selection
        /*
        private List<DateTime> selectedDates = new List<DateTime>();
        private Dictionary<DateTime, TimeSpan> selectedDateTimes = new Dictionary<DateTime, TimeSpan>();
        //private ObservableCollection<DateTime> selectedDateTimesObservable = new ObservableCollection<DateTime>(selectedDates);
        private DateTime? selectedDate { get; set; }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the previous selection
            selectedDates.Clear();
            selectedDateTimes.Clear();

            // Add the selected dates to the list
            foreach (DateTime date in calendar.SelectedDates)
            {
                selectedDates.Add(date);
                selectedDateTimes.Add(date, new TimeSpan(0, 0, 0));
            }

            // Update the list box with the selected dates and times
            UpdateSelectedDatesListBox();
        }

        private void HoursTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectedDate != null)
            {
                // Update the selected dates and times dictionary with the hours entered
                if (int.TryParse(hoursTextBox.Text, out int hours))
                {
                    foreach (DateTime date in selectedDates)
                    {
                        if (selectedDate.Equals(date))
                        {
                            selectedDateTimes[date] = new TimeSpan(hours, selectedDateTimes[date].Minutes, 0);
                     //       UpdateSelectedDatesListBox();
                        }
                            
                    }
                }

                // Update the list box with the selected dates and times
                UpdateSelectedDatesListBox();
            }
            
        }

        private void MinutesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (selectedDate != null)
            {
                // Update the selected dates and times dictionary with the minutes entered
                if (int.TryParse(minutesTextBox.Text, out int minutes))
                {
                    foreach (DateTime date in selectedDates)
                    {
                       // if (selectedDate.Equals(date))
                        {
                            selectedDateTimes[date] = new TimeSpan(selectedDateTimes[date].Hours, minutes, 0);
                        //    UpdateSelectedDatesListBox();
                        }
                    }
                }

                // Update the list box with the selected dates and times
                UpdateSelectedDatesListBox();
            }
                
        }

        private void UpdateSelectedDatesListBox()
        {
            selectedDatesListBox.Items.Clear();
            foreach (KeyValuePair<DateTime, TimeSpan> kvp in selectedDateTimes)
            {
                selectedDatesListBox.Items.Add(kvp.Key.ToShortDateString() + " " + kvp.Value.ToString(@"hh\:mm"));
            }
        }

        private void SaveCalendarButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<DateTime, TimeSpan> kvp in selectedDateTimes)
            {
                //I need to save the SelectedDateTimes
            }

            // Clear the selection and update the list box
            selectedDates.Clear();
            selectedDateTimes.Clear();
            UpdateSelectedDatesListBox();
        }*/
    }
}
